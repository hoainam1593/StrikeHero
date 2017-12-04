using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Structs

public enum FireMode {
	Semi,      // Shoot once when the mouse is down.
	Auto,      // Shoot periodically over shooting rate as long as the mouse is pressing.
	Launcher   // Launcher a rocket.
}

public struct GunView {
	
	public Vector3 GunPos;
	public Quaternion GunRot;
	public float CameraFOV;

	public GunView(Vector3 _gunPos, Quaternion _gunRot, float _cameraFOV) {
		GunPos = _gunPos;
		GunRot = _gunRot;
		CameraFOV = _cameraFOV;
	}
}

#endregion

public class GunController : MonoBehaviour {

	#region Inspector Members

	[Header("Model")]
	public GameObject m_gunOnly;

	[Header("Animation")]
	public Animator m_gunAnimator;

	[Header("Fire")]
	public FireMode m_fireMode;
	public float m_fireRate;
	public float m_fireRange;
	public int m_fireDamage;

	[Header("Ammo")]
	public int m_bulletCapacity;
	public int m_magazineCapacity;

	[Header("Aim")]
	public Transform m_transformAdjust;
	public Transform m_aimTransform;
	public float m_aimFOV;
	public float m_aimSpeed;

	[Header("Effects")]
	public Transform m_gunMuzzle;
	public ParticleSystem m_muzzleFlash;
	public GameObject m_trailEffect;

	[Header("Crosshair")]
	public GameObject m_crosshairPrefab;
	public Vector2 m_crosshairCenterOffset;

	[Header("Sound")]
	public AudioClip m_fireSound;
	public AudioClip m_emptyFireSound;
	public AudioClip m_reloadSound;

	#endregion

	#region Class Members

	// Ammo.
	public int NBullets { get; set; }
	public int NMagazines { get; set; }

	// Aim.
	public GunView UnaimedView { get; set; }
	public GunView AimedView { get; set; }
	public GunView TargetView { get; set; }

	// State machine.
	private IGunState m_primaryState = null;
	private IGunState m_secondaryState = null;

	public Crosshair _Crosshair { get; private set; }
	private RaycastHit m_raycastHit;
	private bool m_isRaycastHitSomething = false;

	#endregion

	#region Public functions

	void Awake() {
		_Crosshair = null;
	}

	void Start () {
		UnaimedView = new GunView (m_transformAdjust.localPosition, m_transformAdjust.localRotation, Camera.main.fieldOfView);
		AimedView = new GunView (m_aimTransform.localPosition, m_aimTransform.localRotation, m_aimFOV);
		TargetView = UnaimedView;

		NBullets = m_bulletCapacity;
		NMagazines = m_magazineCapacity;

		ChangeState (new GunStateDraw ());
	}

	void Update () {
		// Raycast.
		RaycastToScene();

		// Aim.
		ProcessAim ();

		// State mahine.
		m_primaryState.Update (this);

		if (m_secondaryState != null) {
			m_secondaryState.Update (this);
		}
			
		// UI.
		UpdateUI();
	}

	public void ActiveGun() {
		// Activate hero.
		gameObject.SetActive (true);

		// Instantiate crosshair.
		if (m_crosshairPrefab != null) {
			GameObject obj = Instantiate(m_crosshairPrefab);

			_Crosshair = obj.GetComponent<Crosshair> ();
			_Crosshair.m_centerOffset = m_crosshairCenterOffset;
		}

		// Transition to draw state.
		ChangeState (new GunStateDraw ());
	}

	public void DeactiveGun() {
		// Deactivate hero.
		gameObject.SetActive (false);

		// Destroy crosshair.
		if (_Crosshair != null) {
			Destroy (_Crosshair.gameObject);
		}

		// Throw gun away.
		Transform throwingPosition = Camera.main.transform;
		GameObject gunModel = Instantiate(m_gunOnly, throwingPosition.position, throwingPosition.rotation);
		gunModel.GetComponent<Rigidbody> ().AddRelativeForce (0, 250, Random.Range (100, 200));
		gunModel.GetComponent<Rigidbody> ().AddTorque (-transform.up * 40);
	}

	public void AddMagazines (int amount) {
		NMagazines += amount;
		if (NMagazines > m_magazineCapacity) {
			NMagazines = m_magazineCapacity;
		}
	}

	#endregion

	#region Fire

	// Can not run co-routine outside of MonoBehaviour, so put fire code here.

	void FireOneBullet() {
		// Decrease number of bullets.
		NBullets--;

		// Start over fire animation.
		m_gunAnimator.Play (0, -1, 0.0f);

		// Show muzzle flash effect.
		m_muzzleFlash.Play();

		// Play firing sound.
		GameStats._GunsManager.GetComponent<AudioSource>().PlayOneShot(m_fireSound);

		// Ray casting.
		if (m_isRaycastHitSomething) {

			// Hit enemy.
			BoneCollider enemy = m_raycastHit.collider.GetComponent<BoneCollider>();
			if (enemy != null) {
				enemy.GetHit (m_fireDamage);
			}

			// Decal.
			GameStats._DecalsManager.SpawnDecal (m_raycastHit);
		}
	}

	void FireOneRocket () {
		// Decrease number of rockets.
		NBullets--;

		// Play firing sound.
		GameStats._GunsManager.GetComponent<AudioSource>().PlayOneShot(m_fireSound);

		// Create rocket trail effect.
		Vector3 pos = m_gunMuzzle.position;
		Quaternion rot = Quaternion.FromToRotation (Vector3.forward, m_gunMuzzle.forward);
		Instantiate(m_trailEffect, pos, rot);
	}

	IEnumerator FireCoroutine() {
		while (true) {
			if (m_fireMode == FireMode.Launcher) {
				FireOneRocket ();
			} else {
				FireOneBullet ();
			}

			if (m_fireMode == FireMode.Auto) {
				if (NBullets <= 0) {
					GameStats._MessageNotifier.PushFloatMessage ("Out of bullet");
					yield break;
				} else {
					yield return new WaitForSeconds (m_fireRate);
				}
			} else {
				yield break;
			}
		}
	}

	public void StartFireCoroutine() {
		StartCoroutine ("FireCoroutine");
	}

	public void StopFireCoroutine() {
		StopCoroutine ("FireCoroutine");
	}

	#endregion

	#region Aim

	void ProcessAim() {
		float t = m_aimSpeed * Time.deltaTime;

		m_transformAdjust.localPosition = Vector3.Lerp (m_transformAdjust.localPosition, TargetView.GunPos, t);
		m_transformAdjust.localRotation = Quaternion.Lerp (m_transformAdjust.localRotation, TargetView.GunRot, t);
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, TargetView.CameraFOV, t);
	}

	#endregion

	#region State machine

	public void ChangeState (IGunState newState) {
		if (m_primaryState != null) {
			m_primaryState.EndState (this);
		}

		m_primaryState = newState;
		m_primaryState.StartState (this);
	}

	public void AddSecondaryState (IGunState secondaryState) {
		m_secondaryState = secondaryState;
		m_secondaryState.StartState (this);
	}

	public void RemoveSecondaryState () {
		m_secondaryState.EndState (this);
		m_secondaryState = null;
	}

	public bool IsSecondaryState(IGunState state) {
		return (state == m_secondaryState);
	}

	#endregion

	#region UI

	void UpdateUI () {
		UICharacterProperties ui = GameStats._UICharacterProperties;
		ui.m_textBullets.text = NBullets.ToString ();
		ui.m_textMagazines.text = NMagazines.ToString ();
	}

	#endregion

	#region Utils

	void RaycastToScene() {
		if (m_fireMode == FireMode.Launcher) {
			return;
		}

		m_isRaycastHitSomething = Physics.Raycast (
			Camera.main.transform.position, 
			Camera.main.transform.forward, 
			out m_raycastHit, 
			m_fireRange);
		
		if (_Crosshair != null) {
			bool isHitEnemy = false;

			if (m_isRaycastHitSomething) {
				isHitEnemy = (m_raycastHit.collider.GetComponent<BoneCollider> () != null);
			}

			if (isHitEnemy) {
				_Crosshair.AimEnemy ();
			} else {
				_Crosshair.ResetColor ();
			}
		}
	}

	#endregion
}

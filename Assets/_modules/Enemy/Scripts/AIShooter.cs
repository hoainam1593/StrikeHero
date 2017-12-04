using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIShooter : MonoBehaviour {

	public float m_beginShootingLimit;
	public Transform m_muzzle;
	public GameObject m_projectile;
	public AudioClip m_firingSound;

	private NavMeshAgent m_navMesh;
	private Animator m_animator;
	private AudioSource m_audioSource;

	// Use this for initialization
	void Start () {
		m_navMesh = GetComponent<NavMeshAgent> ();
		m_animator = GetComponent<Animator> ();
		m_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_animator.GetCurrentAnimatorStateInfo (0).IsName ("shoot")) {
			
			if (IsShooting()) {
				m_navMesh.enabled = false;
				m_animator.SetBool ("IsShooting", true);
			}
		}
	}

	bool IsShooting () {
		Vector3 v1 = transform.forward;
		Vector3 v2 = GameStats._FPSMovement.transform.position - transform.position;

		v1.y = v2.y = 0.0f;

		v1.Normalize ();
		v2.Normalize ();

		return (Mathf.Abs (Vector3.Dot (v1, v2) - 1.0f) < m_beginShootingLimit);
	}

	void EnemyFireShootingAnim () {
		Vector3 projectileVel = m_muzzle.forward;
		projectileVel.y = 0;

		// Instantiate projectile.
		GameObject obj = Instantiate (m_projectile, m_muzzle.position, Quaternion.LookRotation(projectileVel));
		obj.GetComponent<Projectile> ().SetVelocity (projectileVel);
		obj.GetComponent<Projectile> ().Enemy = gameObject;

		// Play firing sound.
		m_audioSource.PlayOneShot(m_firingSound);
	}

	void EnemyEndShootingAnim () {
		if (!IsShooting()) {
			m_navMesh.enabled = true;
			m_animator.SetBool ("IsShooting", false);
		}
	}
}

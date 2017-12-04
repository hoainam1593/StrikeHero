using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProperties : MonoBehaviour {

	public int m_maxHealth;
	public AudioClip m_hurtSound;
	public AudioClip m_dieSound;

	public bool IsDead { get; private set; }

	private UICharacterProperties m_UIProperties;
	private GetHurtEffect m_hurtEffect;
	private FPSMovement m_fpsMovement;
	private AudioSource m_audioSource;
	private int m_health;

	void Awake() {
		GameStats._CharacterProperties = this;
	}

	// Use this for initialization
	void Start () {
		m_UIProperties = GameStats._UICharacterProperties;
		m_hurtEffect = GameStats._GetHurtEffect;
		m_fpsMovement = GameStats._FPSMovement;
		m_audioSource = GetComponent<AudioSource> ();
		m_health = m_maxHealth;

		IsDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		m_UIProperties.m_textHealth.text = m_health.ToString ();
	}

	public void Heal (int amount) {
		m_health += amount;
		if (m_health > m_maxHealth) {
			m_health = m_maxHealth;
		}
	}

	public void TakeDamage (int damage) {
		if (!IsDead) {
			// Play hurt effect.
			m_hurtEffect.GetHurt();

			// Decline health.
			m_health -= damage;

			if (m_health <= 0) {
				m_health = 0;
				IsDead = true;

				Die ();
			} else {
				// Play sound.
				m_audioSource.PlayOneShot(m_hurtSound);
			}
		}
	}

	void Die () {
		// Play hurt effect.
		m_hurtEffect.Die ();

		// Play sound.
		m_audioSource.PlayOneShot(m_dieSound);

		// Change to die state.
		m_fpsMovement.ChangeState(new FPSCharacterStateDie());
	}
}

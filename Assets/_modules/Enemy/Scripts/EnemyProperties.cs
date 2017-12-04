using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProperties : MonoBehaviour {

	public int m_damage;
	public int m_health;
	public float m_decayingTime;

	private NavMeshAgent m_navMesh;
	private Animator m_animator;
	private CharacterProperties m_player;

	private bool m_isDead = false;

	// Use this for initialization
	void Start () {
		m_navMesh = GetComponent<NavMeshAgent> ();
		m_animator = GetComponent<Animator> ();
		m_player = GameStats._CharacterProperties;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AttackPlayer () {
		if (!m_isDead) {
			m_player.TakeDamage (m_damage);
		}
	}

	public void TakeDamage (int damage) {
		if (!m_isDead) {
			m_health -= damage;

			if (m_health <= 0) {
				Die ();
			}
		}
	}

	void Die () {
		m_isDead = true;
		m_navMesh.enabled = false;
		m_animator.SetTrigger ("Die");
		Destroy (gameObject, m_decayingTime);
	}
}

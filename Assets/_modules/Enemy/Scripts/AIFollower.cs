using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollower : MonoBehaviour {

	private Transform m_target;
	private NavMeshAgent m_navMesh;
	private Animator m_animator;

	// Use this for initialization
	void Start () {
		m_target = GameStats._FPSMovement.transform;
		m_navMesh = GetComponent<NavMeshAgent> ();
		m_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_navMesh.enabled) {
			// Set target.
			m_navMesh.SetDestination (m_target.position);

			// Set animator's parameters
			m_animator.SetFloat ("DistToTarget", m_navMesh.remainingDistance);
		}
	}

	public void OnAnimatorMove() {
		//m_navMesh.speed = m_walkSpeed * m_animator.GetFloat ("WalkSpeed");
	}
}

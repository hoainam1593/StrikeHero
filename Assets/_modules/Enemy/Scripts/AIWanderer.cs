using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWanderer : MonoBehaviour {

	public float m_revolvingRadius;
	public float m_revolvingSpeed;

	private float m_revolvingAngle = 0.0f;
	private NavMeshAgent m_navMesh;

	// Use this for initialization
	void Start () {
		m_navMesh = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_navMesh.enabled) {
			// Calculate target's position.
			Vector3 center = GameStats._FPSMovement.transform.position;
			Vector3 v = Vector3.forward;

			v = Quaternion.AngleAxis (m_revolvingAngle, Vector3.up) * v;

			Vector3 target = center + v * m_revolvingRadius;

			// Set target for NavMesh agent.
			m_navMesh.SetDestination (target);
		}

		// Accumulate revolving angle.
		m_revolvingAngle += m_revolvingSpeed * Time.deltaTime;
		if (m_revolvingAngle > 360.0f) {
			m_revolvingAngle = 0.0f;
		}
	}
}

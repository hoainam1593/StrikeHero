using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrailController : MonoBehaviour {

	public GameObject m_explosionEffect;

	private ParticleSystem m_particleSystem;
	private List<ParticleCollisionEvent> m_collisionEvents;

	// Use this for initialization
	void Start () {
		m_particleSystem = GetComponent<ParticleSystem> ();
		m_collisionEvents = new List<ParticleCollisionEvent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other) {
		int nEvents = m_particleSystem.GetCollisionEvents (other, m_collisionEvents);

		if (nEvents > 0) {
			Vector3 hitPos = m_collisionEvents [0].intersection;
			Instantiate (m_explosionEffect, hitPos, m_explosionEffect.transform.localRotation);
		}
	}
}

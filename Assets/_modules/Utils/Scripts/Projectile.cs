using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float m_speed;

	private Vector3 m_velocity;

	public GameObject Enemy { get; set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition += m_speed * Time.deltaTime * m_velocity;
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Player")) {
			Enemy.GetComponent<EnemyProperties> ().AttackPlayer ();
		}

		Destroy (gameObject);
	}

	public void SetVelocity (Vector3 vel) {
		m_velocity = vel.normalized;
	}
}

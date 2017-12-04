using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoneCollider : MonoBehaviour {

	public Transform m_bone;
	public Vector3 m_localRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (m_bone != null) {
			transform.position = m_bone.transform.position;
			transform.rotation = m_bone.transform.rotation * Quaternion.Euler(m_localRotation);
		}
	}

	public void GetHit (int damage) {
		EnemyProperties host = transform.parent.parent.GetComponent<EnemyProperties> ();
		host.TakeDamage (damage);
	}
}

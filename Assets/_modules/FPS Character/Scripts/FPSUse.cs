using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSUse : MonoBehaviour {

	public float m_maxDistance;
	public LayerMask m_objectToUse;

	private MessageNotifier m_messageNotifier;

	// Use this for initialization
	void Start () {
		m_messageNotifier = GameStats._MessageNotifier;
	}
	
	// Update is called once per frame
	void Update () {
		Camera camera = Camera.main;
		Vector3 pos = camera.transform.position;
		Vector3 dir = camera.transform.TransformDirection (Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast (pos, dir, out hit, m_maxDistance, m_objectToUse)) {
			UsableObject usableObj = hit.collider.gameObject.GetComponent<UsableObject> ();

			m_messageNotifier.PushFixedMessage (usableObj.GetUseMessage());

			if (MyInput.GetButtonDown ("Use")) {
				usableObj.UseObject ();
			}
		} else {
			m_messageNotifier.PopFixedMessage ();
		}
	}
}

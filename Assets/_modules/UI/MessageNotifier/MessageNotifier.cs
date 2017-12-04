using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageNotifier : MonoBehaviour {

	public GameObject m_fixedMessage;
	public float m_floatMessageTime;
	public float m_messageDistance;

	private float m_baseYPos;
	private float m_messageHeight;

	void Awake() {
		GameStats._MessageNotifier = this;
	}

	// Use this for initialization
	void Start () {
		m_baseYPos = m_fixedMessage.transform.localPosition.y;
		m_messageHeight = m_fixedMessage.GetComponent<Text> ().preferredHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region Fixed message

	public void PushFixedMessage (string message) {
		m_fixedMessage.SetActive (true);
		m_fixedMessage.GetComponent<Text> ().text = message;
	}

	public void PopFixedMessage () {
		m_fixedMessage.SetActive (false);
	}

	#endregion

	#region Float message

	public void PushFloatMessage (string message) {
		
		// Put newest message at bottom location, so move up the others.
		foreach (Transform t in transform) {
			if (t.gameObject != m_fixedMessage) {
				t.localPosition = new Vector3 (
					t.localPosition.x,
					t.localPosition.y + m_messageHeight + m_messageDistance,
					t.localPosition.z
				);
			}
		}

		// Instantiate message.
		GameObject obj = Instantiate (m_fixedMessage);

		// Text component.
		Text text = obj.GetComponent<Text> ();
		text.text = message;

		// Config stuffs.
		obj.transform.SetParent (transform, false);
		obj.SetActive (true);
		Destroy (obj, m_floatMessageTime);

		// Config transformation.
		float y = m_baseYPos;
		//if (m_fixedMessage.activeSelf) {
			y += m_messageHeight + m_messageDistance;
		//}

		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, y, obj.transform.localPosition.z);
	}

	#endregion
}

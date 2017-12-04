using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseLook : MonoBehaviour {

	public Transform m_target;

	public float m_sensitivity;
	public float m_smoothness;
	public float m_minPitchAngle;
	public float m_maxPitchAngle;

	private float m_pitchAngle;
	private float m_yawAngle;

	// Use this for initialization
	void Start () {
		if (m_target == null) {
			m_target = transform;
		}

		m_pitchAngle = m_target.eulerAngles.x;
		m_yawAngle = m_target.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		// Get mouse movement.
		float xMouse = MyInput.GetAxis ("Mouse X");
		float yMouse = MyInput.GetAxis ("Mouse Y");

		// Update pitch and yaw angle.
		m_pitchAngle -= yMouse * m_sensitivity;
		m_yawAngle += xMouse * m_sensitivity;

		m_pitchAngle = Mathf.Clamp (m_pitchAngle, -m_minPitchAngle, m_maxPitchAngle);

		// Rotate to new angle.
		Quaternion rot = Quaternion.Euler(m_pitchAngle, m_yawAngle, 0.0f);
		float t = FPSConstants.MAX_FPS * Time.deltaTime / (m_smoothness + 1.0f);
		//m_target.localRotation = Quaternion.Lerp (m_target.localRotation, rot, t);
		m_target.localRotation = Quaternion.Slerp (m_target.localRotation, rot, t);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput {

	public static bool m_isLock = false;

	public static float GetAxis(string axisName) {
		if (m_isLock) {
			return 0.0f;
		} else {
			return Input.GetAxis (axisName);
		}
	}

	public static bool GetButton (string buttonName) {
		if (m_isLock) {
			return false;
		} else {
			return Input.GetButton (buttonName);
		}
	}

	public static bool GetButtonDown (string buttonName) {
		if (m_isLock) {
			return false;
		} else {
			return Input.GetButtonDown (buttonName);
		}
	}

	public static bool GetButtonUp (string buttonName) {
		if (m_isLock) {
			return false;
		} else {
			return Input.GetButtonUp (buttonName);
		}
	}
}

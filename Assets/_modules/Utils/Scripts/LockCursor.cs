using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursor : MonoBehaviour {

	private static bool m_isLocking;

	public static void Lock() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		m_isLocking = true;
		MyInput.m_isLock = false;
	}

	public static void Unlock() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		m_isLocking = false;
		MyInput.m_isLock = true;
	}

	// Use this for initialization
	void Start () {
		Lock ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("LockCursor")) {
			if (m_isLocking) {
				Unlock ();
			} else {
				Lock ();
			}
		}
	}
}

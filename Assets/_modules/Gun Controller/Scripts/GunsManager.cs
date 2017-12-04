using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunsManager : MonoBehaviour {
	
	void Awake() {
		GameStats._GunsManager = this;
	}

	// Use this for initialization
	void Start () {
		GetActiveGun ().ActiveGun ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public GunController GetGunAt (int idx) {
		if ((idx >= 0) && (idx < transform.childCount)) {
			return transform.GetChild (idx).gameObject.GetComponent<GunController> ();
		}

		return null;
	}

	public GunController GetActiveGun() {
		foreach (Transform gun in transform) {
			if (gun.gameObject.activeSelf) {
				return gun.gameObject.GetComponent<GunController> ();
			}
		}

		return null;
	}

	public void ActivateGun(GunController gunToActivate) {
		// Deactivate current guns.
		GetActiveGun().DeactiveGun();

		// Activate new gun.
		gunToActivate.ActiveGun ();
	}
}

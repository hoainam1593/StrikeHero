using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateAim : IGunState {

	public override void StartState (GunController gun) {
		// Change view.
		gun.TargetView = gun.AimedView;

		// Hide crosshair.
		if (gun._Crosshair != null) {
			gun._Crosshair.gameObject.SetActive (false);
		}
	}

	public override void Update (GunController gun) {
		// To fire state.
		if (MyInput.GetButtonDown ("Fire1")) {
			if (gun.NBullets > 0) {
				gun.AddSecondaryState (new GunStateFire ());
			} else {
				GameStats._MessageNotifier.PushFloatMessage ("Out of bullet");
				GameStats._GunsManager.GetComponent<AudioSource>().PlayOneShot(gun.m_emptyFireSound);
			}
		}

		// End aim.
		if (MyInput.GetButtonUp ("Fire2")) {
			if (gun.IsSecondaryState(this)) {
				gun.RemoveSecondaryState ();
			} else {
				gun.ChangeState (new GunStateIdle ());
			}
		}
	}

	public override void EndState (GunController gun) {
		// Change view.
		gun.TargetView = gun.UnaimedView;

		// Show crosshair.
		if (gun._Crosshair != null) {
			gun._Crosshair.gameObject.SetActive (true);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateIdle : IGunState {
	
	public override void StartState (GunController gun) {
	}

	public override void Update (GunController gun) {
		// To fire state.
		if (MyInput.GetButtonDown ("Fire1") && (gun.m_fireMode != FireMode.Launcher)) {
			if (gun.NBullets > 0) {
				gun.ChangeState (new GunStateFire ());
			} else {
				GameStats._MessageNotifier.PushFloatMessage ("Out of bullet");
				GameStats._GunsManager.GetComponent<AudioSource>().PlayOneShot(gun.m_emptyFireSound);
			}
		}

		// To aim state.
		if (MyInput.GetButtonDown ("Fire2")) {
			gun.ChangeState (new GunStateAim ());
		}

		// To reload state.
		if (MyInput.GetButtonDown ("Reload") && (gun.m_fireMode != FireMode.Launcher)) {
			if (gun.NMagazines > 0) {
				gun.ChangeState (new GunStateReload ());
			} else {
				GameStats._MessageNotifier.PushFloatMessage ("Out of magazine");
				GameStats._GunsManager.GetComponent<AudioSource>().PlayOneShot(gun.m_emptyFireSound);
			}
		}
	}

	public override void EndState (GunController gun) {
	}
}

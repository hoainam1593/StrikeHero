using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateFire : IGunState {

	private bool m_currPlayingFire = false;
	private bool m_prevPlayingFire = false;

	public override void StartState (GunController gun) {
		// Play fire animation.
		if (!gun.m_gunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("fire")) {
			gun.m_gunAnimator.SetTrigger ("fire");
		}

		// Fire bullets.
		gun.StartFireCoroutine();

		// Enlarge crosshair.
		if (gun._Crosshair != null) {
			gun._Crosshair.ChangeInaccuracy (1.0f);
		}
	}

	public override void Update (GunController gun) {
		m_currPlayingFire = gun.m_gunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("fire");

		// To aim state.
		if (MyInput.GetButtonDown ("Fire2")) {
			if (gun.IsSecondaryState (this)) {
				gun.ChangeState (new GunStateAim ());
			} else {
				gun.AddSecondaryState (new GunStateAim ());
			}
		}

		// End fire.
		if (
			((gun.m_fireMode != FireMode.Launcher) && MyInput.GetButtonUp ("Fire1")) ||
			((gun.m_fireMode == FireMode.Launcher) && m_prevPlayingFire && !m_currPlayingFire)
		) {
			if (gun.IsSecondaryState(this)) {
				gun.RemoveSecondaryState ();
			} else {
				gun.ChangeState (new GunStateIdle ());
			}
		}

		m_prevPlayingFire = m_currPlayingFire;
	}

	public override void EndState (GunController gun) {
		// Stop firing bullets.
		if (gun.m_fireMode == FireMode.Auto) {
			gun.StopFireCoroutine ();
		}

		if (gun.m_fireMode == FireMode.Launcher) {
			// Modify ammo.
			if (gun.NMagazines > 0) {
				gun.NBullets = gun.m_bulletCapacity;
				gun.NMagazines--;
			}
		} else {
			// Put crosshair to normal state.
			if (gun._Crosshair != null) {
				gun._Crosshair.ChangeInaccuracy (0.0f);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateReload : IGunState {

	public override void StartState (GunController gun) {
		// Play reload animation.
		gun.m_gunAnimator.SetTrigger ("reload");

		// Hide crosshair.
		if (gun._Crosshair != null) {
			gun._Crosshair.gameObject.SetActive (false);
		}

		// Play sound.
		GameStats._GunsManager.GetComponent<AudioSource>().PlayOneShot(gun.m_reloadSound);
	}

	public override void Update (GunController gun) {
		// End reload.
		if (!gun.m_gunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("reload")) {
			gun.ChangeState (new GunStateIdle ());
		}
	}

	public override void EndState (GunController gun) {
		// Show cross hair.
		if (gun._Crosshair != null) {
			gun._Crosshair.gameObject.SetActive (true);
		}

		// Modify ammo.
		gun.NBullets = gun.m_bulletCapacity;
		gun.NMagazines--;
	}
}

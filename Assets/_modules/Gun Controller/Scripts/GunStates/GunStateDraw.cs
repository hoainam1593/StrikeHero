using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateDraw : IGunState {

	private bool m_currPlayingDraw = false;
	private bool m_prevPlayingDraw = false;
	
	public override void StartState (GunController gun) {
		// Play draw animation.
		if (!gun.m_gunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("draw")) {
			gun.m_gunAnimator.SetTrigger ("draw");
		}

		// Hide crosshair.
		if (gun._Crosshair != null) {
			gun._Crosshair.gameObject.SetActive (false);
		}
	}

	public override void Update (GunController gun) {
		m_currPlayingDraw = gun.m_gunAnimator.GetCurrentAnimatorStateInfo (0).IsName ("draw");

		if (m_prevPlayingDraw && !m_currPlayingDraw) {
			gun.ChangeState (new GunStateIdle ());
		}

		m_prevPlayingDraw = m_currPlayingDraw;
	}

	public override void EndState (GunController gun) {
		// Show cross hair.
		if (gun._Crosshair != null) {
			gun._Crosshair.gameObject.SetActive (true);
		}
	}
}

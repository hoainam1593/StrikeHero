using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterStateDie : IFPSCharacterState {

	private bool m_currPlayingDie = false;
	private bool m_prevPlayingDie = false;

	public void StartState (FPSMovement movement) {
		movement.m_cameraAnimator.SetTrigger ("Die");
		MyInput.m_isLock = true;
	}

	public void Update (FPSMovement movement) {
		m_currPlayingDie = movement.m_cameraAnimator.GetCurrentAnimatorStateInfo (0).IsName ("CameraDie");

		if (m_prevPlayingDie && !m_currPlayingDie) {
			GameStats._MenuButtonsHandler.OnEndGame ();
		}

		m_prevPlayingDie = m_currPlayingDie;
	}

	public void EndState (FPSMovement movement) {
	}
}

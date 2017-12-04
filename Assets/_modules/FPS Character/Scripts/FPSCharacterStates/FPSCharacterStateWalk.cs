using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterStateWalk : IFPSCharacterState {

	public void StartState (FPSMovement movement) {
		GunController activeGun = GameStats._GunsManager.GetActiveGun ();
		Crosshair crosshair = activeGun._Crosshair;

		if (crosshair != null) {
			crosshair.ChangeInaccuracy (0.5f);
		}
	}

	public void Update (FPSMovement movement) {
		// Change to idle state.
		if (movement.GetVelocity () < FPSConstants.BEGIN_WALK_VELOCITY) {
			movement.ChangeState (new FPSCharacterStateIdle ());
		}

		// Change to run state.
		if (MyInput.GetButtonDown ("Sprint")) {
			movement.ChangeState (new FPSCharacterStateRun ());
		}
	}

	public void EndState (FPSMovement movement) {
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterStateIdle : IFPSCharacterState {

	public void StartState (FPSMovement movement) {
		GunController activeGun = GameStats._GunsManager.GetActiveGun ();
		Crosshair crosshair = activeGun._Crosshair;

		if (crosshair != null) {
			crosshair.ChangeInaccuracy (0.0f);
		}
	}

	public void Update (FPSMovement movement) {
		// Change to walk state.
		if (movement.GetVelocity () > FPSConstants.BEGIN_WALK_VELOCITY) {
			movement.ChangeState (new FPSCharacterStateWalk ());
		}
	}

	public void EndState (FPSMovement movement) {
	}
}

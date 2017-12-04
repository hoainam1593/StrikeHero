using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterStateJump : IFPSCharacterState {

	public void StartState (FPSMovement movement) {
		// Raise character to height.
		movement.MoveDirection = new Vector3(movement.MoveDirection.x, movement.m_jumpHeight, movement.MoveDirection.z);

		// Hide crosshair when character is jumping.
		GunController activeGun = GameStats._GunsManager.GetActiveGun ();
		Crosshair crosshair = activeGun._Crosshair;

		if (crosshair != null) {
			crosshair.gameObject.SetActive(false);
		}
	}

	public void Update (FPSMovement movement) {
		// Change to idle state.
		if (movement.m_target.isGrounded) {
			movement.ChangeState (new FPSCharacterStateIdle ());
		}
	}

	public void EndState (FPSMovement movement) {
		// Show crosshair when character land.
		GunController activeGun = GameStats._GunsManager.GetActiveGun ();
		Crosshair crosshair = activeGun._Crosshair;

		if (crosshair != null) {
			crosshair.gameObject.SetActive(true);
		}

		// Play end animation.
		movement.m_cameraAnimator.SetTrigger("Land");
	}
}

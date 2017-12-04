using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFPSCharacterState {

	void StartState (FPSMovement movement);
	void Update (FPSMovement movement);
	void EndState (FPSMovement movement);
}

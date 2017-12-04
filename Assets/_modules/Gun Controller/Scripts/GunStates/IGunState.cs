using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGunState {
	public abstract void StartState (GunController gun);
	public abstract void Update (GunController gun);
	public abstract void EndState (GunController gun);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableGun : UsableObject {

	public string m_gunName;
	public int m_gunIndex;

	// Use this for initialization
	void Start () {
		m_useMessage = "Picks up " + m_gunName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseObject () {
		base.UseObject ();

		GunsManager mgr = GameStats._GunsManager;
		GunController gun = mgr.GetGunAt (m_gunIndex);

		if ((gun != null) && (gun != mgr.GetActiveGun ())) {
			// Use this gun.
			mgr.ActivateGun(gun);

			// Destroy pickup.
			Destroy(gameObject);
		}
	}
}

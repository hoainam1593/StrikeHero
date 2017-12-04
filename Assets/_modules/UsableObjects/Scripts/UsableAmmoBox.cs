using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableAmmoBox : UsableObject {

	public int m_magazineAmount;
	public string m_boxSize;

	// Use this for initialization
	void Start () {
		m_useMessage = "Picks up " + m_boxSize + " ammo box";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseObject () {
		base.UseObject ();

		GameStats._GunsManager.GetActiveGun ().AddMagazines (m_magazineAmount);
		GameStats._MessageNotifier.PushFloatMessage ("+" + m_magazineAmount + " magazines");

		Destroy (gameObject);
	}
}

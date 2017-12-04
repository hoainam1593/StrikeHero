using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableHealthBox : UsableObject {

	public int m_healingValue;

	// Use this for initialization
	void Start () {
		m_useMessage = "Picks up health box";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void UseObject () {
		base.UseObject ();

		GameStats._CharacterProperties.Heal (m_healingValue);
		GameStats._MessageNotifier.PushFloatMessage ("+" + m_healingValue + " HP");

		Destroy (gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterProperties : MonoBehaviour {

	public Text m_textHealth;
	public Text m_textBullets;
	public Text m_textMagazines;

	void Awake() {
		GameStats._UICharacterProperties = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

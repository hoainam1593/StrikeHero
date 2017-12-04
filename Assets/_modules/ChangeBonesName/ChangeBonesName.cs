using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChangeBonesNameMode {
	AddPrefix,
	RemovePrefix
}

public class ChangeBonesName : MonoBehaviour {

	public ChangeBonesNameMode m_mode;
	public string m_prefixToAdd;
	public string m_prefixToRemove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

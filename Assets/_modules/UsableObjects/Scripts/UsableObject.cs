using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableObject : MonoBehaviour {

	public AudioClip m_pickupSound;

	protected string m_useMessage;

	public string GetUseMessage () {
		return m_useMessage;
	}

	public virtual void UseObject () {
		GameStats._CharacterProperties.GetComponent<AudioSource> ().PlayOneShot (m_pickupSound);
	}
}

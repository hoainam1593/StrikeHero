using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, ISelectHandler {

	public AudioClip m_selectSound;

	private AudioSource m_audioSource;
	
	// Use this for initialization
	void Start () {
		m_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSelect(BaseEventData eventData) {
		m_audioSource.clip = m_selectSound;
		m_audioSource.Play ();
	}
}

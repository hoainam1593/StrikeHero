using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHurtEffect : MonoBehaviour {

	[Tooltip("Alpha [0;255] of GetHurtEffect image when get hurt.")]
	public float m_hurtAlpha;
	[Tooltip("Alpha [0;255] of GetHurtEffect image when die.")]
	public float m_dieAlpha;
	public float m_fadeSpeed;

	private Image m_image;
	private bool m_isFading = true;

	void Awake() {
		GameStats._GetHurtEffect = this;
	}

	// Use this for initialization
	void Start () {
		m_image = GetComponent<Image> ();
		SetImageAlpha (0.0f);

		m_hurtAlpha /= 255.0f;
		m_dieAlpha /= 255.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isFading) {
			SetImageAlpha (Mathf.Lerp (m_image.color.a, 0.0f, m_fadeSpeed * Time.deltaTime));
		}
	}

	public void GetHurt () {
		SetImageAlpha (m_hurtAlpha);
	}

	public void Die () {
		SetImageAlpha (m_dieAlpha);
		m_isFading = false;
	}

	void SetImageAlpha (float a) {
		m_image.color = new Color (m_image.color.r, m_image.color.g, m_image.color.b, a);
	}
}

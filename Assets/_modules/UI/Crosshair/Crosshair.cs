using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CrosshairType {
	Normal,
	Launcher
}

public class Crosshair : MonoBehaviour {

	public CrosshairType m_type = CrosshairType.Normal;
	public float m_inaccuracyScale;
	public float m_changeSizeSpeed;
	public Vector2 m_centerOffset;
	public Color m_aimEnemyColor;

	private Transform m_left, m_right, m_up, m_down;
	private Vector3 m_baseLeft, m_baseRight, m_baseUp, m_baseDown;

	private float m_inaccuracy = 0.0f;
	private float m_inaccuracyTarget = 0.0f;

	private Color m_originalColor;

	// Use this for initialization
	void Start () {
		if (m_type == CrosshairType.Normal) {
			m_left = transform.Find ("left");
			m_right = transform.Find ("right");
			m_up = transform.Find ("up");
			m_down = transform.Find ("down");

			m_baseLeft = m_left.localPosition;
			m_baseRight = m_right.localPosition;
			m_baseUp = m_up.localPosition;
			m_baseDown = m_down.localPosition;

			m_originalColor = m_left.GetComponent<Image> ().color;
		}
	}

	// Update is called once per frame
	void Update () {
		if (m_type == CrosshairType.Normal) {
			// Set inaccuracy smoothly to inaccuracy target.
			m_inaccuracy = Mathf.Lerp (m_inaccuracy, m_inaccuracyTarget, m_changeSizeSpeed * Time.deltaTime);

			// Change size of crosshair basing on inaccuracy.
			float t = m_inaccuracyScale * m_inaccuracy;
			Vector3 offset = new Vector3 (m_centerOffset.x, m_centerOffset.y, 0.0f);

			m_left.localPosition = offset + m_baseLeft + new Vector3 (-t, 0, 0);
			m_right.localPosition = offset + m_baseRight + new Vector3 (t, 0, 0);
			m_up.localPosition = offset + m_baseUp + new Vector3 (0, t, 0);
			m_down.localPosition = offset + m_baseDown + new Vector3 (0, -t, 0);
		}

	}

	/// <summary>
	/// Please pass inaccuracy in [0; 1].
	/// </summary>
	/// <param name="normalizedInaccuracy">Normalized inaccuracy.</param>
	public void ChangeInaccuracy(float normalizedInaccuracy) {
		if (m_type == CrosshairType.Normal) {
			m_inaccuracyTarget = Mathf.Clamp (normalizedInaccuracy, 0.0f, 1.0f);
		}
	}

	void ChangeColor(Transform image, Color color) {
		if (image != null) {
			image.gameObject.GetComponent<Image>().color = color;
		}
	}

	public void AimEnemy () {
		if (m_type == CrosshairType.Normal) {
			ChangeColor (m_left, m_aimEnemyColor);
			ChangeColor (m_right, m_aimEnemyColor);
			ChangeColor (m_up, m_aimEnemyColor);
			ChangeColor (m_down, m_aimEnemyColor);
		}
	}

	public void ResetColor () {
		if (m_type == CrosshairType.Normal) {
			ChangeColor (m_left, m_originalColor);
			ChangeColor (m_right, m_originalColor);
			ChangeColor (m_up, m_originalColor);
			ChangeColor (m_down, m_originalColor);
		}
	}
}

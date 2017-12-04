using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DecalType {
	Stone,
	Flesh,
	Metal,
	None
}

[System.Serializable]
public class DecalInfo {
	public DecalType m_type;
	public GameObject m_decal;
	public float m_minScale;
	public float m_maxScale;
}

public class DecalsManager : MonoBehaviour {

	public DecalInfo[] m_decals;

	void Awake() {
		GameStats._DecalsManager = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	DecalInfo GetDecalInfo(string name) {
		DecalType type;

		// Determine type basing on name.
		switch (name) {
		case "Stone":
			type = DecalType.Stone;
			break;
		case "Flesh":
			type = DecalType.Flesh;
			break;
		case "Metal":
			type = DecalType.Metal;
			break;
		default:
			type = DecalType.None;
			break;
		}

		// Get decal object.
		foreach (DecalInfo info in m_decals) {
			if (info.m_type == type) {
				return info;
			}
		}

		return null;
	}

	public void SpawnDecal(RaycastHit hit) {
		DecalInfo decalInfo = GetDecalInfo (hit.collider.tag);
		Vector3 pos = hit.point;
		Quaternion rot = Quaternion.LookRotation (hit.normal);

		if (decalInfo != null) {
			float scale = Random.Range (decalInfo.m_minScale, decalInfo.m_maxScale);

			GameObject decalObject = GameObject.Instantiate (decalInfo.m_decal, pos, rot);
			decalObject.transform.localScale = new Vector3 (scale, scale, scale);
			decalObject.transform.SetParent (hit.collider.transform);
		}
	}
}

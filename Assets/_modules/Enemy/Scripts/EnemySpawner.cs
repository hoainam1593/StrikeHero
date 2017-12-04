using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Transform m_spawnLocationsParent;
	public GameObject m_enemy;
	public float m_spawningRate;
	public int m_maxEnemiesNumber;

	// Use this for initialization
	void Start () {
		StartCoroutine ("SpawnCoroutine");
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator SpawnCoroutine() {
		while (true) {
			SpawnAnEnemy ();

			yield return new WaitForSeconds (m_spawningRate);
		}
	}

	void SpawnAnEnemy () {
		if (transform.childCount < m_maxEnemiesNumber) {
			// Random a location.
			int i = Random.Range(0, m_spawnLocationsParent.childCount);
			Vector3 loc = m_spawnLocationsParent.GetChild (i).position;

			// Instantiate enemy.
			Vector3 enemyLoc = new Vector3(loc.x, m_enemy.transform.localPosition.y, loc.z);
			Instantiate (m_enemy, enemyLoc, m_enemy.transform.localRotation, transform);
		}
	}
}

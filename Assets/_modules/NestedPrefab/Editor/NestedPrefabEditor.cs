using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NestedPrefab))]
public class NestedPrefabEditor : Editor {
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		if (GUILayout.Button ("Apply Prefab")) {
			IterateAllObjects ();
		}
	}

	void IterateAllObjects() {
		NestedPrefab prefab = (NestedPrefab)target;

		// Get all game objects in scene.
		GameObject[] objs = GameObject.FindObjectsOfType<GameObject> ();

		// Iterate all over them.
		foreach (GameObject obj in objs) {
			if ((obj != null) && obj.name.StartsWith (prefab.name)) {
				if (!prefab.m_isUpdateChildren) {
					// Just update components of game object.
					RemoveComponentsFromObject (obj);
					CopyComponentsFromPrefab (obj);
					CopyTagFromPrefab (obj);
				} else {
					// Substitute an instance of prefab for game object in the scene.
					ChangeGameObject (obj);
				}
			}
		}
	}

	#region Update components of game object.

	void RemoveComponentsFromObject(GameObject obj) {
		Component[] components = obj.GetComponents<Component> ();
		foreach (Component component in components) {
			if (component.GetType () != typeof(Transform)) {
				DestroyImmediate (component);
			}
		}
	}

	void CopyComponentFromPrefab(Component component, GameObject obj) {
		var compType = component.GetType ();
		Component compInObj = obj.AddComponent (compType);

		EditorUtility.CopySerialized (component, compInObj);
	}

	void CopyComponentsFromPrefab(GameObject obj) {
		NestedPrefab prefab = (NestedPrefab)target;

		Component[] components = prefab.GetComponents<Component> ();
		foreach (Component component in components) {
			if ((component.GetType () != typeof(Transform)) && (component.GetType () != typeof(NestedPrefab))) {
				CopyComponentFromPrefab (component, obj);
			}
		}
	}

	void CopyTagFromPrefab(GameObject obj) {
		NestedPrefab prefab = (NestedPrefab)target;
		obj.tag = prefab.tag;
	}

	#endregion

	#region Remove old game object and clone new one

	void ChangeGameObject(GameObject obj) {
		Transform parent = obj.transform.parent;
		string name = obj.name;
		Vector3 pos = obj.transform.localPosition;
		Quaternion rot = obj.transform.localRotation;
		Vector3 scale = obj.transform.localScale;

		NestedPrefab prefab = (NestedPrefab)target;

		DestroyImmediate (obj);
		GameObject newObj = Instantiate (prefab.gameObject);

		newObj.name = name;
		newObj.transform.parent = parent;
		newObj.transform.localPosition = pos;
		newObj.transform.localRotation = rot;
		newObj.transform.localScale = scale;

		// Remove redundant component.
		DestroyImmediate(newObj.GetComponent<NestedPrefab>());
	}

	#endregion


}
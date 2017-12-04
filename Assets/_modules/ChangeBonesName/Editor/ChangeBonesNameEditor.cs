using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeBonesName))]
public class ChangeBonesNameEditor : Editor {

	SerializedProperty m_mode, m_prefixToAdd, m_prefixToRemove;

	void OnEnable () {
		m_mode = serializedObject.FindProperty ("m_mode");
		m_prefixToAdd = serializedObject.FindProperty ("m_prefixToAdd");
		m_prefixToRemove = serializedObject.FindProperty ("m_prefixToRemove");
	}
	
	public override void OnInspectorGUI() {

		// Draw script.
		EditorGUILayout.ObjectField(
			"Script", 
			MonoScript.FromMonoBehaviour((ChangeBonesName)target), 
			typeof(ChangeBonesName), 
			false);

		// Draw target's properties.
		serializedObject.Update ();

		EditorGUILayout.PropertyField (m_mode);

		if ((target as ChangeBonesName).m_mode == ChangeBonesNameMode.AddPrefix) {
			EditorGUILayout.PropertyField (m_prefixToAdd);
		}

		if ((target as ChangeBonesName).m_mode == ChangeBonesNameMode.RemovePrefix) {
			EditorGUILayout.PropertyField (m_prefixToRemove);
		}

		serializedObject.ApplyModifiedProperties ();

		// Draw action button.
		if (GUILayout.Button ("Change Names")) {
			OnChangeNamesClicked ();
		}
	}

	void OnChangeNamesClicked () {
		ProcessBone ((target as ChangeBonesName).gameObject);
	}

	void ProcessBone (GameObject bone) {
		if (bone == null) {
			return;
		}

		// Process bone's name.
		bone.name = ProcessBoneName(bone.name);

		// Recursively process current bone's children.
		foreach (Transform t in bone.transform) {
			ProcessBone (t.gameObject);
		}
	}

	string ProcessBoneName (string name) {
		ChangeBonesName obj = (ChangeBonesName)target;

		if (obj.m_mode == ChangeBonesNameMode.AddPrefix) {
			return obj.m_prefixToAdd + name;
		}

		if (obj.m_mode == ChangeBonesNameMode.RemovePrefix) {
			string strToRemove = obj.m_prefixToRemove;

			int i;
			for (i = 0; i < strToRemove.Length; i++) {
				if (strToRemove [i] != name [i]) {
					break;
				}
			}

			if (i == strToRemove.Length) {
				return name.Substring (i);
			}
		}

		return name;
	}
}

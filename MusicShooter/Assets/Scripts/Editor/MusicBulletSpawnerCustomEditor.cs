using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MusicBulletSpawnerController))]
public class MusicBulletSpawnerCustomEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MusicBulletSpawnerController myScript = (MusicBulletSpawnerController)target;
		if (GUILayout.Button("Build Spawners"))
		{
			myScript.BuildObjects();
		}
	}
} 

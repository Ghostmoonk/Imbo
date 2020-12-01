using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollower))]
public class CameraFollowerEditor : Editor
{
    SerializedProperty cameraLockedAxis;
    CameraFollower cameraFollower;

    private void OnEnable()
    {
        cameraFollower = (CameraFollower)target;
        cameraLockedAxis = serializedObject.FindProperty("lockedAxis");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        cameraLockedAxis.GetArrayElementAtIndex(0).boolValue = EditorGUILayout.Toggle("x", cameraLockedAxis.GetArrayElementAtIndex(0).boolValue);
        cameraLockedAxis.GetArrayElementAtIndex(1).boolValue = EditorGUILayout.Toggle("y", cameraLockedAxis.GetArrayElementAtIndex(1).boolValue);
        cameraLockedAxis.GetArrayElementAtIndex(2).boolValue = EditorGUILayout.Toggle("z", cameraLockedAxis.GetArrayElementAtIndex(2).boolValue);

        serializedObject.ApplyModifiedProperties();
    }
}

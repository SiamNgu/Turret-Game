using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BackgroundManager))]
public class BackgroundManagerEditor : Editor
{
    private BackgroundManager backgroundManager;

    private void OnEnable()
    {
        backgroundManager = (BackgroundManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (backgroundManager.backgroundData.backgroundsArray.Length > 0)
        {
            string[] backgroundKeys = new string[backgroundManager.backgroundData.backgroundsArray.Length];

            for (int i = 0; i < backgroundKeys.Length; i++)
            {
                backgroundKeys[i] = backgroundManager.backgroundData.backgroundsArray[i].name;
            }
            backgroundManager.selectedBackground = EditorGUILayout.Popup("Background Name", backgroundManager.selectedBackground, backgroundKeys);
        }
        else
        {
            EditorGUILayout.LabelField("Backgrounds Data or Dictionary is null.");
        }
        if (GUILayout.Button("Set Background")) backgroundManager.SetBackground();
    }
}

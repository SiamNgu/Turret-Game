using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MyButtonScript))]
public class MyButtonScriptEditor : Editor
{
    private string[] sceneNames;

    private void OnEnable()
    {
        RefreshSceneNames();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var buttonScript = (MyButtonScript)target;
        switch (buttonScript.clickAction)
        {
            case MyButtonScript.ButtonAction.LoadScene:
                if (buttonScript.clickAction == MyButtonScript.ButtonAction.Quit) { return; }
                buttonScript.selectedSceneIndex = EditorGUILayout.Popup("Select Scene", buttonScript.selectedSceneIndex, sceneNames);
                if (GUI.changed) buttonScript.sceneToLoad = sceneNames[buttonScript.selectedSceneIndex];
                break;

            case MyButtonScript.ButtonAction.Quit:
                break;
        }
    }

    private void RefreshSceneNames()
    {
        // Get all scene names from EditorBuildSettings
        sceneNames = EditorBuildSettings.scenes.Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path)).ToArray();
    }
}

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PreprocessDeleteObjectsFromScenes : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log(report.summary.platform + " build.");
        int sceneCount = EditorSceneManager.sceneCount;
        bool isOnMobile = report.summary.platform == UnityEditor.BuildTarget.Android || report.summary.platform == UnityEditor.BuildTarget.iOS;
        for (int i = 0; i < sceneCount; i++)
        {
            var scene = EditorSceneManager.GetSceneByBuildIndex(i);
            EditorSceneManager.OpenScene(scene.path);

            PlatformSpecificFeature[] toDestroy = Object.FindObjectsOfType<PlatformSpecificFeature>();
            foreach (PlatformSpecificFeature obj in toDestroy)
            {
                Behaviour component = obj.GetComponent<PlatformSpecificFeature>();
                Debug.Log(component.GetType());
                if (System.Convert.ToString(component.GetType()) == "MobileFeature")
                {
                    
                    if (isOnMobile)
                    {
                        obj.tag = "Untagged";
                    }
                    else
                    {
                        obj.tag = "EditorOnly";

                    }
                }
                else if (System.Convert.ToString(component.GetType()) == "PCFeature")
                {
                    if (isOnMobile)
                    {
                        obj.tag = "EditorOnly";
                    }
                    else
                    {
                        obj.tag = "Untagged";

                    }
                }
            }

        }
        EditorSceneManager.MarkAllScenesDirty();
        EditorSceneManager.SaveOpenScenes();
    }
    
}
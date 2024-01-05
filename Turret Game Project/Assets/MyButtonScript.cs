using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MyButtonScript : MonoBehaviour
{
    public ButtonAction clickAction;
    [HideInInspector] public int selectedSceneIndex;
    [HideInInspector] public string sceneToLoad;
    public enum ButtonAction
    {
        LoadScene,
        Quit
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        switch (clickAction)
        {
            case ButtonAction.LoadScene:
                SceneManager.LoadScene(sceneToLoad);
                break;
            case ButtonAction.Quit:
                Application.Quit();
                break;
        }
    }
}

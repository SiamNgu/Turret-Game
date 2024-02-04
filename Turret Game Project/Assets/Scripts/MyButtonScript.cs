using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MyButtonScript : MonoBehaviour
{
    public ButtonAction clickAction;
    [HideInInspector] public int selectedSceneIndex;
    [HideInInspector] public string sceneToLoad;
    [HideInInspector] public GameObject panel;
    public enum ButtonAction
    {
        LoadScene,
        Quit,
        TogglePanel,
        None
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
            case ButtonAction.TogglePanel:
                panel.SetActive(!panel.activeSelf);
                break;
            case ButtonAction.None: break;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MyButtonScript : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}

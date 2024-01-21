using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PostGameTitleScript : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.EnterPostGameEvent += OnEnterPostGame;
    }
    private void OnEnterPostGame(string winner)
    {
        GetComponent<TMP_Text>().text = "Game Over\n" + winner + " has won the game";
    }
}

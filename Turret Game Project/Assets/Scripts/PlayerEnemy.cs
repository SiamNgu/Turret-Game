using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEnemy : PlayerBase
{
    protected override GameManager.PlayerUIReferences uiReferences { get; set; }
    void Start()
    {
        uiReferences = GameManager.Instance.invaderUIReferences;
        GameManager.Instance.inputMaster._1V1.Invader.performed += ctx => Shoot();
    }

    float currentRotationTimer = 0;
    protected override void Orbit()
    {
        int direction = (right ? 1 : -1);

        currentRotationTimer += Time.deltaTime * direction;

        float aspect = (float)Screen.width / Screen.height;

        float screenMaxHeight = Camera.main.orthographicSize;

        float screenMaxWidth = screenMaxHeight * aspect;

        float x = Mathf.Sin(currentRotationTimer * profile.mobility * 0.01f) * screenMaxWidth * 0.8f;
        float y = Mathf.Cos(currentRotationTimer * profile.mobility * 0.01f) * screenMaxHeight * 0.8f;

        transform.position = new Vector2(x, y);// Calculate the angle of the enemy to point at the turret in the position (0; 0)
        float angle = Mathf.Atan2(-transform.position.y, -transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    
}

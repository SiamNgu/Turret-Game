using UnityEngine;

public class PlayerEnemy : PlayerBase
{
    protected override string other { get; set; } = "Defender";

    void Start()
    {
        #if UNITY_ANDROID || UNITY_IOS
            GameManager.Instance.inputMaster._1V1.Invader.performed += ctx => TouchscreenShoot(ctx.ReadValue<float>());
        #else
            GameManager.Instance.inputMaster._1V1.Invader.performed += ctx => Shoot();
        #endif

    }

    #if UNITY_ANDROID
        void TouchscreenShoot(float x)
        {
            if (x / Screen.width > 0.5f)
            {
                Shoot();
            }
        }
    #endif

    float currentRotationTimer = 0;
    protected override void Orbit()
    {
        int direction = (right ? 1 : -1);

        currentRotationTimer += Time.deltaTime * direction;

        float aspect = (float)Screen.width / Screen.height;

        float screenMaxHeight = Camera.main.orthographicSize;

        float screenMaxWidth = screenMaxHeight * aspect;

        float x = Mathf.Sin(currentRotationTimer * profile.mobility * 0.02f) * screenMaxWidth * 0.8f;
        float y = Mathf.Cos(currentRotationTimer * profile.mobility * 0.02f) * screenMaxHeight * 0.8f;

        transform.position = new Vector2(x, y);// Calculate the angle of the enemy to point at the turret in the position (0; 0)
        float angle = Mathf.Atan2(-transform.position.y, -transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    
}

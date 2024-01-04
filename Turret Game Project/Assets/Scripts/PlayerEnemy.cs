using UnityEngine;

public class PlayerEnemy : PlayerBase
{
    [SerializeField] private float height;
    [SerializeField] private float width;

    private float currentRotationTimer;

    protected override void Awake()
    {
        base.Awake();
        playerControls.Turret.ReleaseEnemy.performed += ctx => Shoot();
    }

    protected override void Rotate()
    {
        // Get mobility of gun profile and get a value acceptable by the sine and cosine function.
        float mobility = (profile.mobility / 100f);
        int direction = (right ? 1 : -1);

        currentRotationTimer += Time.deltaTime * direction;

        // Calculate and sets the x and y position of the enemy transform with trigonometric functions and with the width and height parameters of the ellipsis
        float x = Mathf.Sin(currentRotationTimer * mobility) * width;
        float y = Mathf.Cos(currentRotationTimer * mobility) * height;
        transform.position = new Vector2(x, y);

        // Calculate the angle of the enemy to point at the turret in the position (0; 0)
        float angle = Mathf.Atan2(-transform.position.y, -transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    
}

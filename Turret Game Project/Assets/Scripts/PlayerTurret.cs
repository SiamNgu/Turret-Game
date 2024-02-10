using UnityEngine;

public class PlayerTurret : PlayerBase
{
    protected override string other { get; set; } = "Invader";
    void Start()
    {
        #if UNITY_ANDROID || UNITY_IOS
            GameManager.Instance.inputMaster._1V1.Defender.performed += ctx => TouchscreenShoot(ctx.ReadValue<float>());
        #else
            GameManager.Instance.inputMaster._1V1.Defender.performed += ctx => Shoot();
        #endif

    }

    #if UNITY_ANDROID
        void TouchscreenShoot(float x)
        {
            if (x / Screen.width < 0.5f)
            {
                Shoot();
            }
        }
    #endif

    protected override void Orbit()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * profile.mobility * (right ? 1 : -1) * shootSlowdown);
    }
}
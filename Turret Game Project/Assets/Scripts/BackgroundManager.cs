using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private GameObject starPrefab;
    public int numberOfStars = 1;

    [SerializeField] private float speed;

    private float screenWidth;
    private float screenHeight;

    [SerializeField] private Transform starOrigin;

    private Transform[] stars;

    [SerializeField] private float starDistance= 10f;
    [SerializeField] private Vector2 sunOffset = new Vector2(10, 10);


    void Start()
    {
        screenWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        screenHeight = Camera.main.orthographicSize * 2f;
        SpawnObjects();
        AssignPos();
    }

    private void Update()
    {
        starOrigin.Rotate(Vector3.up * Time.deltaTime * speed);
    }

    void AssignPos()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            float p = (i * 1f / numberOfStars * 1f);
            Vector3 spawnPosition = new Vector3(
                Camera.main.transform.position.x + Mathf.Cos(p * 360) * starDistance,
                Random.Range(-screenHeight / 2, screenHeight / 2),
                Camera.main.transform.position.z + Mathf.Sin(p * 360) * starDistance
                );
            stars[i].position = spawnPosition;
            // Get the direction from the star to the camera but only in the XZ plane
            Vector3 direction = Camera.main.transform.position - stars[i].position;
            direction.y = 0f; // Set the Y component to 0 to ignore vertical difference

            stars[i].rotation = Quaternion.LookRotation( direction);
        }
    }

    void SpawnObjects()
    {
        stars = new Transform[numberOfStars];
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = Instantiate(starPrefab,starOrigin).transform;
        }
        Instantiate(sunPrefab, new Vector2(-screenWidth/2, screenHeight/2) + sunOffset, Quaternion.LookRotation(Vector2.zero));
    }
}

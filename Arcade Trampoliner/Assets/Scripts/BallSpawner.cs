using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] float spawnRate = 0.2f;
    [SerializeField] float spawnRateRamp = 0.05f;

    [SerializeField] GameObject pointSystem;
    [SerializeField] GameObject crowdSystem;
    [SerializeField] GameObject lifeCounter;

    [Header("Ball Spawning")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float spawnX = -8;
    [SerializeField] float randomSpawnYMin = -8;
    [SerializeField] float randomSpawnYMax = 4;
    [SerializeField] float screenTopY = 8;
    [SerializeField] float horizontalPeakMin = -5;
    [SerializeField] float horizontalPeakMax = 0;
    [SerializeField] float timeToPeak = 1;

    float timer = 0;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3, true);
        timer = 1 / spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 1/spawnRate)
        {
            timer = 0;
            SpawnBall();
        }

        timer += Time.deltaTime;
    }

    // When called, add to spawn rate
    public void AddSpawnRate()
    {
        spawnRate += spawnRateRamp;
    }

    // Spawn ball with applied spawn settings
    void SpawnBall()
    {
        float spawnY = Random.Range(randomSpawnYMin, randomSpawnYMax);
        float horizontalPeak = Random.Range(horizontalPeakMin, horizontalPeakMax);

        float launchVelX = (horizontalPeak - spawnX) / timeToPeak;
        float launchVelY = Mathf.Sqrt(2 * -Physics2D.gravity.y * (screenTopY - spawnY));

        GameObject newBall = Instantiate(ballPrefab, new Vector2(spawnX, spawnY), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(launchVelX, launchVelY);

        newBall.GetComponent<Basketball>().SetReferences(new GameObject[] { gameObject, pointSystem, crowdSystem, lifeCounter });
    }
}

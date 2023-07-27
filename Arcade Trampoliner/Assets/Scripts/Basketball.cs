using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    [SerializeField] float spawnImmunityTime = 0.15f;
    [SerializeField] float despawnLevel = -15;
    [SerializeField] float nearMissCooldown = 0.2f;
    [SerializeField] float minLaunchSpeed = 2;
    [SerializeField] float maxLaunchSpeed = 15;

    PointSystem pointSystem;
    CrowdSystem crowdSystem;
    BallSpawner ballSpawner;
    LifeCounter lifeCounter;

    float immunityTimer = 0;
    bool hasSpawned = false;
    float scoredHeight = 10;
    float nearMissCounter = 0;
    AdvancedMusicPlayer ballBounce;

    // Start is called before the first frame update
    void Start()
    {
        ballBounce = GetComponent<AdvancedMusicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enable collision after some time since spawn
        if (immunityTimer > spawnImmunityTime && hasSpawned == false)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            hasSpawned = true;
        }
        else if (hasSpawned == false) { immunityTimer += Time.deltaTime; }

        // Despawn ball. Lose a life if it wasn't scored. Make crowd disappointed if it was a Near ball
        if (transform.position.y <= despawnLevel)
        {
            if (!CompareTag("Scored"))
            {
                lifeCounter.ReduceLife();
            }
            if (CompareTag("Near"))
            {
                crowdSystem.EndSuspense(false);
            }
            Destroy(gameObject);
        }

        // If ball scored, fade out ball
        if (CompareTag("Scored"))
        {
            ChangeAlpha(scoredHeight, despawnLevel, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Launch ball from rebound lines with ballBounce sound
        if (other.CompareTag("Player"))
        {
            float launchSpeed = GetComponent<Rigidbody2D>().velocity.magnitude * other.GetComponent<Rebound>().launchForce;
            if (launchSpeed > maxLaunchSpeed)
            {
                launchSpeed = maxLaunchSpeed;
            }
            else if (launchSpeed < minLaunchSpeed)
            {
                launchSpeed = minLaunchSpeed;
            }

            GetComponent<Rigidbody2D>().velocity = launchSpeed * other.transform.up;
            other.GetComponent<Rebound>().DespawnRebound();
            ballBounce.PlayRandom();
        }
        // Score ball if entering Bucket trigger. If it was a Near ball, also let crowd cheer.
        else if (other.CompareTag("Bucket"))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            scoredHeight = transform.position.y;
            if (CompareTag("Near"))
            {
                crowdSystem.EndSuspense(true);
            }
            tag = "Scored";
            pointSystem.GetComponent<PointSystem>().IncrementPoint();
            ballSpawner.AddSpawnRate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When leaving Rim trigger, mark ball as Near
        if (other.transform.CompareTag("Rim"))
        {
            nearMissCounter += Time.deltaTime;

            if (nearMissCounter >= nearMissCooldown) 
            {
                tag = "Near";
                crowdSystem.CrowdGasp(true);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballBounce.PlayRandom();
    }

    // Set object references to prefab ball
    public void SetReferences(GameObject[] objects)
    {
        ballSpawner = objects[0].GetComponent<BallSpawner>();
        pointSystem = objects[1].GetComponent<PointSystem>();
        crowdSystem = objects[2].GetComponent<CrowdSystem>();
        lifeCounter = objects[3].GetComponent<LifeCounter>();
    }

    // Change Alpha of gameObject sprite based on start, end and current points
    void ChangeAlpha(float posStart, float posEnd, float posCurr)
    {
        Color currColor = GetComponentInChildren<SpriteRenderer>().color;
        float alphaRatio = Mathf.Abs(posEnd - posCurr) / Mathf.Abs(posEnd - posStart);

        GetComponentInChildren<SpriteRenderer>().color = new Color(currColor.r, currColor.g, currColor.b, alphaRatio);
    }
}

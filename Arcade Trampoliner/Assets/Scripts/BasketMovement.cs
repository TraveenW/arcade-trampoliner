using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    [SerializeField] int scoreInterval;
    [SerializeField] int maxScore;

    [Header("Basket Movement")]
    [SerializeField] float movementStepUp;
    [SerializeField] float maxDrop;

    [Header("Sound Step Ups")]
    [SerializeField] float bgTrackPitchIncrease = 0.1f;
    [SerializeField] GameObject BGTrack;
    [SerializeField] GameObject BGCrowd;
    [SerializeField] float cheerChance = 0.5f;

    float moveIncrement = 0;
    float moveMultiplier = 0;
    float startHeight;
    float currentHeight;

    AdvancedMusicPlayer basketSound;

    // Start is called before the first frame update
    void Start()
    {
        basketSound = GetComponent<AdvancedMusicPlayer>();
        startHeight = transform.position.y;
    }

    private void FixedUpdate()
    {
        // Move basket depending on moveIncrement. Movement is in a Cosine wave pattern
        moveIncrement += Time.fixedDeltaTime * moveMultiplier;
        currentHeight = startHeight + maxDrop * (Mathf.Cos(moveIncrement) - 1);
        transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
    }

    // Called by PointSystem to play a sound when scoring a point. If scoreInterval is passed, also increase basket's movement amount
    public void BasketReact(int currPoint)
    {
        basketSound.PlayRandom();

        if (currPoint % scoreInterval == 0 && currPoint > 0 && currPoint <= maxScore)
        {
            moveMultiplier += movementStepUp;
            if (Random.Range(0, 1) < cheerChance)
            {
                BGCrowd.GetComponent<AdvancedMusicPlayer>().PlayByIndex(1);
            }

            BGTrack.GetComponent<AudioSource>().pitch += bgTrackPitchIncrease;
            BGCrowd.GetComponent<CrowdSystem>().CrowdBGStepUp();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] GameObject bgCrowd;
    [SerializeField] GameObject bgTrack;

    int lifeAmount = 0;

    List<Transform> hearts = new List<Transform>();
    AudioSource bgCrowdSound;
    AudioSource bgTrackSound;

    // Start is called before the first frame update
    void Start()
    {
        bgCrowdSound = bgCrowd.GetComponent<AudioSource>();
        bgTrackSound = bgTrack.GetComponent<AudioSource>();

        foreach (Transform c in transform)
        {
            hearts.Add(c);
            lifeAmount++;
        }
    }

    // Called by Basketball when it falls off. Reduce lifeAmount by 1 and remove one of the hearts
    // At 1 life, the crowd gasps. At 0 life, crowd sighs and game over starts
    public void ReduceLife()
    {
        lifeAmount--;
        GetComponent<AudioSource>().Play();

        Destroy(hearts[0].gameObject);
        hearts.RemoveAt(0);

        if (lifeAmount == 0)
        {
            bgCrowdSound.Stop();
            bgTrackSound.Stop();
            gameOverScreen.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] GameObject crowdSounds;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] GameObject bgCrowd;
    [SerializeField] GameObject bgTrack;

    int lifeAmount = 0;
    int crowdGaspIndex;

    List<GameObject> hearts = new List<GameObject>();
    AudioSource bgCrowdSound;
    AudioSource bgTrackSound;

    // Start is called before the first frame update
    void Start()
    {
        crowdGaspIndex = crowdSounds.GetComponent<CrowdSystem>().gaspIndex;
        bgCrowdSound = bgCrowd.GetComponent<AudioSource>();
        bgTrackSound = bgTrack.GetComponent<AudioSource>();

        foreach (GameObject c in transform)
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
        Destroy(hearts[0]);
        hearts.RemoveAt(0);

        if (lifeAmount == 1)
        {
            crowdSounds.GetComponent<AdvancedMusicPlayer>().PlayByIndex(crowdGaspIndex);
        }
        if (lifeAmount == 0)
        {
            bgCrowdSound.Stop();
            bgTrackSound.Stop();
            gameOverScreen.SetActive(true);
        }
    }
}

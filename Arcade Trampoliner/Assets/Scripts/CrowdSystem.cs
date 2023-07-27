using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{

    [SerializeField] GameObject crowdBG;

    [SerializeField] float startVolume = 0;
    [SerializeField] float crowdStepUp = 0.2f;
    [SerializeField] float suspenseMultiplier = 0.2f;

    [Header("Audio Clip List Indices")]
    [SerializeField] int gaspIndex = 0;
    [SerializeField] int cheerIndex = 1;
    [SerializeField] int sighIndex = 2;

    AdvancedMusicPlayer crowdNoiseSource;
    AudioSource crowdBGSource;
    SpriteRenderer crowdBGLights;

    float crowdBGVolumeStore;
    bool suspenseState = false;

    // Update BGCrowd's volume using stored volume. If suspenseState is true, will also add in the suspenseMultiplier
    void UpdateBGVolume(float newVolume)
    {
        crowdBGVolumeStore = newVolume;

        if (suspenseState)
        {
            crowdBGSource.volume = crowdBGVolumeStore * suspenseMultiplier;
        }
        else
        {
            crowdBGSource.volume = crowdBGVolumeStore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        crowdNoiseSource = GetComponent<AdvancedMusicPlayer>();
        crowdBGSource = crowdBG.GetComponent<AudioSource>();

        UpdateBGVolume(startVolume);
    }

    // Increase volume of the background crowd and opacity of the lights
    public void CrowdBGStepUp()
    {
        float crowdLightsAlpha = crowdBGLights.color.a;

        UpdateBGVolume(crowdBGVolumeStore + crowdStepUp);
        crowdBGLights.color = new Color(1, 1, 1, crowdLightsAlpha + crowdStepUp);
    }

    // Call for gasp sound. If isSuspense is true, also reduce crowdBG volume temporarily
    public void CrowdGasp(bool isSuspense = false)
    {
        crowdNoiseSource.PlayByIndex(gaspIndex);

        if (isSuspense && suspenseState == false)
        {
            suspenseState = true;
            UpdateBGVolume(crowdBGVolumeStore);
        }
    }

    // Makes crowd sigh or cheer depending on hasScored. If isSuspense is true, also reset the BG crowd noises
    public void EndSuspense(bool hasScored)
    {
        if (hasScored)
        {
            crowdNoiseSource.PlayByIndex(cheerIndex);
        }
        else
        {
            crowdNoiseSource.PlayByIndex(sighIndex);
        }

        if (suspenseState == true)
        {
            suspenseState = false;
            UpdateBGVolume(crowdBGVolumeStore);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedMusicPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClipList = new List<AudioClip>();
    [Range(0f, 1f)]
    [SerializeField] float volume = 1f;
    [SerializeField] int antiRepeatClipPeriod = 1;

    List<int> playedClips = new List<int>();
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Play a specific sound from the list by index
    public void PlayByIndex(int clipIndex)
    {
        if (clipIndex < audioClipList.Count)
        {
            audioSource.PlayOneShot(audioClipList[clipIndex], volume);
        }
        else
        {
            Debug.Log("ERROR: Clip list does not contain clip with this index: " + clipIndex);
        }
    }

    // Play a random sound from the list. Also has anti repeat functionality
    public void PlayRandom()
    {
        bool isUnique = false;
        int clipIndex;

        do
        {
            clipIndex = Random.Range(0, audioClipList.Count);

            // Check if sound exists in playedClips list
            // Skips check if playedClips equals or exceeds list of clips for any reason
            if (playedClips.Count >= audioClipList.Count)
            {
                isUnique = true;
            }
            else if (playedClips.Contains(clipIndex) == false)
            {
                isUnique = true;
            }
        }
        while (isUnique == false);
        
        audioSource.PlayOneShot(audioClipList[clipIndex], volume);
        playedClips.Add(clipIndex);
        if (playedClips.Count > antiRepeatClipPeriod)
        {
            playedClips.RemoveAt(0);
        }
    }

    // Play a completely random sound
    public void PlayCompleteRandom()
    {
        audioSource.PlayOneShot(audioClipList[Random.Range(0, audioClipList.Count)], volume);
    }
}

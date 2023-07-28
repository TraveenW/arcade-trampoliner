using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] AudioClip countdown;
    [SerializeField] float startDelay = 3.3f;

    [SerializeField] GameObject bgTrack;
    [SerializeField] GameObject bgCrowd;
    
    bool initialIntro = true;
    UIImageFade transitionFade;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(countdown);
        transitionFade = gameObject.GetComponent<UIImageFade>();
        if (initialIntro)
        {
            initialIntro = false;
            transitionFade.FadeImage(1, 0);
            StartCoroutine(StartGame());
        }
    }

    // When called, fade to black and reload scene
    public void ReloadScene()
    {
        transitionFade = gameObject.GetComponent<UIImageFade>();
        transitionFade.FadeImage(0, 1);
        StartCoroutine(StartReload());
    }

    // Delay playing the looping tracks for an amount of time
    IEnumerator StartGame()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(startDelay);
        Time.timeScale = 1;

        bgTrack.GetComponent<AudioSource>().Play();
        bgCrowd.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }

    // Load back Start Menu
    IEnumerator StartReload()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / transitionFade.fadeDuration)
        {
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
}

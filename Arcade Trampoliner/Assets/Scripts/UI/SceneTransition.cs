using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    bool initialIntro = true;
    UIImageFade transitionFade;

    // Start is called before the first frame update
    void Start()
    {
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
        transitionFade.FadeImage(1);
        StartCoroutine(StartReload());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(transitionFade.fadeDuration);
        gameObject.SetActive(false);
    }

    IEnumerator StartReload()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / transitionFade.fadeDuration)
        {
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

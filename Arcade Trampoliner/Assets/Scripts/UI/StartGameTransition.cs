using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTransition : MonoBehaviour
{
    UIImageFade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponent<UIImageFade>();
        fade.FadeImage(0, 1);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(fade.fadeDuration);
        SceneManager.LoadScene("MainGame");
    }
}

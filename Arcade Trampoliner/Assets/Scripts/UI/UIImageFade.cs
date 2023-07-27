using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageFade : MonoBehaviour
{
    public float fadeDuration = 1.3f;

    Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Called by other scripts to fade image to a new alpha value
    // Input: The goal alpha amount
    public void FadeImage(float endAlpha)
    {
        image = gameObject.GetComponent<Image>();
        float startAlpha = image.color.a;
        StartCoroutine(ChangeAlpha(startAlpha, endAlpha));
    }
    // Called by other scripts to fade image between two different alpha values
    // Input startAlpha: The starting alpha amount
    // Input endAlpha: The goal alpha amount
    public void FadeImage(float startAlpha, float endAlpha)
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(ChangeAlpha(startAlpha, endAlpha));
    }


    // Fade UI element over unscaled time
    IEnumerator ChangeAlpha(float start, float end)
    {
        float t = 0;
        do
        {
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(start, end, t));
            image.color = newColor;
            t += Time.unscaledDeltaTime / fadeDuration;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        } while (t < 1.0f);
    }
}

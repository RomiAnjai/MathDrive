using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 1f;

    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color color = image.color;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            color.a = alpha;
            image.color = color;

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }
}

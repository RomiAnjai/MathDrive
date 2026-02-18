using UnityEngine;
using System.Collections;

public class WinPopupAnimation : MonoBehaviour
{
    public RectTransform panel;
    public float duration = 0.6f;
    public float bounceAmount = 40f;
    public float posisiAkhirUI;

    private Vector2 startPos;
    private Vector2 targetPos;

    void Start()
    {
        targetPos = new Vector2(0, posisiAkhirUI);
        startPos = new Vector2(0, -950);
        panel.anchoredPosition = startPos;
    }

    public void PlayAnimation()
    {
        StartCoroutine(AnimatePopup());
    }

    IEnumerator AnimatePopup()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / duration;

            // Ease Out Cubic
            float ease = 1 - Mathf.Pow(1 - t, 3);

            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, ease);
            yield return null;
        }

        // Bounce kecil
        yield return StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        float time = 0;
        float bounceDuration = 0.2f;

        Vector2 overshoot = targetPos + Vector2.up * bounceAmount;

        while (time < bounceDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / bounceDuration;

            panel.anchoredPosition = Vector2.Lerp(targetPos, overshoot, t);
            yield return null;
        }

        time = 0;
        while (time < bounceDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / bounceDuration;

            panel.anchoredPosition = Vector2.Lerp(overshoot, targetPos, t);
            yield return null;
        }

        panel.anchoredPosition = targetPos;
    }
}

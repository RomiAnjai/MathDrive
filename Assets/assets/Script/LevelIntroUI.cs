using UnityEngine;
using System.Collections;

public class LevelIntroUI : MonoBehaviour
{
    public RectTransform panelLevel;
    public float slideSpeed = 800f;
    public float holdTime = 3f;

    private Vector2 startPos;
    private Vector2 centerPos;
    private Vector2 endPos;

    void Start()
    {
        startPos = new Vector2(0, -115f);

        centerPos = new Vector2(0, 0f);

        endPos = new Vector2(0, -115f);

        panelLevel.anchoredPosition = startPos;

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSecondsRealtime(1f);
        yield return StartCoroutine(SlideTo(centerPos));

        yield return new WaitForSecondsRealtime(holdTime);

        yield return StartCoroutine(SlideTo(endPos));
    }

    IEnumerator SlideTo(Vector2 target)
    {
        while (Vector2.Distance(panelLevel.anchoredPosition, target) > 1f)
        {
            panelLevel.anchoredPosition = Vector2.MoveTowards(
                panelLevel.anchoredPosition,
                target,
                slideSpeed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        panelLevel.anchoredPosition = target;
    }
}

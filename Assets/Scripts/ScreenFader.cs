using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;

    public IEnumerator FadeOutIn(System.Action afterFade)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // Fade to black
        afterFade?.Invoke();                       // Do world switch
        yield return StartCoroutine(Fade(1f, 0f)); // Fade back in
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            fadeCanvasGroup.alpha = alpha;
            timer += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = to;
    }
}

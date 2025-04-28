using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;

    public IEnumerator FadeOutIn(System.Action afterFade)
    {
        afterFade?.Invoke();                       // Do world switch
        yield break;
    }

    private IEnumerator Fade(float from, float to)
    {
        yield break;
    }
}

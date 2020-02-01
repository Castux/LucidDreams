using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float Speed = 0.05f;

    public void StartFadeOut(System.Action callback)
    {
        StartCoroutine(Fade(callback));
    }

    private IEnumerator Fade(System.Action callback)
    {
        var group = GetComponent<CanvasGroup>();

        for (float f = 1f; f >= 0; f -= Speed)
        {
            group.alpha = f;
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
        group.alpha = 1f;

        callback();
    }
}

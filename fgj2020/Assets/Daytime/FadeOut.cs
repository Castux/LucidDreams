using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public enum Direction
    {
        FadeIn,
        FadeOut
    }

    public float Speed = 0.05f;

    public void StartFadeOut(Direction direction, System.Action callback = null)
    {
        StartCoroutine(Fade(direction, callback));
    }

    private IEnumerator Fade(Direction direction, System.Action callback = null)
    {
        var group = GetComponent<CanvasGroup>();

        if (direction == Direction.FadeOut)
        {

            for (float f = 1f; f >= 0; f -= Speed)
            {
                group.alpha = f;
                yield return new WaitForEndOfFrame();
            }

            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            for (float f = 0f; f <= 1f; f += Speed)
            {
                group.alpha = f;
                yield return new WaitForEndOfFrame();
            }
        }

        group.alpha = 1f;

        callback?.Invoke();
    }
}

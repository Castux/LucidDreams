using UnityEngine;
using UnityEngine.UI;

public class Glitcher : MonoBehaviour
{
    public float Probability = 0.01f;
    public float ScaleRange = 0.05f;

    public float GlitchProbFactor = 1f;

    public bool UseCanvasGroup = false;

    private Image image;

    private bool fadingToDark;
    private bool fadingToLight;
    private float fadeTimer;
    public float fadeTime;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (fadingToDark)
        {
            if (fadeTimer < fadeTime)
            {
                fadeTimer += Time.deltaTime;
                var group = GetComponent<CanvasGroup>();
                group.alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeTime);

            }
            else
            {
                var group = GetComponent<CanvasGroup>();
                group.alpha = 0f;

                if (group.alpha == 0f)
                    Probability *= GlitchProbFactor;
                else
                    Probability /= GlitchProbFactor;

                fadeTimer = 0f;
                fadingToDark = false;
            }
        }
        else if (fadingToLight)
        {
            if (fadeTimer < fadeTime)
            {
                fadeTimer += Time.deltaTime;
                var group = GetComponent<CanvasGroup>();
                group.alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeTime);

            }
            else
            {
                var group = GetComponent<CanvasGroup>();
                group.alpha = 1f;

                if (group.alpha == 0f)
                    Probability *= GlitchProbFactor;
                else
                    Probability /= GlitchProbFactor;

                fadeTimer = 0f;
                fadingToLight = false;
            }
        }
        else
        {
            if (Random.Range(0f, 1f) < Probability)
            {
                transform.localScale = Vector3.one + Vector3.one * Random.Range(-ScaleRange, ScaleRange);
            }

            if (Random.Range(0f, 1f) < Probability)
            {
                if (UseCanvasGroup)
                {
                    var group = GetComponent<CanvasGroup>();
                    if (group.alpha == 0f)
                    {
                        fadingToLight = true;
                    }
                    else
                    {
                        fadingToDark = true;
                    }

                    // if (group.alpha == 0f)
                    //     Probability *= GlitchProbFactor;
                    // else
                    //     Probability /= GlitchProbFactor;
                }
                else
                {
                    var nextColor = Color.white;
                    nextColor.a = (image.color.a == 1f) ? 0f : 1f;

                    image.color = nextColor;

                    if (nextColor.a == 0f)
                        Probability *= GlitchProbFactor;
                    else
                        Probability /= GlitchProbFactor;
                }
            }
        }
    }
}

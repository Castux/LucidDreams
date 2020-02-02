using UnityEngine;
using UnityEngine.UI;

public class Glitcher : MonoBehaviour
{
    public float Probability = 0.01f;
    public float ScaleRange = 0.05f;

    public bool UseCanvasGroup = false;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(Random.Range(0f, 1f) < Probability)
        {
            transform.localScale = Vector3.one + Vector3.one * Random.Range(-ScaleRange, ScaleRange);
        }

        if (Random.Range(0f, 1f) < Probability)
        {
            if (UseCanvasGroup)
            {
                var group = GetComponent<CanvasGroup>();
                group.alpha = (group.alpha == 1f) ? 0f : 1f;
            }
            else
            {
                var nextColor = Color.white;
                nextColor.a = (image.color.a == 1f) ? 0f : 1f;

                image.color = nextColor;
            }
        }
    }
}

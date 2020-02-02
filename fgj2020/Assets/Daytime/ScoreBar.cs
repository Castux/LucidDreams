using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    public GameObject Star;

    private PlayerProgression playerProg;

    void Start()
    {
        playerProg = FindObjectOfType<PlayerProgression>();
    }

    void Update()
    {
        var progress = 0f;

        if (playerProg != null)
            progress = (float)playerProg.TotalPoints / PlayerProgression.MaxPoints;

        var rect = Star.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, progress);
        rect.anchorMax = new Vector2(0.5f, progress);
        rect.anchoredPosition = new Vector2(0f, 0f);

        GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

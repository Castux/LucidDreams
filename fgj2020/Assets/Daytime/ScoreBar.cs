using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    public GameObject Star;
    public float Speed = 5f;

    private PlayerProgression playerProg;

    private float currentProgress = 0f;

    void Start()
    {
        playerProg = FindObjectOfType<PlayerProgression>();

        if (playerProg != null)
            currentProgress = (float)playerProg.TotalPoints / PlayerProgression.MaxPoints;
    }

    void Update()
    {
        var targetProgress = 0f;

        if (playerProg != null)
            targetProgress = (float)playerProg.TotalPoints / PlayerProgression.MaxPoints;

        currentProgress += (targetProgress - currentProgress) * Speed * Time.deltaTime;

        var rect = Star.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, currentProgress);
        rect.anchorMax = new Vector2(0.5f, currentProgress);
        rect.anchoredPosition = new Vector2(0f, 0f);

        GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
}

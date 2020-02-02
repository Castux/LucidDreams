using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to store player progression including collected clues, day-time choices and problems solved
/// </summary>
public class PlayerProgression : MonoBehaviour
{
    public static PlayerProgression Instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }
            else
            {
                _Instance = FindObjectOfType<PlayerProgression>();
                if (_Instance == null)
                {
                    Debug.LogError("No PlayerProgression instance in scene");
                }

                return _Instance;
            }
        }
    }

    private static PlayerProgression _Instance = null;

    private List<Clue> collectedClues;
    public int TotalPoints;
    public List<Problem> problems;
    public List<Problem> solvedProblems;

    public const int MaxPoints = 100;
    public const int GoodEnding = 75;
    public const int MediumEnding = 50;

    public AudioSource audioSource;
    public AudioClip positiveScore;
    public AudioClip negativeScore;

    private void Awake()
    {
        if (_Instance != this)
        {
            if (_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        Init();
    }

    private void Init()
    {
        DontDestroyOnLoad(this);
        collectedClues = new List<Clue>();
        solvedProblems = new List<Problem>();
        TotalPoints = 25;
    }

    public void SwitchToDayTime()
    {
        SceneManager.LoadScene("DayTime");
    }

    public void SwitchToDream()
    {
        SceneManager.LoadScene("DreamWorld");
    }

    public void SolveProblem(Problem problem)
    {
        solvedProblems.Add(problem);
    }

    public List<string> GetCollectedClueTexts()
    {
        List<string> clueTexts = new List<string>();
        foreach(Clue clue in collectedClues)
        {
            clueTexts.Add(clue.clueText);
        }

        return clueTexts;
    }

    public void AddClue(Clue clue)
    {
        if (!collectedClues.Contains(clue))
        {
            collectedClues.Add(clue);
        }
    }

    public void ModifyPoints(int points)
    {
        TotalPoints += points;

        if (TotalPoints < 0)
            TotalPoints = 0;

        if (TotalPoints > MaxPoints)
            TotalPoints = MaxPoints;

        if (points > 0)
		{
            audioSource.clip = positiveScore;
            audioSource.Play();
		}
        else
		{
            audioSource.clip = negativeScore;
            audioSource.Play();
        }
    }
}

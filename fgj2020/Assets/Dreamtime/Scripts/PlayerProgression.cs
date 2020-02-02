using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to store player progression including collected clues, day-time choices and problems solved
/// </summary>
public class PlayerProgression : MonoBehaviour
{
    private List<Clue> collectedClues;
    public int TotalPoints;
    public List<Problem> problems;
    public List<Problem> solvedProblems;

    public const int MaxPoints = 100;
    public const int GoodEnding = 75;
    public const int MediumEnding = 50;

    private void Awake()
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
    }
}

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
    private int totalPoints;
    public List<Problem> problems;
    private List<Problem> solvedProblems;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        collectedClues = new List<Clue>();
        solvedProblems = new List<Problem>();
        totalPoints = 25;
    }

    public void SwitchToDayTime()
    {
        SceneManager.LoadScene("DayTime");

        FindObjectOfType<OpenNotebook>().SetProblems(problems, solvedProblems);
    }

    public void SwitchToDream()
    {
        SceneManager.LoadScene("DreamScene");
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
        totalPoints += points;
        // TODO: update UI
    }
}

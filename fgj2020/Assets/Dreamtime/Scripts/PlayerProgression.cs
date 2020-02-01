using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store player progression including collected clues, day-time choices and problems solved
/// </summary>
public class PlayerProgression : MonoBehaviour
{
    private List<Clue> collectedClues;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        collectedClues = new List<Clue>();
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
}

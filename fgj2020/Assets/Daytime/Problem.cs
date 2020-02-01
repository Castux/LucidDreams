using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Problem", fileName = "Problem")]
public class Problem : ScriptableObject
{
    public string problemTitle;

    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public int points;
}

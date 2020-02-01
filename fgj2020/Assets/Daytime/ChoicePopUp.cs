using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoicePopUp : MonoBehaviour
{
    public GameObject uiParent;
    public TextMeshProUGUI problemTitleText;
    public List<Button> choiceButtons;

    public Button goToDreamButton;

    private void Awake()
    {
        goToDreamButton.onClick.AddListener(GoToDreamButtonPress);
        ClearChoices();
    }

    private void GoToDreamButtonPress()
    {
        goToDreamButton.gameObject.SetActive(false);

        Invoke("GoToDream", 2f);
    }

    void GoToDream()
    {
        FindObjectOfType<PlayerProgression>().SwitchToDream();
    }

    public void OpenChoicePopUp(Problem problem)
    {
        uiParent.SetActive(true);
        problemTitleText.SetText(problem.problemTitle);
        foreach(Button choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        for (int i = 0; i < problem.choices.Count; i++)
        {
            var tmp = i;

            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(problem.choices[i].choiceText);
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => ChooseChoice(problem, problem.choices[tmp].points));
        }
    }

    private void ChooseChoice(Problem problem, int points)
    {
        FindObjectOfType<PlayerProgression>().ModifyPoints(points);
        FindObjectOfType<PlayerProgression>().SolveProblem(problem);

        ClearChoices();

        goToDreamButton.gameObject.SetActive(true);
    }

    private void ClearChoices()
    {
        problemTitleText.SetText("");
        foreach(Button choiceButton in choiceButtons)
        {
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().SetText("");
            choiceButton.gameObject.SetActive(false);
        }
        
        uiParent.SetActive(false);
    }
}

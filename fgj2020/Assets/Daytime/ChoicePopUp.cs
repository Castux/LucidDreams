using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoicePopUp : MonoBehaviour
{
    public TextMeshProUGUI problemTitleText;
    public List<Button> choiceButtons;

    public OpenNotebook notebook;
    public GameObject EndingsContainer;
    public GameObject[] Endings;
    public GameObject EndingButton;

    public Button goToDreamButton;
    public FadeOut backCoverFader;

    private void Awake()
    {
        ClearChoices();
    }

    public void GoToDreamButtonPress()
    {
        goToDreamButton.gameObject.SetActive(false);
        notebook.GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            backCoverFader.gameObject.SetActive(true);
            backCoverFader.StartFadeOut(FadeOut.Direction.FadeIn, () =>
            {
                Invoke("GoToDream", 2f);
            });
        });
    }

    private void GoToDream()
    {
        FindObjectOfType<PlayerProgression>().SwitchToDream();
    }

    public void OpenChoicePopUp(Problem problem)
    {
        gameObject.SetActive(true);
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
        notebook.LeftArrow.SetActive(false);

        // End condition check!

        var prog = FindObjectOfType<PlayerProgression>();

        if (prog.problems.Count == prog.solvedProblems.Count)
        {
            DisplayEndings();
        }
        else
        {
            goToDreamButton.gameObject.SetActive(true);
        }
    }

    private void ClearChoices()
    {
        problemTitleText.SetText("");
        foreach(Button choiceButton in choiceButtons)
        {
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().SetText("");
            choiceButton.gameObject.SetActive(false);
        }
    }

    private void DisplayEndings()
    {
        EndingsContainer.SetActive(true);

        var prog = FindObjectOfType<PlayerProgression>();

        int endingIndex;

        if (prog.TotalPoints >= PlayerProgression.GoodEnding)
            endingIndex = 0;
        else if (prog.TotalPoints >= PlayerProgression.MediumEnding)
            endingIndex = 1;
        else
            endingIndex = 2;

        Endings[endingIndex].SetActive(true);
        EndingButton.SetActive(true);

        DestroyImmediate(prog.gameObject);
    }

    public void OnEndingButtonClicked()
    {
        notebook.GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            SceneManager.LoadScene("TitleScreen");
        });
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenNotebook : MonoBehaviour
{
    public GameObject LeftArrow;
    public GameObject RightArrow;

    public GameObject ProblemsPage;
    public GameObject CluesContainer;
    public GameObject ClueTextPrefab;

    public GameObject NotebookCover;

    public int CluesPerSpread = 12;

    private int currentPage;
    private List<string> clues;

    public List<Button> problemButtons;

    public void Start()
    {
        var playerProg = FindObjectOfType<PlayerProgression>();
        SetProblems(playerProg.problems, playerProg.solvedProblems);
    }

    private void SetProblems(List<Problem> problems, List<Problem> solvedProblems)
    {
        foreach(Button button in problemButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < problems.Count && i < problemButtons.Count; i++)
        {
            problemButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(problems[i].problemTitle);
            if (solvedProblems.Contains(problems[i]))
            {
                problemButtons[i].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;

                continue;
            }

            var tmp = i;

            problemButtons[i].gameObject.SetActive(true);
            problemButtons[i].onClick.AddListener(() =>
            {
                OpenChoiceUI(problems[tmp]);
            });
        }
    }

    void OpenChoiceUI(Problem problem)
    {
        FindObjectOfType<ChoicePopUp>().OpenChoicePopUp(problem);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        currentPage = -1;
        Populate(new List<string>());

        UpdatePageContent();
    }

    public void FlipPage(int direction)
    {
        currentPage += direction;

        if (currentPage < -1)
        {
            Close();
        }
        else
        {
            UpdatePageContent();
        }
    }

    private void Populate(List<string> clues)
    {
        this.clues = clues;

        clues.Clear();

        for (int i = 0; i < 20; i++)
            clues.Add("This is a super clue. With it you'll figure out why your life sucks so much. It's sad really. But true. " + i);
    }

    private void UpdatePageContent()
    {
        if (currentPage == -1)
        {
            ProblemsPage.SetActive(true);
            CluesContainer.SetActive(false);

            UpdateProblemsPage();
        }
        else
        {
            ProblemsPage.SetActive(false);
            CluesContainer.SetActive(true);

            UpdateCluesOnPage();
        }
    }

    private void UpdateProblemsPage()
    {

    }

    private void UpdateCluesOnPage()
    {
        var startIndex = currentPage * CluesPerSpread;
        var endIndex = Mathf.Min((currentPage + 1) * CluesPerSpread, clues.Count) - 1;

        foreach (Transform child in CluesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = startIndex; i <= endIndex; i++)
        {
            var clue = Instantiate(ClueTextPrefab);
            clue.transform.SetParent(CluesContainer.transform);
            clue.transform.localScale = Vector3.one;

            var textComp = clue.GetComponentInChildren<Text>();
            textComp.text = clues[i];
            textComp.alignment = (i - startIndex <= 5) ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
        }

        RightArrow.SetActive(endIndex < clues.Count - 1);
    }

    private void Close()
    {
        GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            NotebookCover.SetActive(true);
        });
    }
}
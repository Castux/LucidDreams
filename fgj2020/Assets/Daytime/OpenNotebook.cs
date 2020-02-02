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

    public ChoicePopUp choicePopUp;

    public GameObject NotebookCover;

    public Clue[] TestClues;

    public int CluesPerSpread = 8;

    private int currentPage;
    private List<string> clues = new List<string>();

    public List<Button> problemButtons;

    private void SetProblems(List<Problem> problems, List<Problem> solvedProblems)
    {
        foreach (Button button in problemButtons)
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
                ProblemsPage.SetActive(false);
                RightArrow.SetActive(false);
                choicePopUp.OpenChoicePopUp(problems[tmp]);
            });
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);

        var playerProg = FindObjectOfType<PlayerProgression>();

        if (playerProg != null)
        {
            SetProblems(playerProg.problems, playerProg.solvedProblems);
            clues = playerProg.GetCollectedClueTexts();
        }
        else
        {
            // Debug clues!
            clues.Clear();
            foreach(var clue in TestClues)
                clues.Add(clue.clueText);
        }

        currentPage = 0;

        UpdatePageContent();
    }

    public void FlipPage(int direction)
    {
        if (choicePopUp.gameObject.activeSelf)
        {
            choicePopUp.gameObject.SetActive(false);
            UpdatePageContent();

            return;
        }

        currentPage += direction;

        if (currentPage < 0)
        {
            Close();
        }
        else
        {
            UpdatePageContent();
        }
    }

    private void UpdatePageContent()
    {
        if (currentPage == (clues.Count + CluesPerSpread - 1) / CluesPerSpread)
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
        RightArrow.SetActive(false);
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
            textComp.alignment = (i - startIndex < CluesPerSpread / 2) ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
        }

        RightArrow.SetActive(true);
    }

    private void Close()
    {
        GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            NotebookCover.SetActive(true);
        });
    }
}
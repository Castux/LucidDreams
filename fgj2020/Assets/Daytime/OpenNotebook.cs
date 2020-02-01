using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNotebook : MonoBehaviour
{
    public GameObject LeftPage;
    public GameObject RightPage;

    public GameObject LeftArrow;
    public GameObject RightArrow;

    public GameObject NotebookCover;

    private int currentPage;

    public void Open()
    {
        gameObject.SetActive(true);
        currentPage = 0;
    }

    public void FlipPage(int direction)
    {
        currentPage += direction;

        if (currentPage < 0)
        {
            Close();
        }
    }

    private void Close()
    {
        GetComponent<FadeOut>().StartFadeOut(() =>
        {
            NotebookCover.SetActive(true);
        });
    }
}
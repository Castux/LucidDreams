using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookCover : MonoBehaviour
{
    public OpenNotebook OpenNotebook;

    public void Start()
    {
        GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeIn, () => { });
    }

    public void OpenBook()
    {
        GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            OpenNotebook.Open();
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookCover : MonoBehaviour
{
    public OpenNotebook OpenNotebook;

    public void OpenBook()
    {
        GetComponent<FadeOut>().StartFadeOut(() =>
        {
            OpenNotebook.Open();
        });
    }
}

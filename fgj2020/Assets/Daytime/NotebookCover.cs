using UnityEngine;

public class NotebookCover : MonoBehaviour
{
    public OpenNotebook OpenNotebook;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeIn);
    }

    public void OpenBook()
    {
        GetComponent<FadeOut>().StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            OpenNotebook.Open();
        });
    }
}

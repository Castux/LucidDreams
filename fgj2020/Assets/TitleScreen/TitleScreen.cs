using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public FadeOut fader;
    public FadeOut creditsFader;

    public void OnPlayClicked()
    {
        fader.StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            SceneManager.LoadScene("DreamWorld");
        });
    }

    public void OnCreditsClicked()
    {
        fader.StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            creditsFader.gameObject.SetActive(true);
            creditsFader.StartFadeOut(FadeOut.Direction.FadeIn);
        });
    }

    public void OnQuitClicked()
    {
        fader.StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            Application.Quit();
        });
    }

    public void OnBackFromCreditsClicked()
    {
        creditsFader.StartFadeOut(FadeOut.Direction.FadeOut, () =>
        {
            fader.gameObject.SetActive(true);
            fader.StartFadeOut(FadeOut.Direction.FadeIn);
        });
    }
}

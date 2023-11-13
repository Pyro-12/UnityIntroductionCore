
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        ButtonClick();
        SceneManager.LoadScene("InGame");

    } 
    public void Options()
    {
        ButtonClick();
        SceneManager.LoadScene("Options");

    }

    public void BackMainMenu()
    {
        ButtonClick();
        SceneManager.LoadScene("Intro");
    }

    public void ExitGame()
    {
        ButtonClick();
        Application.Quit();
    }
    public void ButtonClick()
    {
       // SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
    }
}

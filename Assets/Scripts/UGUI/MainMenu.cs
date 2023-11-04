using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        ButtonClick();

    }

    public void BackMainMenu()
    {

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

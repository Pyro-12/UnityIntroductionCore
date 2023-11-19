
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }
    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void NewGame()
    {
        ButtonClick();
        //SceneManager.LoadScene("InGame");
        //GameManager.Instance.StartGame();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .GameSceneName);

    } 
    public void Options()
    {
        ButtonClick();
       // SceneManager.LoadScene("Options"); //OLD METHOD
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
           .OptionsSceneName);

    }

    public void BackMainMenu()
    {
        ButtonClick();
        // SceneManager.LoadScene("Intro"); //OLD METHOD without bootstrap
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
           .DefaultMainMenuSceneName);
    }

    public void ExitGame()
    {
        ButtonClick();
        Application.Quit();
    }
    public void ButtonClick()
    {
        //SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position); //->ver por que no instancia el sonido
    }
}

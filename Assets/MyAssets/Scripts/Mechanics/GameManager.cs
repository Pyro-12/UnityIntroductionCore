using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    Coroutine _coroutineGameTimer;

    private bool isPaused;
    public bool IsPaused => isPaused;
    #region InitData

    #region Events data
    public delegate void PauseGame(bool paused);

    public static event PauseGame OnPauseGame;

    public delegate void PlayerDeath();

    public event PlayerDeath OnDeath;

    public delegate void GameOver();

    public static event GameOver OnGameOver;

    public delegate void WinGame();

    public event WinGame OnWinGame;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        RestartGameSession();
        SubscribeToDelegatesAndUpdateValues();
    }

    void SubscribeToDelegatesAndUpdateValues()
    {
        OnPauseGame += StopGame;
        OnGameOver += RestartGameSession;
    }

    void UnsubscribeToDelegates()
    {
        OnPauseGame -= StopGame;
        OnGameOver -= RestartGameSession;
    }

    #endregion
        public void StartGame()
    {
        RestartGameSession();
    }
    public void RestartGameSession()
    {
        Instance.isPaused = false;
        Time.timeScale = 1;
    }
    public void ExitGameSession()
    {
        RestartGameSession();
        StopHandleGameOver();
    }

    public void StopGame(bool paused)
    {
        if (paused)
        {
            StopHandleGameOver();
        }
       
    }

    public void StopHandleGameOver()
    {
        if (Instance._coroutineGameTimer != null)
        {
            StopAllCoroutines();
            Instance.StopAllCoroutines();
        }
    }

    public void PauseGameEvent(bool paused)
    {
        Instance.isPaused = !Instance.isPaused;
        if (Instance.isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        OnPauseGame?.Invoke(Instance.isPaused);
    }

    public void OnNewGame()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        //RestartGameEvent();
    }

    public void OnExitMenu()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        ExitGameSession();
    }

  /* public void OnCredits()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .CreditsSceneName);
    }
  */
}

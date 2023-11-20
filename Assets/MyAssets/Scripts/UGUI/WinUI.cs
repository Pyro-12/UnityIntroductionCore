using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    GameManager gameManager;
    [Header("UI win game")]
    [SerializeField] GameObject _winPanel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WinUI Start");
        _winPanel.SetActive(false);
        gameManager = GameManager.Instance;
        SubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        Debug.Log("Subscribing to events");
        gameManager.OnWinGame += ShowWinGameUI;
    }

    void ShowWinGameUI()
    {
        Debug.Log("ShowWinGameUI() called");
        _winPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    void UnsubscribeFromEvents()
    {
        gameManager.OnWinGame -= ShowWinGameUI;
    }
}
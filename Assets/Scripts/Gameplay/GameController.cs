using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    public static GameController Instance;

    public Action HitTheZone;
    public Action Error;

    private void OnEnable()
    {
        Instance = this;
        Error += EndGame;
    }

    private void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    public void ReloadScene()
    {
        GameOverPanel.SetActive(false);
        ChangeScene.Instance.ChangeSceneTransition("Bounce-Gameplay"); 
    }
    
    public void GoToMenu()
    {
        GameOverPanel.SetActive(false);
        ChangeScene.Instance.ChangeSceneTransition("Bounce-Menu");
    }

    IEnumerator EndGameCoroutine()
    {
        yield return ChangeScene.Instance.StartGameOver();
        GameOverPanel.SetActive(true);
    }
    
}

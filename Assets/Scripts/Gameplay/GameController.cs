using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverUI GameOverPanel;
    [SerializeField] private GameObject PausePanel;
    public static GameController Instance;

    public Action HitTheZone;
    public Action Error;

    private void OnEnable()
    {
        Instance = this;
        Error += EndGame;
    }

    private async void EndGame()
    {
        await ChangeScene.Instance.StartGameOver();
        GameOverPanel.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        AudioManager.Instance.PlayAudio(Sounds.UIClick);
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        AudioManager.Instance.PlayAudio(Sounds.UIClick);
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayAudio(Sounds.UIClick);
        GameOverPanel.gameObject.SetActive(false);
        ChangeScene.Instance.ChangeSceneTransition("Bounce-Gameplay"); 
    }
    
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayAudio(Sounds.UIClick);
        GameOverPanel.gameObject.SetActive(false);
        ChangeScene.Instance.ChangeSceneTransition("Bounce-Menu");
    }


    public void OnScoreResult(int score, int maxScore)
    {
        if(score > maxScore) {
            GameOverPanel.SetNewBestScore(score);
        }
        else
        {
            GameOverPanel.SetNewScore(score, maxScore);
        }
    }
    
}

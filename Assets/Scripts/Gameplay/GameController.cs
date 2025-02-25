using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverUI GameOverPanel;
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

    public void ReloadScene()
    {
        AudioManager.Instance.PlayAudio(Sounds.UIClick);
        GameOverPanel.gameObject.SetActive(false);
        ChangeScene.Instance.ChangeSceneTransition("Bounce-Gameplay"); 
    }
    
    public void GoToMenu()
    {
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

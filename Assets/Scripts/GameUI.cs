using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI volumeValueText;
    public TextMeshProUGUI playModeButtonText;

    public Action onStartGame;

    public void Awake()
    {
        AdjustPlayButtonText();
    }

    public void UpdateScore(int scorePlayer1, int scorePlayer2)
    {
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighLightScore(int id)
    {
        if (id == 1)
            scoreTextPlayer1.HighLight();
        else
            scoreTextPlayer2.HighLight();
    }

    public void OnStartGameButtonClicked()
    {
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winId) 
    {
        menuObject.SetActive(true);
        winText.text = $"Player {winId} won!";
    }

    public void OnVolumeChanged(float value) 
    {
        AudioListener.volume = value;
        volumeValueText.text = $"{Math.Round(value * 100)} %";
    }

    public void OnPlayModeButtonClicked()
    {
        GameManager.instance.OnPlayModeButtonClicked();
        AdjustPlayButtonText();
    }

    private void AdjustPlayButtonText()
    {
        switch (GameManager.instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                playModeButtonText.text = "2 Players";
                break;

            case GameManager.PlayMode.PlayerVsAi:
                playModeButtonText.text = "Player vs AI";
                break;
        }
    }
}

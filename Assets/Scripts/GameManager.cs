using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameUI gameUI;
    public GameAudio gameAudio;
    public BallMovement ballMovement;

    public int scorePlayer1, scorePlayer2;
    public int maxScore = 4;

    public Action onReset;  // This action is used to fire event to subscribers. GameManager acts as publisher.

    public PlayMode playMode;

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsAi
    }

    // Along with first line is the Singleton pattern
    // We keep only one instance of the GameManager class
    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
            playMode = PlayMode.PlayerVsPlayer;
        }
    }

    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
            scorePlayer1++;

        if (id == 2)
            scorePlayer2++;

        gameUI.HighLightScore(id);
        gameUI.UpdateScore(scorePlayer1, scorePlayer2);
        CheckWin();
    }

    private void CheckWin()
    {
        int winnerId = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if (winnerId != 0)
        {
            gameUI.OnGameEnds(winnerId);
            gameAudio.PlayWinSound();
        }
        else
        {
            onReset?.Invoke();
            gameAudio.PlayScoreSound();
        }
    }

    private void OnStartGame()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScore(scorePlayer1, scorePlayer2);
    }

    public void OnPlayModeButtonClicked()
    {
        switch (playMode) {
            case (PlayMode.PlayerVsPlayer):
                playMode = PlayMode.PlayerVsAi;
                break;
            case (PlayMode.PlayerVsAi):
                playMode = PlayMode.PlayerVsPlayer;
                break;
        }
    }

}

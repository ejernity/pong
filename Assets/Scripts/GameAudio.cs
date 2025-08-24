using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip paddleSound;
    public AudioClip wallSound;
    public AudioClip scoreSound;
    public AudioClip winSound;

    public void PlayPaddleSound()
    {
        audioSource.PlayOneShot(paddleSound);
    }

    public void PlayWallSound()
    {
        audioSource.PlayOneShot(wallSound);
    }

    public void PlayScoreSound()
    {
        audioSource.PlayOneShot(scoreSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
}

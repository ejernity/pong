using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 8f;
    private float moveSpeedMultiplier = 1f;
    public float maxStartY = 4f;

    private Rigidbody2D rb2d;
    private float startX = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //InitialPush();
        GameManager.instance.onReset += ResetBall;
        GameManager.instance.gameUI.onStartGame += ResetBall;
    }

    private void ResetBall()
    {
        ResetBallPosition();
        InitialPush();
    }

    private void InitialPush()
    {
        Vector2 direction = Random.value < 0.5f ? Vector2.left : Vector2.right;
        direction.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.linearVelocity = direction * moveSpeed * moveSpeedMultiplier;
    }

    private void ResetBallPosition()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
            moveSpeedMultiplier = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Wall wall = collision.collider.GetComponent<Wall>();
        if (wall)
        {
            GameManager.instance.gameAudio.PlayWallSound();
        }

        Player player = collision.collider.GetComponent<Player>();
        if (player)
        {
            GameManager.instance.gameAudio.PlayPaddleSound();
        }

        if (Random.value < 0.1f)
        {
            moveSpeedMultiplier += Random.Range(0.5f, 1.5f);
            Debug.Log($"Ball speed multiplier {moveSpeedMultiplier}");
            rb2d.linearVelocity = rb2d.linearVelocity * moveSpeedMultiplier;
        }
    }
}

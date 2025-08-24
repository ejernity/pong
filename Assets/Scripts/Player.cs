using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    public int id;
    public float moveSpeed = 5f;
    public float moveSpeedMultiplier = 2f;

    private Vector3 startPosition;

    private float aiDeadzone = 1f;
    private int direction = 0;

    private void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    void Update()
    {
        if (IsAi())
        {
            MoveAi();
        }
        else
        {
            float movement = ProcessInput();
            Move(movement);
        }
    }

    private bool IsAi() {
        return id == 2 && GameManager.instance.playMode == GameManager.PlayMode.PlayerVsAi;
    }

    private void MoveAi()
    {
        Vector2 ballPos = GameManager.instance.ballMovement.transform.position;

        if (Mathf.Abs(ballPos.y - transform.position.y) > aiDeadzone)
        {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(0.5f, 1.5f);
        }

        Move(direction);
    }

    private float ProcessInput()
    {
        float movement = 0f;

        switch (id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;

            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }

        return movement;
    }

    private void Move(float movement)
    {
        Vector2 vector2 = playerRb2D.linearVelocity;
        vector2.y = movement * moveSpeedMultiplier * moveSpeed;
        playerRb2D.linearVelocity = vector2;
    }

    private void ResetPosition() {
        transform.position = startPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float acceleration;

    private int counter = 0;
    private Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        
        direction = new Vector2(1, 0);

        StartCoroutine(LaunchBall(direction));
    }

    public IEnumerator LaunchBall(Vector2 direction)
    {
        yield return new WaitForSeconds(3.0f);
        MoveBall(direction);
    }

    public void MoveBall(Vector2 direction)
    {
        direction = direction.normalized;
        
        speed += (counter * acceleration);
        
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        } else {
            counter++;
        }
        
        rb.velocity = direction * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direction = new Vector2(direction.x, direction.y * -1);
            MoveBall(direction);
        } else if (collision.gameObject.tag == "Wall")
        {
            direction = new Vector2(direction.x * -1, direction.y);
            GameOver();
        }
    }

    public void GameOver()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        speed = minSpeed;
        counter = 0;
        StartCoroutine(LaunchBall(direction));
    }


}

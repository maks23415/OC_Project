using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Air : MonoBehaviour
{
    public float rotatePower;
    public float jumpSpeed;
    public float speed;

    private Rigidbody2D rb;
    private void Start()
    {
        Object.speed = speed;
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * jumpSpeed;

        }
        transform.eulerAngles = new Vector3(0, 0, rb.linearVelocity.y* rotatePower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") || collision.CompareTag("wardrobe"))
        {
            Score.score++;
        }
    }
}

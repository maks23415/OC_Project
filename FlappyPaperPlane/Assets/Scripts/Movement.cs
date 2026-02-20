using UnityEngine;

public class Movement : MonoBehaviour
{
    private float jumpForce = 5; // ���� ������
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(0, jumpForce);
        }
    }
}

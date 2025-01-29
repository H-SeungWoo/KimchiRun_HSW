using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce = 10f;

    [Header("References")]  
    public Rigidbody2D PlayerRB;

    private bool isGrounded = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 선형속도를 높이는 방법
            // PlayerRB.linearVelocityX = 10;
            // PlayerRB.linearVelocityY = 20;

            PlayerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

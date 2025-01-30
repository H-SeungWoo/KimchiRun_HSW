using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce = 10f;
    public int lives = 3;
    public bool isInvincible = false;

    [Header("References")]  
    public Rigidbody2D PlayerRB;
    public Animator PlayerAnim;
    private bool isGrounded = true;
    public BoxCollider2D PlayerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Hit()
    {
        lives--;
        if(lives <= 0)
        {
            DiePlayer();
        }
    }

    void Heal()
    {
        lives = Mathf.Min(lives +1, 3);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    void DiePlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnim.enabled = false;
        PlayerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
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
            PlayerAnim.SetInteger("State", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Platform")
        {
            if(!isGrounded)
            {
                PlayerAnim.SetInteger("State", 2);
            }
            isGrounded = true;
        }


    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemy")
        {
            if(!isInvincible)
            {
                Destroy(collision.gameObject);
                Hit();
            }
        }        
        else if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.tag == "GoldenFood")
        {
            Destroy(collision.gameObject);
            StartInvincible();
        }
    }
}

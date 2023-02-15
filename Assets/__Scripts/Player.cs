using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask layerOfGround;
    [SerializeField] private float circleRadius;
    [SerializeField] private Text scoreText;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isSeeRight = true;
    private bool isJumping = false;
    private bool isGround;
    private int score;

    public bool IsSeeRight
    {
        get { return isSeeRight; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scoreText.text = "Score:" + score;
    }

    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        Vector2 direction;

        if (xAxis != 0)
        {
            direction = new Vector2(xAxis * speed, rb.velocity.y);
            rb.velocity = direction;
        }
        else
        {
            direction = new Vector2(0, rb.velocity.y);
            rb.velocity = direction;
        }

        if(xAxis > 0 && isSeeRight == false || xAxis < 0 && isSeeRight)
        {
            Vector3 newScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.y);
            transform.localScale = newScale;
            isSeeRight = !isSeeRight;
        }

        anim.SetFloat("Speed", Mathf.Abs(xAxis));
    }

    private void Jump()
    {
        if(Input.GetButton("Jump") && isJumping == false && isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            anim.SetBool("IsJumping", true);
        }
    }

    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(feetPos.position, circleRadius, layerOfGround);

        if(isGround)
        {
            isJumping = false;
            anim.SetBool("IsJumping", false);
        }
    }

    public void AddScore(int bonusScore)
    {
        score += bonusScore;
        scoreText.text = "Score:" + score;
    }
}

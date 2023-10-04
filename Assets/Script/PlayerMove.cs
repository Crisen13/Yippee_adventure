using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

    float horizontalMove;
    public float speed = 4f;

    Rigidbody2D myBody;

    bool grounded = false;
    public float castDist = 1f;

    public float jumpPower = 10f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;

    bool jump = false;

    bool m_FacingRight = true;

    public GameObject heart1, heart2, heart3;
    private int life = 3;
    public bool isHurt = false;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalMove);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            myAnim.SetBool("jumping", true);
            jump = true;
        }

        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            myAnim.SetBool("running", true);
        }
        else
        {
            myAnim.SetBool("running", false);
        }

    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        if (myBody.velocity.y > 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        if (moveSpeed > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (moveSpeed <  0 && m_FacingRight)
        {
            Flip();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        if (hit.collider != null && hit.transform.tag == "Blocks")
        {
            myAnim.SetBool("jumping", false);
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);

        void Flip()
        {
            m_FacingRight = !m_FacingRight;

            transform.Rotate(0f, 180f, 0f);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            life--;
            Life();
            isHurt = true;
        }
    }

    void Life()
    {
        if (life == 3)
        {
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }

        if (life == 2)
        {
            heart3.SetActive(false);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }

        if (life == 1)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(true);
        }

        if (life < 1)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(false);

            SceneManager.LoadScene("DIES", LoadSceneMode.Single);
        }
    }
}
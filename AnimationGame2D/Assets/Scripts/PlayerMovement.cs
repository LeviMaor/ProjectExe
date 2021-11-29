
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }


        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
        anim.SetTrigger("jump");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }


    private void ResetState()
    {

        this.transform.position = new Vector3(-32, -10.5f, 0);
    }

    // A function just to show the differnce between winning and losing after the ninja saves the princess , later i will add a "You Won" frame
    private void YouWon()
    {

        this.transform.position = new Vector3(26.05f, -9f, 0);
    }

    // A function just to show that the player lose, later i will add a "You Lose" frame 
    private void YouLose()
    {
        this.transform.position = new Vector3(11, -11, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            YouLose();
        }

        if (collision.gameObject.tag == "Dragon")
        {
            YouLose();
        }

        if (collision.gameObject.tag == "Princes")
        {
            YouWon();
        }
    }


}



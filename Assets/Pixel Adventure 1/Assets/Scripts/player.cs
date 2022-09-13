using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static KeyCode KEY_JUMP;
    public KeyCode key_jump;
    public float speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;

    bool isBlowing;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // = key_jump;
    }

    void FixedUpdate()
    {
        move();
    }
    // Update is called once per frame
    void Update()
    {

        Jump();

    }

    void move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Walk", true);

        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Walk", false);

        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(key_jump) && !isBlowing)
        {

            if (!isJumping)
            {
                rig.AddForce(new Vector3(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if (doubleJump)
                {
                    rig.AddForce(new Vector3(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)

        {
            isJumping = false;
            anim.SetBool("jump", false);

        }

        if (collision.gameObject.tag == "Spikes")

        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);

        }

        if (collision.gameObject.tag == "Saw")
        {
            print("chegou aqui");
            GameController.instance.ShowGameOver();
            Destroy(gameObject);

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)

        {
            isJumping = true;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isBlowing = true;
        }
    }



    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isBlowing = false;
        }
    }

}
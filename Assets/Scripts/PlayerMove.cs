using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject character;
    private float speed = 7f;
    private float JumpHeight = 200;
    private float JumpDown = 50;
    public Rigidbody2D myrb;
    public Animator JumperAnim;

    private Vector2 playerInput;
    private bool IsJumping;
    private bool IsGrounded;
    // Start is called before the first frame update
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (IsGrounded && Input.GetKeyDown(KeyCode.W))
        {
            IsGrounded = false;
            IsJumping = true;
            myrb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            
        }

        if(!IsGrounded && Input.GetKey(KeyCode.S))
        {
            myrb.AddForce(Vector2.down * JumpDown, ForceMode2D.Force);
        }

        if (IsJumping)
        {
            myrb.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
            IsGrounded = false;
           
            
        }

        if (!IsGrounded)
        {
            IsJumping = false;
            
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
        character.transform.tag = "Ground";
        JumperAnim.SetBool("IsJumping", false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        character.transform.tag = "Ground";
        JumperAnim.SetBool("IsJumping", true);
    }
}

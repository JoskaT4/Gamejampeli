using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameObject character;
    private float speed = 6f;
    private float JumpHeight = 65;
    private float JumpDown = 45;
    public Rigidbody2D myrb;
    public Animator JumperAnim;
    //private Vector2 moveDirection;

    private Vector2 playerInput;
    private bool IsJumping;
    private bool IsGrounded;
    public bool IsWalking;
    Vector2 startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        



        {
            startPos = transform.position;
            
            
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = startPos;
    }






    // Update is called once per frame
    void FixedUpdate()
        
        //(Vector2 moveDirection)
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) // Liikkuminen vasemmalle
        {
            moveInput = -speed; // Vasemmalle
        }
        else if (Input.GetKey(KeyCode.D)) // Liikkuminen oikealle
        {
            moveInput = speed; // Oikealle
        }

        // Asetetaan pelaajan nopeus (x-akselilla)
        myrb.velocity = new Vector2(moveInput, myrb.velocity.y); // y-akselin nopeus säilyy

        // Kävelyanimaatio (vain maassa)
        if (IsGrounded && moveInput != 0)
        {
            JumperAnim.SetBool("IsWalking", true);
            JumperAnim.SetBool("IsJumping", false);
        }
        else if (moveInput == 0)
        {
            JumperAnim.SetBool("IsWalking", false);
        }

        // Hyppy-animaatio, jos ei ole maassa
        if (!IsGrounded)
        {
            JumperAnim.SetBool("IsJumping", true);
            JumperAnim.SetBool("IsWalking", false); // Sammuta kävelyanimaatio ilmassa
        }

        // Varmistetaan, että hyppy-animaatio poistetaan, kun pelaaja palaa maahan
        if (IsGrounded && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            JumperAnim.SetBool("IsJumping", false); // Sammuta hyppy animaatio maassa
        }












        if (Input.GetKey(KeyCode.W) && IsGrounded )
        {
            IsGrounded = false;
            IsJumping = true;
            myrb.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
            
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
            if (collision.gameObject.tag == ("Ground"))

            {
                Debug.Log("Grounded");
                IsGrounded = true;


            }
            JumperAnim.SetBool("IsJumping", false);
        }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!IsGrounded)
        {
            Debug.Log("Not Grounded");
            IsGrounded=false;
        }
        JumperAnim.SetBool("IsJumping", true);
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

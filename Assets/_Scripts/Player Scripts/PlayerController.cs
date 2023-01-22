using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    [SerializeField]
    public InputActionReference move,jump,interact;
    private Rigidbody rb;
    float distToGround;
    private Vector2 lastMoved = Vector2.right;

    public bool ghostActive = false;    //true when ghost is following, false when ghost is in grave
    public bool ghostAlmost = false;
    public float counter = 0;
    public float waitTime = 60;
    public GameObject ghost = default;

    public bool doubleJump = false;
    public float jumpsLeft = 1;
    public float jumpWait = 250;
    public float jumpTimer = 0;
    public Animator animator;

    public AudioSource SFX_Jump = default;
    public AudioSource SFX_Step = default;
    public AudioSource SFX_Launch = default;

    void Start()
    {
       rb=gameObject.GetComponent<Rigidbody>();
       distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        var moveInput = move.action.ReadValue<Vector2>();
        rb.velocity = new Vector3(moveInput.x*moveSpeed,rb.velocity.y,moveInput.y*moveSpeed);
        if(IsGrounded()) { animator.SetBool(1, false); }
        else { animator.SetBool(1, true); }

        if (IsGrounded() && jump.action.ReadValue<float>() > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x,jumpHeight,rb.velocity.z);
            SFX_Jump.Play();
            jumpTimer = 0;
        }
        else if(doubleJump && jumpTimer >= jumpWait && jumpsLeft > 0 && jump.action.ReadValue<float>() > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            SFX_Jump.Play();
            jumpsLeft--;
            jumpTimer = 0;
        }
        if (!IsGrounded() && doubleJump && jumpTimer < jumpWait)
        {
            jumpTimer++;
        }
        if (IsGrounded() && doubleJump)
        {
            jumpsLeft = 1;
        }
        if (!moveInput.Equals(Vector2.zero))
        {
            animator.SetBool(0, true);
            if(moveInput.y > 0) 
            {
                lastMoved = new Vector2(0, 1);
            }
            else if (moveInput.y < 0)
            {
                lastMoved = new Vector2(0, -1);
            }
            else if (moveInput.x > 0)
            {
                lastMoved = new Vector2(1,0);
            }
            else if (moveInput.x < 0)
            {
                lastMoved = new Vector2(-1, 0);
            }
        }
        else
        {
            animator.SetBool(0, false);
        }
        if (ghostAlmost)
        {
            if (counter >= waitTime)
            {
                ghostAlmost = false;
                ghostActive = true;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        if (interact.action.ReadValue<float>() > 0)
        {
           
            if (ghostActive)
            {
                Ghost ghostScript = ghost.GetComponent<Ghost>();

                if (!ghostScript.directed)
                {
                    Vector3 ghostPosition = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
                    ghost.transform.position = ghostPosition;
                    SFX_Launch.Play();
                    ghostScript.facingDirection = lastMoved;
                    ghostScript.directed = true;
                }
            }
        }
        
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Grave")
        {
            Grave graveScript = collider.gameObject.GetComponent<Grave>();
            if (graveScript.active && interact.action.ReadValue<float>() > 0)
            {
                graveScript.active = false;
                ghost.SetActive(true);
            }
        }
    }

    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}

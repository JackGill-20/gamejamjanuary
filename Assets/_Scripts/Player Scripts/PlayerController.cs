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
    private InputActionReference move,jump,interact;
    private Rigidbody rb;
    float distToGround;
    // Start is called before the first frame update

    void Start()
    {
       rb=gameObject.GetComponent<Rigidbody>();
       distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = move.action.ReadValue<Vector2>();
        rb.velocity = new Vector3(moveInput.x*moveSpeed,rb.velocity.y,moveInput.y*moveSpeed);

        if (IsGrounded()&&jump.action.ReadValue<float>()>0)
        {
            rb.velocity = new Vector3(rb.velocity.x,jumpHeight,rb.velocity.z);
        }
    }
    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}

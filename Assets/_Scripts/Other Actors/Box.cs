using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Rigidbody rb;
    bool beingPushed;
    //Features
    //  Moves when colliding with player or ghost
    //  Stopped by walls
    //  Activates pressure plate switches
    
    void Start()
    {
        rb= gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Ghost"&&collider.gameObject.GetComponent<Ghost>().directed)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            beingPushed = true;
        }
        else
        {
            rb.constraints=RigidbodyConstraints.FreezePosition|RigidbodyConstraints.FreezeRotation;
            if(beingPushed)
            {

            }
        }
    }
    }

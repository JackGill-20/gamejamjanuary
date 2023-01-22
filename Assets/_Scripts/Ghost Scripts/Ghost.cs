using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    //Features:
    //  Follows player movement, keeping within a certain circle around player
    //  Move straight in direction of player when button is pressed
    //     Moves until stopped by walls, then returns to player, vanishes and reappears next to player
    //     Passes through grates, stopped by walls
    //  Enter graves when colliding with them
    //  Spawned from active grave when player interacts with it (and deactives grave)
    //  Activates buttons when colliding with them
    //  Pushes boxes when colliding, pushes until the box hits a wall, then acts like wall
    //  Float over water

    public GameObject player = default;

    private Rigidbody rb;

    public float moveSpeed = 5f;
    public Vector2 moveDirection = default;

    public bool directed = false;
    public Vector2 facingDirection = default;

    public float followRadius = 2;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        moveDirection = new Vector2(0, 0);
    }

    void FixedUpdate()
    {

        if (!directed)
        {
            //Follow player movement
            if(transform.position.x > player.transform.position.x + followRadius || transform.position.x < player.transform.position.x - followRadius ||
                transform.position.z > player.transform.position.z + followRadius || transform.position.z < player.transform.position.z - followRadius)
            {
                Vector3 travelDirection = player.transform.position - transform.position;
                travelDirection = travelDirection.normalized;
                moveDirection.x = travelDirection.x;
                moveDirection.y = travelDirection.z;
            }
            else
            {
                moveDirection = Vector2.zero;
            }

            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.y * moveSpeed);
        }
        else
        {
            //Continue moving forward

            rb.velocity = new Vector3(facingDirection.x * moveSpeed * 2, rb.velocity.y, facingDirection.y * moveSpeed * 2);

        }

    }

    void OnCollisionEnter(Collision collider)
    {
        if (directed)
        {
            if(collider.gameObject.tag=="Grave")
            {
                PlayerController playerScript = player.GetComponent<PlayerController>();
                playerScript.ghostActive = false;
                transform.gameObject.SetActive(false);
            }
            if (collider.gameObject.tag == "Pushblock")
            {
                StartCoroutine(pushCoroutine(collider));
               
            }
        }
    }
    private IEnumerator pushCoroutine(Collision collider)
    {
        yield return new WaitForSeconds(0.1f);
        if (collider.gameObject.GetComponent<Rigidbody>().constraints == (RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation)) ;
        {
            directed = false;
        }
    }
}

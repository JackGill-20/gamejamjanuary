using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{

    //Features
    //  Two states, active and inactive
    //  Active:
    //    Activates one of three abilities
    //    Spawns ghost and becomes in inactive when player interacts with it
    //  Inactive:
    //    Becomes active once ghost collides with it, ghost is despawned

    public bool active = false;
    public float abilityType = 0;

    public float radius = 3f;

    public GameObject player = default;
    public GameObject ghost = default;

    public bool yep = false;

    private PlayerController playerScript = default;

    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        
        if(active && player.transform.position.x > transform.position.x - radius && player.transform.position.x < transform.position.x + radius &&
            player.transform.position.z > transform.position.z - radius && player.transform.position.z < transform.position.z + radius)
        {
            yep = true;

            if(playerScript.interact.action.ReadValue<float>() > 0)
            {
                yep = false;
                active = false;
                playerScript.ghostAlmost = true;
                ghost.SetActive(true);
                ghost.transform.position = player.transform.position;
            }
        }

    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Ghost")
        {
            if (collider.gameObject.GetComponent<Ghost>().directed && !active)
            {
                active = true;
                collider.gameObject.GetComponent<Ghost>().directed = false;
            }
        }
    }
}

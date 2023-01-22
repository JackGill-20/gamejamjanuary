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
    public int abilityType = 0;

    public float radius = 3f;

    public GameObject player = default;
    public GameObject ghost = default;
    private GameObject[] grates;
    private SpriteRenderer spriteRenderer;
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private PlayerController playerScript = default;

    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
       
        grates=GameObject.FindGameObjectsWithTag("Grate");

        spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        
        if(active && player.transform.position.x > transform.position.x - radius && player.transform.position.x < transform.position.x + radius &&
            player.transform.position.z > transform.position.z - radius && player.transform.position.z < transform.position.z + radius)
        {

            if(playerScript.interact.action.ReadValue<float>() > 0)
            {
                active = false;
                spriteRenderer.sprite = inactiveSprite;
                playerScript.ghostAlmost = true;
                playerScript.doubleJump = false;
                ghost.SetActive(true);
                ghost.transform.position = player.transform.position;
                foreach (GameObject g in grates)
                {
                    g.GetComponent<Collider>().enabled = true;
                }
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
                spriteRenderer.sprite = activeSprite;
                collider.gameObject.GetComponent<Ghost>().directed = false;
                if(abilityType == 0)
                {
                    playerScript.doubleJump = true;
                }
                if (abilityType == 1)
                {
                    foreach(GameObject g in grates)
                    {
                        g.GetComponent<Collider>().enabled = false;

                    }
                }
            }
        }
    }
}

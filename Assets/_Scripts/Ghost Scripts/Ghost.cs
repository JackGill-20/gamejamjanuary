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

    public bool commanded = false;

    void Start()
    {
        
    }

    void FixedUpdate()
    {

        if (!commanded)
        {
            //Follow player movement

        }
        else
        {
            //Continue moving forward

            //Check for button collision

        }

    }
}

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

    public GameObject ghost = default;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float playerPosition = default;
    public float height = default;  //Change to take height of camera
    public float angle = 65;

    //Finds the position the camera needs to keep the player centered
    //Distance from player equation is as follows:
    //  height * tan(angle) = distance

    void FixedUpdate()
    {
        
    }
}

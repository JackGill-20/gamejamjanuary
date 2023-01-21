using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player = default;

    public float height = default;
    public float angle = 25;

    public float heightOffset = default;
    public float offset = default;

    //Finds the position the camera needs to keep the player centered
    //Distance from player equation is as follows:
    //  height * tan(angle) = distance

    void Start()
    {
        height = transform.position.y;
    }

    void FixedUpdate()
    {

        heightOffset = height - player.transform.position.y;
        offset = heightOffset * Mathf.Tan((Mathf.PI/180) * angle);

        transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z - offset);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool pressed=false;
    public Vector3 spawncoords;
    public GameObject spawned;

    //Features
    //  Activates when ghost collides with it
    //  Cannot be activated by protagonist

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       
            if(!pressed) 
            { 
                pressed= true;
                Instantiate(spawned,spawncoords,Quaternion.identity);

            }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool pressed=false;
    public Vector3 spawncoords;
    public GameObject spawned;
    private SpriteRenderer spriteRenderer;
    public Sprite activeSprite;

    public AudioSource SFX_Pressed = default;

    //Features
    //  Activates when ghost collides with it
    //  Cannot be activated by protagonist

    void Start()
    {
       spriteRenderer= gameObject.GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (!pressed)
        {
            SFX_Pressed.Play();
            spriteRenderer.sprite = activeSprite;
            pressed = true;
            Instantiate(spawned, spawncoords, Quaternion.identity);
        }
        
    }

}

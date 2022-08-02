using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rbCollider;
    private Vector2 screenBounds;
    private Vector3 direction;

    public void Setup(Vector3 dir)
    {
        this.direction = dir;
        if (rbCollider == null)
            rbCollider = GetComponent<Rigidbody2D>();
        
        rbCollider.AddForce(dir * speed, ForceMode2D.Impulse);
        
        Destroy(this.gameObject, .75f);
    }
    
    private 

    void Start()
    {
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    void Update()
    {
        if (transform.position.y > screenBounds.y * -2)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check for enemy
        Destroy(this.gameObject);
    }
}

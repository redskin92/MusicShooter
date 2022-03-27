using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rbCollider;
    private Vector2 screenBounds; 
    


    void Start()
    {
        rbCollider = this.GetComponent<Rigidbody2D>();
        rbCollider.velocity = new Vector2(0, speed);
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

}

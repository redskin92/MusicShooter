using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject _playerObject;

    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       // Move the player 
        Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

    }
    void Move(Vector2 move)
    {
        _playerObject.transform.Translate(move*moveSpeed*Time.deltaTime);
    }
    
 
}

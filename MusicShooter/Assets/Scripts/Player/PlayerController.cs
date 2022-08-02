using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject _playerObject;
    public float moveSpeed = 5f;
    private Transform _transform;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        CursorController.instance.ActivateCursor();
        _transform = _playerObject.transform;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // look at cursor
        LookAtCursor();    
        
        // Move the player 
        Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
    void Move(Vector2 move)
    {
        _transform.Translate(move*moveSpeed*Time.deltaTime,Space.World);
    }

    private void LookAtCursor()
    {
        Vector3 lookToVector = Vector3.zero;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);             
        Vector3 dir = Input.mousePosition - objectPos;              
        lookToVector = Vector3.zero;             
        lookToVector.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;             
        transform.rotation = Quaternion.Euler(lookToVector);
    }

}

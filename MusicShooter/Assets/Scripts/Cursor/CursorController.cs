using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CursorController: MonoBehaviour
{

    public static CursorController instance;
    
    public  Texture2D crossHair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        instance = this; 
    }
    
    public void ActivateCursor()
    {
        Cursor.SetCursor(crossHair,new Vector2( crossHair.width/2,crossHair.height/2),CursorMode.Auto);
    }

    public Vector3 GetCursorPositon()
    {
        return Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }
    
}

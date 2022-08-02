using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject _bullet;
    public float timeBetweenShots;
    private bool shooting;
    private bool readyToShoot;
    public Transform firingPoint;
    public bool allowInvoke = true;
    private CursorController cursor;

    private void Awake()
    {
        readyToShoot = true;
       
    }

    // Start is called before the first frame update
    private void CheckInput()
    {
        shooting = Input.GetKey(KeyCode.Space);

        if (readyToShoot && shooting)
            Shoot();
    }

    private IEnumerator WaitTimetoShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    void Shoot()
    {
        readyToShoot = false;
        GameObject shot = Instantiate(_bullet , firingPoint.position, quaternion.identity) as GameObject;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);             
        Vector3 dir = Input.mousePosition - objectPos;
        shot.gameObject.GetComponent<Projectile>().Setup((dir - firingPoint.position).normalized);
        StartCoroutine(WaitTimetoShot());
    }
    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject _bullet;
    public float timeBetweenShots;
    private bool shooting;
    private bool readyToShoot;
    public Transform firingPoint;
    public bool allowInvoke = true;

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
        GameObject shot = Instantiate(_bullet) as GameObject;
        shot.transform.position = firingPoint.transform.position;
        StartCoroutine(WaitTimetoShot());

    }
    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
}

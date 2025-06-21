using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AlienShooting : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float shootTime;
    [SerializeField] private List<GameObject> laserPool;

    private bool _canShoot;
    private GameObject _laser;
    
    private Vector3 _direction;
    private float _angle;


    private void OnEnable()
    {
        StartCoroutine(ShootingTimer());
    }

    void Update()
    {
        if (_canShoot)
        {
            _canShoot = false;
            Shoot();
            StartCoroutine(ShootingTimer());
        }
    }

    // shooting with pooling logic
    private void Shoot()
    {
        foreach (GameObject laser in laserPool.Where(obj => !obj.activeInHierarchy))
        {
            _laser = laser;
            break;
        }

        if (_laser)
        {
            _laser.transform.position = transform.position;
            _laser.transform.rotation = Quaternion.Euler(GetLaserRotation());
            _laser.SetActive(true);
        }
        else
        {
            Debug.LogError("Haven't found any laser Objects!!!!!!!!!!!!!!!!");
            return;
        }
    }

    private Vector3 GetLaserRotation()
    {
        _direction = target.transform.position - transform.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        return new Vector3(0, 0, _angle-90);
    }

    IEnumerator ShootingTimer()
    {
        yield return new WaitForSeconds(shootTime);
        _canShoot = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectileTemplate;

    [SerializeField] private float _cooldownTime;
    private float _cooldownElapsed = 0f;

    private bool _canShoot = true;

    private void Update ()
    {
        if (!GameManager.instance.GetHasStarted())
            return;

        if (Input.GetButtonDown("Fire1") && _canShoot)
        {
            Shoot();
        }

        if (!_canShoot)
        {
            _cooldownElapsed += Time.deltaTime;
            if (_cooldownElapsed >= _cooldownTime)
            {
                _canShoot = true;
                _cooldownElapsed = 0f;
            }
        }
    }

    private void Shoot()
    {
        _canShoot = false;
        GameObject newProjectile = Instantiate(_projectileTemplate, _firePoint.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetDirection(_firePoint.up);
    }
}

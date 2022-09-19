using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Spawnable
{
    private void Start ()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate ()
    {
        if (_canTranslate)
        {
            transform.position = transform.position + (Vector3)_direction * _speed * Time.fixedDeltaTime;
        }
    }
}

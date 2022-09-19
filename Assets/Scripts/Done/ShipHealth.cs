using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private float _blinkTime = 0.25f;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private bool _immune = false;

    private void Awake ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && !_immune)
        {
            ProcessDeath();
        }
    }

    private void ProcessDeath ()
    {
        GameManager.instance.DecreaseNoLives();

        if (GameManager.instance.GetNoLives() > 0)
        {
            _immune = true;
            _rigidBody.isKinematic = true;

            // Reset da física
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            _rigidBody.velocity = Vector3.zero;

            StartCoroutine(Blink(5));
        }
    }

    IEnumerator Blink (int noTimes)
    {
        for (int i = 0; i < noTimes; ++i)
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(_blinkTime);
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(_blinkTime);
        }
        Respawn();
    }

    private void Respawn ()
    {
        _rigidBody.isKinematic = false;
        _immune = false;
    }
}

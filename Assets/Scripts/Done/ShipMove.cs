using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    [SerializeField] private Sprite _idle, _thrust;

    [SerializeField] private float _thrustPower = 2f;
    [SerializeField] private float _angleStep = 90f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;

    private Vector2 _movement;

    private void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start ()
    {
        _spriteRenderer.sprite = _idle;
    }

    private void Update ()
    {
        if (!GameManager.instance.GetHasStarted())
            return;

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        ValidateInput();

        UpdateSprite();
    }

    private void ValidateInput()
    {
        // Não permite que o input de y negativo seja válido (foguete dando ré) ou nulo (parado)
        if (_movement.y <= 0f)
            _movement.y = 0f;
    }   

    private void FixedUpdate ()
    {
        if (_movement.y != 0f)
        {
            _rigidBody.AddForce(transform.up * _thrustPower, ForceMode2D.Force);
        }

        if (_movement.x != 0f)
        {
            RotateShip();
        }
    }

    private void UpdateSprite ()
    {
        // Atualiza sprite caso esteja acelerando o foguete
        // ou quando deixa de acelerar
        if (_movement.y > 0f)
            _spriteRenderer.sprite = _thrust;
        else
            _spriteRenderer.sprite = _idle;
    }

    private void RotateShip ()
    {
        Vector3 angleRotation = transform.rotation.eulerAngles;

        if (_movement.x > 0f)
            angleRotation.z -= _angleStep * Time.fixedDeltaTime;
        else
            angleRotation.z += _angleStep * Time.fixedDeltaTime;

        transform.rotation = Quaternion.Euler(angleRotation);
    }
}

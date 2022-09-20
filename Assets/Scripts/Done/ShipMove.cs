using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    [SerializeField] private Sprite _idle, _thrust;

    [SerializeField] private float _thrustPower = 2f;
    [SerializeField] private float _angleStep = 90f;

    [SerializeField] private Transform _boundaryLeft;
    [SerializeField] private Transform _boundaryRight;
    [SerializeField] private Transform _boundaryTop;
    [SerializeField] private Transform _boundaryBottom;

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

        CheckTeleport();

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

    private void CheckTeleport ()
    {
        

        if (transform.position.x < _boundaryLeft.position.x)
        {
            Teleport(_boundaryRight, true);
            return;
        }
        
        if (transform.position.x > _boundaryRight.position.x)
        {
            Teleport(_boundaryLeft, true);
            return;
        }

        if (transform.position.y > _boundaryTop.position.y)
        {
            Teleport(_boundaryBottom, false);
            return;
        }

        if (transform.position.y < _boundaryBottom.position.y)
        {
            Teleport(_boundaryTop, false);
            return;
        }
    }

    private void Teleport (Transform destination, bool isX)
    {
        _rigidBody.isKinematic = true;

        if (isX)
        {
            transform.position = new Vector3(destination.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, destination.position.y, transform.position.z);
        }

        _rigidBody.isKinematic = false;
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

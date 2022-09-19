using System.Collections;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [SerializeField] protected float _speed = 5f;

    protected Vector2 _direction;
    protected bool _canTranslate = false;

    public void SetDirection (Vector2 direction)
    {
        _direction = direction;
        _canTranslate = true;
    }
    
    public void SetSpeed (float speed)
    {
        _speed = speed;
    }
}
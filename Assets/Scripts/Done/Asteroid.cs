using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Spawnable
{
    [SerializeField] private AsteroidSize _size;

    private void FixedUpdate()
    {
        if (_canTranslate)
        {
            transform.position = transform.position + (Vector3)_direction * _speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (_size == AsteroidSize.Medium || _size == AsteroidSize.Large)
            {
                SplitAsteroid();
            }
            AddToScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void SplitAsteroid ()
    {
        int splitNo = Random.Range(2, 4);

        for (int i = 0; i < splitNo; ++i)
        {
            GameObject spawnedAsteroid;

            Vector2 randomDirection = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            float randomSpeed = Random.Range(0.5f, 3f);

            if (i == 2 && _size == AsteroidSize.Large)
            {
                spawnedAsteroid = Instantiate(GameManager.instance.GetAsteroidMedium(), 
                    transform.position, Quaternion.identity);
            }
            else
            {
                spawnedAsteroid = Instantiate(GameManager.instance.GetAsteroidSmall(), 
                    transform.position, Quaternion.identity);
            }

            Asteroid newAsteroidComponent = spawnedAsteroid.GetComponent<Asteroid>();
            newAsteroidComponent.SetDirection(randomDirection);
            newAsteroidComponent.SetSpeed(randomSpeed);
        }
    }

    private void AddToScore()
    {
        GameManager.instance.AddScore(_size);
    }
}

using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawn Coordinates")]
    [Space]
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _topRight;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _bottomRight;
    [SerializeField] private Transform _leftTop;
    [SerializeField] private Transform _leftBottom;
    [SerializeField] private Transform _rightTop;
    [SerializeField] private Transform _rightBottom;

    [Header("Spawn Time")]
    [Space]
    [SerializeField] private float _spawnTimeMax;
    [SerializeField] private float _spawnTimeMin;
    [SerializeField] private float _spawnGameTimeMaxThreshold;

    [Header("Spawned Asteroid Speed")]
    [Space]
    [SerializeField] private float _spawnedMinSpeed = 0.5f;
    [SerializeField] private float _spawnedMaxSpeed = 2.5f;

    private float _spawnTimeNext;
    private float _spawnTimeElapsed = 0f;
    private bool _canSpawn = true;

    private float CalculateNextSpawnTime ()
    {
        return Mathf.Lerp(_spawnTimeMax, _spawnTimeMin,
            Mathf.Clamp01(GameManager.instance.GetGameTime() / _spawnGameTimeMaxThreshold * _spawnTimeMin));
    }

    private void Update ()
    {
        if (!GameManager.instance.GetHasStarted())
            return;

        if (_canSpawn)
        {
            SpawnAsteroid();
            _canSpawn = false;
            _spawnTimeNext = CalculateNextSpawnTime();
        }

        _spawnTimeElapsed += Time.deltaTime;

        if (_spawnTimeElapsed >= _spawnTimeNext)
        {
            _spawnTimeElapsed = 0f;
            _canSpawn = true;
        }
    }

    private void SpawnAsteroid ()
    {
        // 0 - Top | 1 - Bottom | 2 - Left | 3 - Right
        int spawnPosition = Random.Range(0, 4);

        // 0 - Large | 1 - Medium | 2 - Small
        int spawnSize = Random.Range(0, 3);

        float spawnSpeed = Random.Range(_spawnedMinSpeed, _spawnedMaxSpeed);

        Vector2 spawnCoordinates = Vector2.zero;
        Vector2 spawnDirection = Vector2.zero; 
        GameObject newAsteroid = null;

        switch (spawnPosition)
        {
            case 0:
                spawnCoordinates = new Vector2(Random.Range(_topLeft.position.x, _topRight.position.x),
                    _topRight.position.y);
                spawnDirection = new Vector2(Random.Range(-0.7f, 0.7f), -1f);
                break;
            case 1:
                spawnCoordinates = new Vector2(Random.Range(_bottomLeft.position.x, _bottomRight.position.x),
                    _bottomRight.position.y);
                spawnDirection = new Vector2(Random.Range(-0.7f, 0.7f), 1f);
                break;
            case 2:
                spawnCoordinates = new Vector2(_leftTop.position.x,
                    Random.Range(_leftTop.position.y, _leftBottom.position.y));
                spawnDirection = new Vector2(1f, Random.Range(-0.7f, 0.7f));
                break;
            case 3:
                spawnCoordinates = new Vector2(_rightTop.position.x,
                    Random.Range(_rightTop.position.y, _rightBottom.position.y));
                spawnDirection = new Vector2(-1f, Random.Range(-0.7f, 0.7f));
                break;
            default:
                break;
        }

        switch (spawnSize)
        {
            case 0:
                newAsteroid = Instantiate(GameManager.instance.GetAsteroidLarge(), 
                    spawnCoordinates, Quaternion.identity);
                break;
            case 1:
                newAsteroid = Instantiate(GameManager.instance.GetAsteroidMedium(),
                    spawnCoordinates, Quaternion.identity);
                break;
            case 2:
                newAsteroid = Instantiate(GameManager.instance.GetAsteroidSmall(),
                   spawnCoordinates, Quaternion.identity);
                break;
            default:
                break;
        }

        if (newAsteroid != null)
        {
            Asteroid asteroidComponent = newAsteroid.GetComponent<Asteroid>();
            asteroidComponent.SetDirection(spawnDirection);
            asteroidComponent.SetSpeed(spawnSpeed);
        }
 
    }
}

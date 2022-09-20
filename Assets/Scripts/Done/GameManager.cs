using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _gameTime = 0f;

    private bool _hasStarted = false;

    private int _score = 0;

    private int _noLives = 3;

    [SerializeField] private int ValueBig = 200;
    [SerializeField] private int ValueMedium = 100;
    [SerializeField] private int ValueSmall = 50;

    [SerializeField] private GameObject _asteroidLarge;
    [SerializeField] private GameObject _asteroidMedium;
    [SerializeField] private GameObject _asteroidSmall;

    [SerializeField] private GameObject _uiGame;
    [SerializeField] private GameObject _uiStartgame;
    [SerializeField] private GameObject _uiGameover;

    [SerializeField] private GameObject _playerObject;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private LivesDisplayer _livesDisplayer;

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(instance);
    }

    private void Update ()
    {
        if (_hasStarted)
            _gameTime += Time.deltaTime;
    }

    public bool GetHasStarted () => _hasStarted;

    public void SetHasStarted (bool value) => _hasStarted = value;

    public float GetGameTime () => _gameTime;

    public float GetNoLives() => _noLives;

    public int GetScore () => _score;

    public void AddScore (int delta) => _score += delta;

    private void UpdateScoreUI ()
    {
        _scoreText.text = _score.ToString();
    }

    public void AddScore (AsteroidSize size)
    {
        switch (size)
        {
            case AsteroidSize.Small:
                AddScore(ValueSmall);
                break;
            case AsteroidSize.Medium:
                AddScore(ValueMedium);
                break;
            case AsteroidSize.Large:
                AddScore(ValueBig);
                break;
            default:
                break;
        }
        UpdateScoreUI();
    }

    public void DecreaseNoLives ()
    {
        _noLives--;

        _livesDisplayer.UpdateLivesDisplay(_noLives);

        if (_noLives == 0)
            ShowGameOver();
    }

    public void StartGame()
    {
        // Reset score
        _score = 0;
        UpdateScoreUI();

        // Reset game time
        _gameTime = 0f;

        _uiGameover.SetActive(false);
        _uiGame.SetActive(true);
        _uiStartgame.SetActive(false);
        _hasStarted = true;
        ResetPlayerPosition();
        _playerObject.SetActive(true);
    }

    private void ResetPlayerPosition()
    {
        _playerObject.transform.position = Vector3.zero;
        _playerObject.transform.rotation = Quaternion.identity;
    }

    private void ShowGameOver ()
    {
        _playerObject.SetActive(false);
        _uiGame.SetActive(false);
        _uiGameover.SetActive(true);
        _hasStarted = false;
    }

    public GameObject GetAsteroidLarge() => _asteroidLarge;
    public GameObject GetAsteroidMedium() => _asteroidMedium;
    public GameObject GetAsteroidSmall() => _asteroidSmall;

}

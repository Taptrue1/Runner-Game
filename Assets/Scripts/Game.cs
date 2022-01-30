using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Player _player;
    [SerializeField] private GameView _gameView;
    [Header("Score Count Settings")]
    [SerializeField] private float _countCoefficient;

    private Money _money;
    private ScoreCounter _scoreCounter;

    private float _playerPosition => _player.transform.position.z;
    private int _sceneIndex => SceneManager.GetActiveScene().buildIndex;

    private void Start()
    {
        _money = new Money(0);
        _scoreCounter = new ScoreCounter();

        _scoreCounter.Init(_playerPosition, _countCoefficient);
        _gameView.Init(_scoreCounter, _money);

        _player.CoinGot += _money.Increase;
        _player.Died += OnPlayerDied;
    }
    private void Update()
    {
        _scoreCounter.Count(_playerPosition);
    }

    private void OnPlayerDied()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
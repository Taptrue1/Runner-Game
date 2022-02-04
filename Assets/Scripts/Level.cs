using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    internal class Level : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Player _player;
        [SerializeField] private ScoreView _scoreView;
        [Header("Score Count Settings")]
        [SerializeField] private int _countCoefficient;

        private Score _levelScore;
        private float _lastPlayerPosition;

        private void Start()
        {
            Init();
        }
        private void Update()
        {
            CountScore();
        }

        private void Init()
        {
            _levelScore = new Score();
            _lastPlayerPosition = _player.transform.position.z;

            _scoreView.Init(_levelScore);

            _player.Died += OnPlayerDied;
            _player.CoinGot += OnCoinGot;
        }

        private void OnCoinGot(int value)
        {
            _levelScore.Add(value);
            _scoreView.AnimateText();
        }
        private void OnPlayerDied()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void CountScore()
        {
            var travelledPath = _player.transform.position.z - _lastPlayerPosition;
            var score = (int)Mathf.Ceil(travelledPath * _countCoefficient);

            _levelScore.Add(score);
            _lastPlayerPosition = _player.transform.position.z;
        }
    }
}

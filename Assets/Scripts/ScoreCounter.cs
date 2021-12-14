using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public float Score => _score;

    [SerializeField] private Transform _player;
    [SerializeField] private float _coefficient;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private string _format;

    private float _score;
    private float _startDistance;
    private float _coveredDistance;

    private void Start()
    {
        _startDistance = _player.position.z;
    }
    private void Update()
    {
        _coveredDistance = _player.position.z - _startDistance;
        _score = Mathf.Ceil(_coveredDistance * _coefficient);
        _scoreText.text = string.Format(_format, _score);
    }
}

using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _coefficient;
    [SerializeField] private TextMeshProUGUI _score;

    private float _startDistance;
    private float _coveredDistance;

    private void Start()
    {
        _startDistance = _player.position.z;
    }
    private void Update()
    {
        _coveredDistance = _player.position.z - _startDistance;
        _score.text = Mathf.Ceil(_coveredDistance * _coefficient).ToString();
    }
}

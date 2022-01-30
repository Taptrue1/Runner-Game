using System;
using UnityEngine;

public class ScoreCounter
{
    public int Score => _score;
    public Action<int> ScoreChanged;

    private int _score;
    private float _lastPosition;
    private float _countCoefficient;

    public void Init(float startPosition, float countCoefficient)
    {
        _lastPosition = startPosition;
        _countCoefficient = countCoefficient;
    }
    public void Count(float currentPosition)
    {
        var difference = currentPosition - _lastPosition;
        _score += (int)Mathf.Ceil(difference * _countCoefficient);
        _lastPosition = currentPosition;
        ScoreChanged?.Invoke(_score);
    }
}

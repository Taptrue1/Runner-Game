using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Position")]
    [SerializeField] private Vector3 _levelPosition;
    [Header("Level Parts Options")]
    [SerializeField] private GameObject[] _levelParts;
    [SerializeField] private float _partsLength;
    [SerializeField] private int _partsCount;
    [Header("Player Transform")]
    [SerializeField] private Transform _player;

    private List<GameObject> _partsPool;
    private int _nextPartIndex;
    private const int _offset = 2;

    private void Start()
    {
        _partsPool = new List<GameObject>();

        for(int i = 0; i < _partsCount; i++)
        {
            SpawnPart();
        }
    }
    private void Update()
    {
        CheckPartPass();
    }

    private void CheckPartPass()
    {
        var part = _partsPool[0];
        var distanceToCover = part.transform.position.z + _offset + _partsLength / 2;
        var isPartPassed = distanceToCover < _player.position.z;

        if (isPartPassed)
        {
            _partsPool.Remove(part);
            Destroy(part);
            SpawnPart();
        }
    }
    private void SpawnPart()
    {
        var part = Instantiate(_levelParts[_nextPartIndex], _levelPosition, Quaternion.identity, transform);

        _partsPool.Add(part);
        _levelPosition.z += _partsLength;
        _nextPartIndex = _nextPartIndex + 1 > _levelParts.Length - 1 ? 0 : _nextPartIndex + 1;
    }
}
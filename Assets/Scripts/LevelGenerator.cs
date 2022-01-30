using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform _player;
    [Header("Level Position")]
    [SerializeField] private Vector3 _levelPosition;
    [Header("Spawn Options")]
    [SerializeField] private GameObject _startLevelPart;
    [SerializeField] private GameObject[] _levelParts;
    [SerializeField] private float _partsLength;
    [SerializeField] private int _partsCount;

    private List<GameObject> _partsPool;
    private const int _offset = 2;

    private void Start()
    {
        _partsPool = new List<GameObject>();

        Spawn(_startLevelPart);

        for(int i = 0; i < _partsCount - 1; i++)
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
    private void Spawn(GameObject part)
    {
        var spawnedPart = Instantiate(part, _levelPosition, Quaternion.identity, transform);

        _partsPool.Add(spawnedPart);
        _levelPosition.z += _partsLength;
    }
    private void SpawnPart()
    {
        var partIndex = Random.Range(0, _levelParts.Length);
        Spawn(_levelParts[partIndex]);
    }
}
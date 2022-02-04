using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class LevelGenerator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _startLevelPart;
        [SerializeField] private GameObject[] _levelParts;
        [Header("Settings")]
        [SerializeField] private int _playerPositionOffset;
        [SerializeField] private Vector3 _startLevelPosition;
        [SerializeField] private float _partLength;
        [SerializeField] private int _partsCount;

        private List<GameObject> _partsPool;

        private void Start()
        {
            _partsPool = new List<GameObject>();

            if (_startLevelPart != null)
                Spawn(_startLevelPart);

            for (int i = 0; i < _partsCount - 1; i++)
            {
                SpawnPart();
            }
        }
        private void Update() => CheckPartPass();

        private void CheckPartPass()
        {
            var part = _partsPool[0];
            var halfPathLength = _partLength / 2;
            var distanceToCover = part.transform.position.z + halfPathLength + _playerPositionOffset;
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
            var partIndex = Random.Range(0, _levelParts.Length);
            Spawn(_levelParts[partIndex]);
        }
        private void Spawn(GameObject part)
        {
            var spawnedPart = Instantiate(part, _startLevelPosition, Quaternion.identity, transform);

            _partsPool.Add(spawnedPart);
            _startLevelPosition.z += _partLength;
        }
    }
}
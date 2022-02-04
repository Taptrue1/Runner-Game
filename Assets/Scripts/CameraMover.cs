using UnityEngine;

namespace Assets.Scripts
{
    internal class CameraMover : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform _player;
        [Header("Settings")]
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private float _speed;

        private Vector3 _velocity;

        private void Start()
        {
            transform.position = _player.position + _positionOffset;
        }
        private void Update() => Move();

        private void Move()
        {
            var cameraPosition = _player.position + _positionOffset;
            transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref _velocity, _speed);
        }
    }
}

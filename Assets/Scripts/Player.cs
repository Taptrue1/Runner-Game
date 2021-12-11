using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedIncrease;

    private Rigidbody _rigidbody;
    private float _runStartTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _runStartTime = Time.time;
    }
    private void FixedUpdate()
    {
        var speed = _speed + _speedIncrease * (Time.time - _runStartTime);
        _rigidbody.velocity = new Vector3(0, 0, speed);
    }
}

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

        Debug.Log(_runStartTime);
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, _speed);
    }
}

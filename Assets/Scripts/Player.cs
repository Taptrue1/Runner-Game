using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Action Died;
    public Action CoinGot;

    [SerializeField] Animator _animator;
    [Header("Speed")]
    [SerializeField] private float _speed;
    [SerializeField] private float _increaseSpeed;
    [SerializeField] private float _sideSpeed;
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [Header("Road Lines")]
    [SerializeField] private int _currentLine;
    [SerializeField] private int _linesCount;
    [SerializeField] private float _lineSize;

    private Rigidbody _rigidbody;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private float _startRunTime;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startRunTime = Time.time;
        _animator.SetFloat("MoveSpeed", 1);
    }
    private void Update()
    {
        var input = GetSwipe();
        var canJump = input == Vector2.up && _isGrounded;

        if (canJump)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        _animator.SetBool("Grounded", _isGrounded);

        ChangeLine(input);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            Died?.Invoke();
        else if (other.TryGetComponent(out Coin coin))
            CoinGot?.Invoke();
    }
    private void OnCollisionStay(Collision collision)
    {
        _isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void Move()
    {
        var speed = _speed + _increaseSpeed * (Time.time - _startRunTime);
        var resultX = Mathf.MoveTowards(transform.position.x, _currentLine * _lineSize, _sideSpeed * Time.fixedDeltaTime);

        transform.position = new Vector3(resultX, transform.position.y, transform.position.z);
        transform.Translate(new Vector3(0, 0, speed) * Time.fixedDeltaTime);
    }
    private Vector2 GetSwipe()
    {
        var swipe = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosition = Input.mousePosition;
            var input = _endTouchPosition - _startTouchPosition;

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                swipe = input.x < 0 ? Vector2.left : Vector2.right;
            else
                swipe = input.y < 0 ? Vector2.down : Vector2.up;
        }

        return swipe;
    }
    private void ChangeLine(Vector2 input)
    {
        if (input == Vector2.right && _currentLine < _linesCount)
            _currentLine++;
        else if (input == Vector2.left && _currentLine > 0)
            _currentLine--;
    }
}
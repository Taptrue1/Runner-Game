using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedIncrease;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;

    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _startRunTime;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _startRunTime = Time.time;
    }
    private void Update()
    {
        var input = GetSwipe();
        var canJump = input == Vector2.up && _controller.isGrounded;

        if (canJump)
        {
            _moveDirection.y = _jumpForce;
        }
    }
    private void FixedUpdate()
    {
        var speed = _speed + _speedIncrease * (Time.time - _startRunTime);
        _moveDirection.z = _speed;
        _moveDirection.y -= _gravity * Time.fixedDeltaTime;
        _controller.Move(_moveDirection * Time.fixedDeltaTime);
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
}

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Speed Options")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedIncrease;
    [SerializeField] private float _sideSpeed;
    [Header("Jump Options")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [Header("Road Options")]
    [SerializeField] private int _currentLine;
    [SerializeField] private int _linesCount;
    [SerializeField] private float _lineSize;

    private CharacterController _controller;
    private Vector3 _moveDirection;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private float _startRunTime;
    private const float _rayDistance = 1;

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
            _moveDirection.y = _jumpForce;

        ChangeLine(input);
    }
    private void FixedUpdate()
    {
        var speed = _speed + _speedIncrease * (Time.time - _startRunTime);
        var resultX = Mathf.MoveTowards(transform.position.x, _currentLine * _lineSize, _sideSpeed * Time.fixedDeltaTime);

        _moveDirection.y -= _gravity * Time.fixedDeltaTime;
        _controller.Move(_moveDirection * Time.fixedDeltaTime);
        transform.position = new Vector3(resultX, transform.position.y, transform.position.z);
        transform.Translate(new Vector3(0, 0, speed) * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            gameObject.SetActive(false);
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
        if (input == Vector2.right)
            _currentLine++;
        else if (input == Vector2.left)
            _currentLine--;

        if (_currentLine < 0) _currentLine = 0;
        else if (_currentLine > _linesCount) _currentLine = _linesCount;
    }
}
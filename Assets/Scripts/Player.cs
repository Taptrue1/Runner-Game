using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Speed Options")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedIncrease;
    [SerializeField] private float _sideSpeed;
    [Header("Jump Options")]
    [SerializeField] private float _jumpForce;
    [Header("Road Options")]
    [SerializeField] private int _currentLine;
    [SerializeField] private int _linesCount;
    [SerializeField] private float _lineSize;

    private Rigidbody _rigidbody;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private float _startRunTime;
    private const float _groundCheckDistance = 1;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startRunTime = Time.time;
    }
    private void Update()
    {
        var input = GetSwipe();
        var canJump = input == Vector2.up && isGrounded();

        if (canJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        ChangeLine(input);
    }
    private void FixedUpdate()
    {
        var speed = _speed + _speedIncrease * (Time.time - _startRunTime);
        var moveDirection = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, speed);
        var targetPosition = new Vector3(_currentLine * _lineSize, transform.position.y, transform.position.z);

        _rigidbody.velocity = moveDirection;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _sideSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
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
    private bool isGrounded()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _groundCheckDistance);
        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance);
        if (hit.collider != null)
            return true;

        return false;
    }
}
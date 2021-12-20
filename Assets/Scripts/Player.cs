using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] Animator _animator;
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

        if (_isGrounded)
            _animator.SetBool("Grounded", true);
        else
            _animator.SetBool("Grounded", false);

        ChangeLine(input);
    }
    private void FixedUpdate()
    {
        var speed = _speed + _speedIncrease * (Time.time - _startRunTime);
        var resultX = Mathf.MoveTowards(transform.position.x, _currentLine * _lineSize, _sideSpeed * Time.fixedDeltaTime);

        transform.position = new Vector3(resultX, transform.position.y, transform.position.z);
        transform.Translate(new Vector3(0, 0, speed) * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            gameObject.SetActive(false);
    }
    private void OnCollisionStay(Collision collision)
    {
        _isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
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
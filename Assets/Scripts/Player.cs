using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    internal class Player : MonoBehaviour
    {
        public Action Died;
        public Action<int> CoinGot;

        [Header("Dependencies")]
        [SerializeField] private Animator _animator;
        [Header("Move Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _increaseSpeed;
        [SerializeField] private float _sideSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _fallForce;
        [Header("Lines Settings")]
        [SerializeField] private int _startLine;
        [SerializeField] private int _linesCount;
        [SerializeField] private float _lineStep;

        private Rigidbody _rigidbody;
        private int _currentLine;
        private float _startTime;
        private bool _isGrounded;
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startTime = Time.time;
            _currentLine = _startLine;
            _animator.SetFloat("MoveSpeed", 1);
        }
        private void Update()
        {
            var input = GetSwipe();
            ReadInput(input);
            Move();
        }
        private void OnCollisionStay(Collision collision) => _isGrounded = true;
        private void OnCollisionExit(Collision collision) => _isGrounded = false;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out Enemy enemy))
            {
                Died?.Invoke();
            }
            else if(other.gameObject.TryGetComponent(out Coin coin))
            {
                CoinGot?.Invoke(coin.Value);
                coin.Destroy();
            }
        }

        private void ReadInput(Vector2 input)
        {
            var canJump = input == Vector2.up && _isGrounded;
            var canFallDown = input == Vector2.down && !_isGrounded;

            if (canJump)
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            else if (canFallDown)
                _rigidbody.AddForce(Vector3.down * _fallForce, ForceMode.Impulse);

            _animator.SetBool("Grounded", _isGrounded);
            ChangeLine(input);
        }
        private void Move()
        {
            var additionSpeed = (Time.time - _startTime) * _increaseSpeed;
            var speed = _speed + Mathf.Ceil(additionSpeed);
            var moveDirection = new Vector3(0, 0, speed);
            var xPosition = Mathf.MoveTowards(transform.position.x, _currentLine * _lineStep, _sideSpeed * Time.deltaTime);

            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
            transform.Translate(moveDirection * Time.deltaTime);
        }
        private void ChangeLine(Vector2 input)
        {
            var isLineLessThanMax = _currentLine < _linesCount - 1;
            var isLineBiggerThanMin = _currentLine > 0;

            if (input.x > 0 && isLineLessThanMax)
                _currentLine++;
            else if(input.x < 0 && isLineBiggerThanMin)
                _currentLine--;
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
}
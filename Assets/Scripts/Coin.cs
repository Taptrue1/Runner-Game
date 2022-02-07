using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    internal class Coin : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private float _animationDuration;

        public int Value => _value;

        private Tween _moveTween;
        private Tween _rotateTween;

        private void Awake()
        {
            Animate();
        }
        private void OnDestroy()
        {
            _moveTween.Kill();
            _rotateTween.Kill();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void Animate()
        {
            var startPosition = transform.position;
            _rotateTween = transform.DOLocalRotate(new Vector3(90, 0, 360), _animationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1)
                .SetEase(Ease.Linear);

            var moveTween = DOTween.Sequence();
            moveTween.Append(transform.DOMoveY(startPosition.y + 1, _animationDuration).SetEase(Ease.InOutSine));
            moveTween.Append(transform.DOMoveY(startPosition.y, _animationDuration).SetEase(Ease.InOutSine));
            moveTween.SetLoops(-1);

            _moveTween = moveTween;
        }
    }
}

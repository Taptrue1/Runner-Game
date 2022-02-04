using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Assets.Scripts
{
    internal class ScoreView : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI _text;
        [Header("Settings")]
        [SerializeField] private string _format;
        [SerializeField] private float _maxAnimateScale;
        [SerializeField] private float _animationDuration;

        private Sequence _sequence;
        private RectTransform _textTransform;

        private void Start()
        {
            _textTransform = _text.gameObject.GetComponent<RectTransform>();
        }

        public void Init(Score score)
        {
            score.ScoreChanged += OnScoreChanged;
        }
        public void AnimateText()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(_textTransform.DOScale(_maxAnimateScale, _animationDuration));
            _sequence.Append(_textTransform.DOScale(1, _animationDuration));

            _sequence.AppendCallback(() => { _sequence.Kill(); });
        }

        private void OnScoreChanged(int value)
        {
            _text.text = string.Format(_format, value);
        }
    }
}

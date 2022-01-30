using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private string _scoreTextFormat;
    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private string _coinTextFormat;

    public void Init(ScoreCounter scoreCounter, Money money)
    {
        scoreCounter.ScoreChanged += OnScoreChanged;
        money.MoneyChanged += OnMoneyChanged;
    }
    public void OnScoreChanged(int score)
    {
        _scoreText.text = string.Format(_scoreTextFormat, score);
    }
    public void OnMoneyChanged(int coins)
    {
        _coinText.text = string.Format(_coinTextFormat, coins);
    }
}

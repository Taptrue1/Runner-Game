using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private string _recordTextFormat;
    [SerializeField] private int _thisSceneIndex;

    private float _scoreRecord;
    private const string _saveKey = "kkll1";

    private void Awake()
    {
        Time.timeScale = 0;
        _scoreRecord = PlayerPrefs.GetFloat(_saveKey, 0);
        _recordText.text = _scoreRecord.ToString();
    }
    private void FixedUpdate()
    {
        if (!_player.activeSelf)
        {
            SaveScore();
            SceneManager.LoadScene(_thisSceneIndex);
        }
    }
    private void OnApplicationQuit()
    {
        SaveScore();
    }

    public void StartGame()
    {
        _menuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void SaveScore()
    {
        if(_scoreCounter.Score > _scoreRecord)
        {
            PlayerPrefs.SetFloat(_saveKey, _scoreCounter.Score);
        }
    }
}

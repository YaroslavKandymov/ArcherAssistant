using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private StartPanel _startPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private AmplitudeAnalytics _amplitudeAnalytics;

    public int LevelNumber => _levelNumber;

    public event Action Started;
    public event Action Complete;
    public event Action Lost;
    public event Action Restarted;

    private void Awake()
    {
        if (_levelNumber == 1)
            _amplitudeAnalytics.Init();
    }

    private void OnEnable()
    {
        _startPanel.LevelStarted += OnLevelStarted;
        _endGamePanel.LevelComplete += OnLevelComplete;
        _endGamePanel.LevelLost += OnLevelLost;
        _losePanel.LevelRestarted += OnLevelRestarted;
    }

    private void OnDisable()
    {
        _startPanel.LevelStarted -= OnLevelStarted;
        _endGamePanel.LevelComplete -= OnLevelComplete;
        _endGamePanel.LevelLost -= OnLevelLost;
        _losePanel.LevelRestarted -= OnLevelRestarted;
    }

    private void OnLevelStarted()
    {
        Started?.Invoke();
    }

    private void OnLevelComplete()
    {
        Complete?.Invoke();
    }

    private void OnLevelLost()
    {
        Lost?.Invoke();
    }

    private void OnLevelRestarted()
    {
        Restarted?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private StartPanel _startPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private AmplitudeAnalytics _amplitudeAnalytics;

    private float _time;
    private Coroutine _coroutine;

    public int LevelNumber => _levelNumber;

    public event Action<Dictionary<string, object>> Started;
    public event Action<Dictionary<string, object>> Complete;
    public event Action<Dictionary<string, object>> Lost;
    public event Action<Dictionary<string, object>> Restarted;

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
        Dictionary<string, object> startDictionary = new Dictionary<string, object>
        {
            { "level", _levelNumber}
        };

        _time = 0;
        _coroutine = StartCoroutine(CalculateGameTime());

        Started?.Invoke(startDictionary);
    }

    private void OnLevelComplete()
    {
        StopCoroutine(_coroutine);

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            { "level", _levelNumber},
            { "time_spent", _time}
        };

        Complete?.Invoke(dictionary);
    }

    private void OnLevelLost()
    {
        Dictionary<string, object> lostDictionary = new Dictionary<string, object>
        {
            { "level", _levelNumber},
            { "reason", "death"},
            { "time_spent", _time}
        };

        Debug.Log(_time);

        StopCoroutine(_coroutine);
        Lost?.Invoke(lostDictionary);
    }

    private void OnLevelRestarted()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            { "level", _levelNumber}
        };

        _time = 0;
        _coroutine = StartCoroutine(CalculateGameTime());

        Restarted?.Invoke(dictionary);
    }

    private IEnumerator CalculateGameTime()
    {
        while (true)
        {
            _time += Time.deltaTime;

            yield return null;
        }
    }
}

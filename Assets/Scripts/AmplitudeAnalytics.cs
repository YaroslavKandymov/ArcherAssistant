using System;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAnalytics : MonoBehaviour
{
    private const string SAVED_REG_DAY = "RegDay";
    private const string SAVED_REG_DAY_FULL = "RegDayFull";
    private const string SAVED_SESSION_ID = "SessionID";

    [SerializeField] private Level _level;

    private Dictionary<string, object> dictionary = new Dictionary<string, object>();

    private string _regDay
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY, DateTime.Today.ToString("dd/MM/yy")); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY, value); }
    }

    private string _regDayFull
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY_FULL, DateTime.Today.ToString()); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY_FULL, value); }
    }

    private int _sessionID
    {
        get { return PlayerPrefs.GetInt(SAVED_SESSION_ID, 0); }
        set { PlayerPrefs.SetInt(SAVED_SESSION_ID, value); }
    }

    private void OnEnable()
    {
        _level.Started += OnStarted;
        _level.Complete += OnComplete;
        _level.Lost += OnlLost;
        _level.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _level.Started -= OnStarted;
        _level.Complete -= OnComplete;
        _level.Lost -= OnlLost;
        _level.Restarted -= OnRestarted;
    }

    public void Init()
    {
        Amplitude.Instance.logging = true;
        Amplitude.Instance.init("cc85ab3072833eb80afbf5973f2637e6");

        GameStart();
    }

    private void GameStart()
    {
        if (_level.LevelNumber == 1)
        {
            _regDay = DateTime.Today.ToString("dd/MM/yy");
            _regDayFull = DateTime.Today.ToString();
            Amplitude.Instance.setOnceUserProperty("reg_day", _regDay);
        }

        SetStartedProperties();

        FireEvent("game_start", dictionary);
    }

    private void SetStartedProperties()
    {
        _sessionID = _sessionID + 1;
        Amplitude.Instance.setUserProperty("session_id", _sessionID);

        int daysAfter = DateTime.Today.Subtract(DateTime.Parse(_regDayFull)).Days;
        Amplitude.Instance.setUserProperty("days_after", daysAfter);
    }

    private void OnStarted()
    {
        FireEvent("level_start", new Dictionary<string, object>());
    }

    private void OnComplete()
    {
        FireEvent("level_complete", new Dictionary<string, object>());
    }

    private void OnlLost()
    {
        FireEvent("fail", new Dictionary<string, object>());
    }

    private void OnRestarted()
    {
        FireEvent("restart", new Dictionary<string, object>());
    }

    private void FireEvent(string eventName, Dictionary<string, object> dictionary)
    {
        SettingUserProperties();

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    private void SettingUserProperties()
    {
        int lastLevel = _level.LevelNumber;
        Amplitude.Instance.setUserProperty("level_last", lastLevel);
    }
}

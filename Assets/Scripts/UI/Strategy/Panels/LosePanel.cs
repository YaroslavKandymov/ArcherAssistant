using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : Panel
{
    [SerializeField] private float _duration;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private ArcherAssistant[] _assistants;
    [SerializeField] private Archer[] _archers;
    
    private List<Quiver> _quivers = new List<Quiver>();

    public event Action LevelRestarted;

    private void Awake()
    {
        InitBehaviors();
        PanelCloser.Close(this);

        foreach (var archer in _archers)
            _quivers.Add(archer.GetComponent<Quiver>());

        foreach (var assistant in _assistants)
            _quivers.Add(assistant.GetComponent<Quiver>());
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected override void InitBehaviors()
    {
        GameCloser = new CloseApplicationBehavior();
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
        Reloader = new RestartSceneBehavior();
    }

    private void OnRestartButtonClick()
    {
        _restartButton.transform.DOScale(_restartButton.transform.localScale * 0.8f, _duration);
        PanelCloser.Close(this, true, _duration);

        StartCoroutine(OpenGamePanel());
    }

    private void OnExitButtonClick()
    {
        _exitButton.transform.DOScale(_exitButton.transform.localScale * 0.8f, _duration);
        GameCloser.Close();
    }

    private IEnumerator OpenGamePanel()
    {
        yield return new WaitForSeconds(_duration);

        PanelOpener.Open(_gamePanel, false);

        Reloader.Restart(_assistants, _archers, _quivers);

        LevelRestarted?.Invoke();
    }
}

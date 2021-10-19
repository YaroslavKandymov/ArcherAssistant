using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : Panel
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private ArrowsSpawner[] _spawners;
    [SerializeField] private ArcherAssistant[] _assistants;
    [SerializeField] private EnemyAssistantArrowCollector _collector;
    [SerializeField] private Archer[] _archers;
    
    private List<Quiver> _quivers = new List<Quiver>();

    public event Action SceneRestarted;

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
        PanelCloser = new CloseBehavior();
        Reloader = new RestartSceneBehavior();
    }

    private void OnRestartButtonClick()
    {
        PanelCloser.Close(this, true);
        PanelOpener.Open(_gamePanel, false);

        Reloader.Restart(_assistants, _archers, _collector, _quivers);

        SceneRestarted?.Invoke();
    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}

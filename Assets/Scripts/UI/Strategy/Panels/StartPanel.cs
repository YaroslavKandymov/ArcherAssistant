using System;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Panel
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GamePanel _gamePanel;

    public event Action LevelStarted;

    private void Awake()
    {
        InitBehaviors();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void Start()
    {
        PanelOpener.Open(this, true);
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
    }

    private void OnStartButtonClick()
    {
        PanelCloser.Close(this, true);
        PanelOpener.Open(_gamePanel);

        LevelStarted?.Invoke();
    }
}

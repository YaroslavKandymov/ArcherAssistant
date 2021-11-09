using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : Panel
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GamePanel _gamePanel;
    [SerializeField] private float _duration;

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
        _startButton.transform.DOScale(_startButton.transform.localScale * 0.8f, _duration);

        PanelCloser.Close(this,  true, _duration);
        StartCoroutine(OpenStartPanel());

    }

    private IEnumerator OpenStartPanel()
    {
        yield return new WaitForSeconds(_duration);

        PanelOpener.Open(_gamePanel);
        LevelStarted?.Invoke();
    }
}

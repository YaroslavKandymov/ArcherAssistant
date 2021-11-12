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
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private ArcherAssistant[] _assistants;
    [SerializeField] private Archer[] _archers;
    
    private List<Quiver> _quivers = new List<Quiver>();
    private Vector3 _restartButtonDefaultScale;

    public event Action LevelRestarted;

    private void Awake()
    {
        InitBehaviors();
        PanelCloser.Close(this);

        foreach (var archer in _archers)
            _quivers.Add(archer.GetComponent<Quiver>());

        foreach (var assistant in _assistants)
            _quivers.Add(assistant.GetComponent<Quiver>());

        _restartButtonDefaultScale = _restartButton.transform.localScale;
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
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

    private IEnumerator OpenGamePanel()
    {
        yield return new WaitForSeconds(_duration);

        Time.timeScale = 1;

        LevelRestarted?.Invoke();
        Reloader.Restart(_assistants, _archers, _quivers);

        var arrows = GameObject.FindObjectsOfType<Arrow>();

        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);

            if (arrow.Transform.parent == null)
            {
                if (arrow != null)
                {
                    arrow.Destroy();
                }
            }
        }

        PanelOpener.Open(_gamePanel, false);
        _restartButton.transform.DOScale(_restartButtonDefaultScale, _duration);
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GamePanel : Panel
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Panel _pausePanel;
    [SerializeField] private TMP_Text _text;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        InitBehaviors();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new CloseBehavior();
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;

        var buttons = new List<Button> {_pauseButton};

        PanelCloser.Close(_canvasGroup, buttons);

        var newCanvasGroup = _pausePanel.GetComponent<CanvasGroup>();
        var newButtons = _pausePanel.GetComponentsInChildren<Button>();

        PanelOpener.Open(newCanvasGroup, newButtons);
    }

    private void OnArrowsCountChanged(int count)
    {
        _text.text = count.ToString();
    }
}

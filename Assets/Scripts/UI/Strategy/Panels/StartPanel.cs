using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : Panel
{
    [SerializeField] private Button _startButton;

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
        PanelCloser = new CloseBehavior();
    }

    private void OnStartButtonClick()
    {
        PanelCloser.Close(this, true);
    }
}

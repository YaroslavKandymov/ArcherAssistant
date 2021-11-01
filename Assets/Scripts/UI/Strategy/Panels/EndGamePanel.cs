using System;
using System.Collections;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private PlayerArcherAssistantHealth _playerHealth;
    [SerializeField] private EnemyArcherHealth[] _enemyLives;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private Panel _losePanel;
    [SerializeField] private Panel _winPanel;
    [SerializeField] private float _seconds;

    public event Action LevelComplete;
    public event Action LevelLost;

    private void Awake()
    {
        InitBehaviors();
    }

    private void OnEnable()
    {
        _playerHealth.Died += OnPlayerDied;

        foreach (var enemy in _enemyLives)
            enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= OnPlayerDied;

        foreach (var enemy in _enemyLives)
            enemy.Died -= OnEnemyDied;
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
        SceneLoader = new LoadSceneBehavior();
    }

    private void OnPlayerDied()
    {
        LevelLost?.Invoke();

        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_losePanel, true);
    }

    private void OnEnemyDied(EnemyArcherHealth archer)
    {
        foreach (var enemy in _enemyLives)
            if (enemy.IsDied == false)
                return;

        LevelComplete?.Invoke();

        _playerHealth.GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(OpenWinPanel());
    }

    private IEnumerator OpenWinPanel()
    {
        yield return new WaitForSeconds(_seconds);

        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_winPanel, true);
    }
}

using System.Collections;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private PlayerArcherAssistantHealth _playerHealth;
    [SerializeField] private EnemyArcherAssistantHealth[] _enemyLives;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private Panel _losePanel;
    [SerializeField] private Panel _winPanel;

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
        PanelCloser = new CloseBehavior();
        SceneLoader = new LoadSceneBehavior();
    }

    private void OnPlayerDied()
    {
        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_losePanel, true);
    }

    private void OnEnemyDied()
    {
        foreach (var enemy in _enemyLives)
            if(enemy.IsDied == false)
                return;

        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_winPanel, true);
    }
}

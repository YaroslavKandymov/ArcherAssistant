using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private PlayerArcherAssistantHealth _playerHealth;
    [SerializeField] private EnemyArcherAssistantHealth[] _enemyLives;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private Panel _gameOverPanel;
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
        StopTime();
        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_gameOverPanel);
    }

    private void OnEnemyDied()
    {
        foreach (var enemy in _enemyLives)
            if(enemy.IsDied == false)
                return;

        StopTime();
        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_winPanel);
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }
}

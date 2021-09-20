using System.Collections;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private PlayerArcherAssistantHealth _playerHealth;
    [SerializeField] private EnemyArcherAssistantHealth[] _enemyLives;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private Panel _gameOverPanel;
    [SerializeField] private Panel _winPanel;
    [SerializeField] private float _seconds;

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
        StartCoroutine(StopTime(0));
        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_gameOverPanel);
    }

    private void OnEnemyDied()
    {
        foreach (var enemy in _enemyLives)
            if(enemy.IsDied == false)
                return;

        StartCoroutine(StopTime(_seconds));
        PanelCloser.Close(_gamePanel);
        PanelOpener.Open(_winPanel);
    }

    private IEnumerator StopTime(float seconds)
    {
        yield return new WaitForSeconds(_seconds);

        Time.timeScale = 0;
    }
}

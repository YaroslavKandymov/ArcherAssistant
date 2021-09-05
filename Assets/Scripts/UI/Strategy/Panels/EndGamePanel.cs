using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private PlayerArcherAssistantHealth _playerHealth;
    [SerializeField] private EnemyArcherAssistantHealth _enemyHealth;

    private void OnEnable()
    {
        _playerHealth.PlayerDied += OnPlayerDied;
        _enemyHealth.EnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        _playerHealth.PlayerDied -= OnPlayerDied;
        _enemyHealth.EnemyDied -= OnEnemyDied;
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new CloseBehavior();
    }

    private void OnPlayerDied()
    {
        StopTime();
    }

    private void OnEnemyDied()
    {
        StopTime();
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }
}

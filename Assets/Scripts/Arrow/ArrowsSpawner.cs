using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowsSpawner : ObjectPool<Arrow>
{
    [SerializeField] private Arrow _arrowTemplate;
    [SerializeField] private ArrowStates _arrowState;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _randomCorner;
    [SerializeField] private int _startArrowsCount;

    private SpawnPoint[] _spawnPoints;
    private WaitForSeconds _seconds;

    private void Start()
    {
        Initialize(_arrowTemplate);

        _spawnPoints = GetComponentsInChildren<SpawnPoint>();

        _seconds = new WaitForSeconds(_secondsBetweenSpawn);

        PositionStartArrows(_startArrowsCount);

        StartCoroutine(Spawn());
    }

    public void Reset()
    {
        PositionStartArrows(_startArrowsCount);
    }

    private void PositionStartArrows(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PositionArrow();
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        { 
            PositionArrow();

            yield return _seconds;
        }
    }

    private void PositionArrow()
    {
        if (TryGetObject(out Arrow arrow))
        {
            arrow.ArrowState = _arrowState;
            var randomPoint = Random.Range(0, _spawnPoints.Length);
            arrow.Transform.position = _spawnPoints[randomPoint].Transform.position;
            arrow.Transform.localEulerAngles = new Vector3(90 + Random.Range(-_randomCorner, _randomCorner), 0f,
                90 + Random.Range(-_randomCorner, _randomCorner));
            arrow.Transform.parent = _spawnPoints[randomPoint].Transform;

            arrow.gameObject.SetActive(true);
        }
    }
}

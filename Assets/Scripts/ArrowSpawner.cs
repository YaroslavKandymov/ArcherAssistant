using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowSpawner : ObjectPool<Arrow>
{
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _randomCorner;

    private Transform[] _spawnPoints;
    private WaitForSeconds _seconds;
    private List<Arrow> _arrows = new List<Arrow>();
    private Coroutine _coroutine;

    private void Start()
    {
        Initialize(_arrowPrefab);

        _spawnPoints = new Transform[_spawnPoint.childCount];

        for (int i = 0; i < _spawnPoint.childCount; i++)
            _spawnPoints[i] = _spawnPoint.GetChild(i);

        _seconds = new WaitForSeconds(_secondsBetweenSpawn);

        _coroutine = StartCoroutine(Spawn());
    }

    public void Restart()
    {
        foreach (var arrow in _arrows)
            arrow.gameObject.SetActive(false);

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (TryGetObject(out Arrow arrow))
            {
                arrow.ArrowState = ArrowStates.NotKiller;
                var randomNumber = Random.Range(0, _spawnPoints.Length);
                arrow.transform.position = _spawnPoints[randomNumber].position;
                arrow.transform.localEulerAngles = new Vector3(90 + Random.Range(-_randomCorner, _randomCorner), 0f,
                    90 + Random.Range(-_randomCorner, _randomCorner));
                arrow.gameObject.SetActive(true);
                _arrows.Add(arrow);
            }

            yield return _seconds;
        }
    }
}

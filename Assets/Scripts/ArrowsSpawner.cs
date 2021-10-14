using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowsSpawner : MonoBehaviour
{
    [SerializeField] private Arrow _arrowTemplate;
    [SerializeField] private ArrowStates _arrowState;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _randomCorner;
    [SerializeField] private Transform _spawner;
    [SerializeField] private int _startArrowsCount;

    private Transform[] _spawnPoints;
    private WaitForSeconds _seconds;

    public event Action<Arrow> ArrowSpawned;

    private void Start()
    {
        

        _spawnPoints = GetComponentsInChildren<Transform>();

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
        var arrow = Instantiate(_arrowTemplate, _spawner);
        arrow.ArrowState = _arrowState;
        var randomPoint = Random.Range(0, _spawnPoints.Length);
        arrow.transform.position = _spawnPoints[randomPoint].transform.position;
        arrow.transform.localEulerAngles = new Vector3(90 + Random.Range(-_randomCorner, _randomCorner), 0f,
            90 + Random.Range(-_randomCorner, _randomCorner));

        gameObject.SetActive(true);

        ArrowSpawned?.Invoke(arrow);
    }
}

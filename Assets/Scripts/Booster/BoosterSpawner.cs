using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoosterSpawner : MonoBehaviour
{
    private BoosterLifetime[] _boosters;
    private float _lastSpawnTime;
    private float _existedTime;

    private void Awake()
    {
        _boosters = GetComponentsInChildren<BoosterLifetime>();

        foreach (var booster in _boosters)
        {
            booster.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach (var booster in _boosters)
        {
            booster.GetComponent<ArrowBooster>().Taken += OnTaken;
        } 
    }

    private void OnDisable()
    {
        foreach (var booster in _boosters)
        {
            booster.GetComponent<ArrowBooster>().Taken -= OnTaken;
        }
    }

    private void Start()
    {
        foreach (var booster in _boosters)
        {
            StartCoroutine(Spawn(booster));
        }
    }

    private void OnTaken(ArrowBooster booster)
    {
        booster.gameObject.SetActive(false);
        _existedTime = 0;
        _lastSpawnTime = booster.GetComponent<BoosterLifetime>().SpawnInterval;
    }

    private IEnumerator Spawn(BoosterLifetime booster)
    {
        var strokeLine = booster.GetComponentInChildren<BoosterStrokeLine>();

        while (true)
        {
            if (_lastSpawnTime <= 0)
            {
                if (booster.gameObject.activeSelf == false)
                {
                    if (_existedTime <= 0)
                    {
                        _existedTime = booster.ExistTime;
                        booster.gameObject.SetActive(true);
                        strokeLine.Draw(booster.ExistTime);
                    }
                }
                else
                {
                    _existedTime -= Time.deltaTime;

                    if (_existedTime <= 0)
                    {
                        booster.gameObject.SetActive(false);
                        _lastSpawnTime = booster.SpawnInterval;
                    }
                }
            }

            _lastSpawnTime -= Time.deltaTime;

            yield return null;
        }
    }
}

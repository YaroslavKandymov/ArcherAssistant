using System.Collections;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _seconds;
    [SerializeField] private float _particleHeight;

    private WaitForSeconds _time;
    private BoosterLifetime[] _boosters;
    private float _lastSpawnTime;
    private float _existedTime;

    private void Awake()
    {
        _boosters = GetComponentsInChildren<BoosterLifetime>();
        _time = new WaitForSeconds(_seconds);
        _particleSystem.gameObject.SetActive(false);

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
        _particleSystem.gameObject.transform.position = new Vector3(booster.transform.position.x, booster.transform.position.y + _particleHeight, booster.transform.position.z);
        StartCoroutine(PlayParticle());
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

    private IEnumerator PlayParticle()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        yield return _time;

        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}

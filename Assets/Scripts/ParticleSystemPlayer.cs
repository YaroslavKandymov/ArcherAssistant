using System.Collections;
using UnityEngine;

public class ParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _playTime;
    [SerializeField] private PlayerArrowsGiver _playerArrowsGiver;

    private ParticleSystem _particleSystem;
    private WaitForSeconds _time;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _playerArrowsGiver.ArrowGiven += OnArrowGiven;
    }

    private void OnDisable()
    {
        _playerArrowsGiver.ArrowGiven -= OnArrowGiven;
    }

    private void Start()
    {
        _time = new WaitForSeconds(_playTime);
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.transform.position = _spawnPoint.transform.position;
        _particleSystem.gameObject.SetActive(false);
    }

    private void OnArrowGiven(Arrow arrow)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PlayEffect());
    }

    private IEnumerator PlayEffect()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        yield return _time;

        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}

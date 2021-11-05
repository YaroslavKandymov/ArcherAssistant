using System.Collections;
using UnityEngine;

public class DissapearEffect : MonoBehaviour
{
    [SerializeField] private PlayerArrowCollector _arrowCollector;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _seconds;

    private WaitForSeconds _time;
    private Transform _particleSystemTransform;

    private void Awake()
    {
        _time = new WaitForSeconds(_seconds);
        _particleSystemTransform = _particleSystem.gameObject.transform;
    }

    private void OnEnable()
    {
        _arrowCollector.ArrowDeparted += OnArrowDeparted;
    }

    private void OnDisable()
    {
        _arrowCollector.ArrowDeparted -= OnArrowDeparted;
    }

    private void OnArrowDeparted(Vector3 position)
    {
        StartCoroutine(Play(position));
    }

    private IEnumerator Play(Vector3 position)
    {
        _particleSystemTransform.position = position;
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        yield return _time;

        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using UnityEngine;

public class ArcherShooter : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _shootPoint;

    private ArrowMover _currentArrow;
    private float _lastShootTime;

    public event Action ArrowsEnded;

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowCountChanged;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowCountChanged;
    }

    private void OnArrowCountChanged()
    {
        //StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (_lastShootTime <= 0)
            {
                _currentArrow = _quiver.TryGetArrow(_shootPoint);

                if (_currentArrow == null)
                {
                    Debug.Log("Стрелы кончились");
                    ArrowsEnded?.Invoke();
                    StopCoroutine(Shoot());
                }
                else
                {
                    _currentArrow.Shoot(_targets);
                }

                _lastShootTime = _secondsBetweenShot;
            }

            _lastShootTime -= Time.deltaTime;

            yield return null;
        }
    }
}

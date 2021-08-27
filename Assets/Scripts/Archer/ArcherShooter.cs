using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherShooter : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _secondsBeforeShot;

    private Arrow _currentArrow;
    private float _lastShootTime;
    private Animator _animator;

    public event Action ArrowsEnded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_lastShootTime <= 0)
        {
            _animator.SetTrigger(ArcherAnimatorController.Params.GetArrow);
            _currentArrow = _quiver.TryGetArrow();

            if (_currentArrow == null)
            {
                ArrowsEnded?.Invoke();
            }
            else
            {
                StartCoroutine(Shoot());
            }

            _lastShootTime = _secondsBetweenShot;
        }

        _lastShootTime -= Time.deltaTime;
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds seconds = new WaitForSeconds(_secondsBeforeShot);

        _animator.SetTrigger(ArcherAnimatorController.Params.Shot);

        yield return seconds;

        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.Shoot(_targets);
    }
}

using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherShooter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _secondsBeforeShot;
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private ArrowStates _arrowState;

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
        _animator.SetTrigger(ArcherAnimatorController.Params.Shot);

        yield return new WaitForSeconds(_secondsBeforeShot);

        if (_currentArrow == null)
            yield break;

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.Shoot(_target);
    }
}

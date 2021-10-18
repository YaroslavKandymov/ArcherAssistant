
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyArcherShooter : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _secondsBeforeShot;
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private ArrowStates _arrowState; 
    
    private EnemyRay _ray;
    private Arrow _currentArrow;
    private float _lastShootTime;
    private Animator _animator;
    private Quiver _quiver;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _quiver = GetComponent<Quiver>();
        _ray = GetComponentInChildren<EnemyRay>();
    }

    private void Update()
    {
        if (_lastShootTime <= 0)
        {
            _animator.SetTrigger(ArcherAnimatorController.Params.GetArrow);
            _currentArrow = _quiver.TryGetArrow();
            _ray.gameObject.SetActive(true);

            if (_currentArrow == null)
            {
                return;
            }
            else
            {
                StartCoroutine(TargetShot());
            }

            _lastShootTime = _secondsBetweenShot + Random.Range(-0.5f, 0.5f);
        }

        _lastShootTime -= Time.deltaTime;
    }

    private IEnumerator TargetShot()
    {
        _animator.SetTrigger(ArcherAnimatorController.Params.Shot);

        yield return new WaitForSeconds(_secondsBeforeShot);

        if (_currentArrow == null)
            yield break;

        _ray.gameObject.SetActive(false);

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.TargetShot(_targetPoint);
    }
}

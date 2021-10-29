using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyArcherShooter : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _getArrowSeconds;
    [SerializeField] private float _secondsBeforeRelease;
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private ArrowStates _arrowState; 
    
    private EnemyRay _ray;
    private Arrow _currentArrow;
    private float _lastShootTime;
    private Animator _animator;
    private Quiver _quiver;
    private WaitForSeconds _timeToGetArrow;
    private WaitForSeconds _timeBeforeRelease;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _quiver = GetComponent<Quiver>();
        _ray = GetComponentInChildren<EnemyRay>();
        _timeToGetArrow = new WaitForSeconds(_getArrowSeconds);
        _timeBeforeRelease = new WaitForSeconds(_secondsBeforeRelease);
        _lastShootTime = Random.Range(0.5f, 2.5f);
    }

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(EnemyArcherAnimatorController.States.TakeDamage))
        {
            return;
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(EnemyArcherAnimatorController.States.Death))
            return;

        if (_lastShootTime <= 0)
        {
            _animator.SetTrigger(EnemyArcherAnimatorController.Params.GetArrow);
            _currentArrow = _quiver.TryGetArrow();

            if (_currentArrow == null)
            {
                return;
            }
            else
            {
                StartCoroutine(TargetShot());
            }

            _lastShootTime = _secondsBetweenShot + Random.Range(-0.5f, 2.5f);
        }

        _lastShootTime -= Time.deltaTime;
    }

    private IEnumerator TargetShot()
    {
        yield return _timeToGetArrow;

        if (_currentArrow == null)
            yield break;

        _animator.SetTrigger(EnemyArcherAnimatorController.States.Hold);
        _ray.Activate();

        yield return _timeBeforeRelease;

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.TargetShot(_targetPoint);

        _animator.SetTrigger(EnemyArcherAnimatorController.Params.Release);
    }
}

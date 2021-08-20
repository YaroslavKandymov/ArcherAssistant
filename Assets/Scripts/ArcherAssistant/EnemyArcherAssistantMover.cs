using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyArcherAssistantMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private readonly List<Arrow> _enemyArrows = new List<Arrow>();
    private Arrow _currentArrow;

    private void Init(Arrow arrow)
    {
        _enemyArrows.Add(arrow);
    }

    public void AddArrow(Arrow arrow)
    {
        if(arrow == null)
            throw new NullReferenceException();

        if (arrow.ArrowState == ArrowStates.NotKiller)
            _enemyArrows.Add(arrow);

        _currentArrow = _enemyArrows.FirstOrDefault(a => a.gameObject.activeSelf == true);
    }

    private void OnArrowMissed()
    {
        TakeArrow();
    }

    private void TakeArrow()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentArrow.transform.position, _speed);
        _currentArrow.GetComponent<ArrowMover>().ArrowMissed -= OnArrowMissed;
        _enemyArrows.Remove(_currentArrow);
    }
}

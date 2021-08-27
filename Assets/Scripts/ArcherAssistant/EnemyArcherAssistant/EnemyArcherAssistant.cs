using UnityEngine;

[RequireComponent(typeof(EnemyArrowCollector))]
public class EnemyArcherAssistant : ArcherAssistant
{
    [SerializeField] private Ground _ground; 
    
    private EnemyArrowCollector _arrowCollector;

    private void OnEnable()
    {
        _ground.ArrowLanded += OnArrowLanded;
    }

    private void OnDisable()
    {
        _ground.ArrowLanded -= OnArrowLanded;
    }

    private void Start()
    {
        _arrowCollector = GetComponent<EnemyArrowCollector>();
    }

    private void OnArrowLanded(Arrow arrow)
    {
        _arrowCollector.Take(arrow);
    }
}

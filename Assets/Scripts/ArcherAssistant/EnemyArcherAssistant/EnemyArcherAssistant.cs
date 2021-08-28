using UnityEngine;

[RequireComponent(typeof(EnemyArrowCollector))]
public class EnemyArcherAssistant : ArcherAssistant
{
    private ICollector _arrowCollector;

    private void Start()
    {
        _arrowCollector = GetComponent<EnemyArrowCollector>();
    }

    protected override void OnArrowLanded(Arrow arrow)
    {
        _arrowCollector.Collect(arrow);
    }
}

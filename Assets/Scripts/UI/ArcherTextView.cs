using UnityEngine;

public class ArcherTextView : MonoBehaviour
{
    [SerializeField] private Canvas _text;
    [SerializeField] private ArcherShooter _archerShooter;
    [SerializeField] private Archer _archer;

    private void OnEnable()
    {
        _archerShooter.ArrowsEnded += OnArrowsEnded;
        _archer.ArrowsIncreased += OnArrowsIncreased;
    }

    private void OnDisable()
    {
        _archerShooter.ArrowsEnded -= OnArrowsEnded;
        _archer.ArrowsIncreased -= OnArrowsIncreased;
    }

    private void OnArrowsEnded()
    {
        _text.gameObject.SetActive(true);
    }

    private void OnArrowsIncreased()
    {
        _text.gameObject.SetActive(false);
    }
}

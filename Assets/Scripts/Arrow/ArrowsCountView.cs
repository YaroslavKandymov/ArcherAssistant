using TMPro;
using UnityEngine;

public class ArrowsCountView : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
    }

    private void OnArrowsCountChanged(int count)
    {

    }
}

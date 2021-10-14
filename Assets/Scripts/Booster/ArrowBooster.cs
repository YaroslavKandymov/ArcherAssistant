using UnityEngine;

public class ArrowBooster : MonoBehaviour
{
    [SerializeField] private int _coefficient;
    [SerializeField] private Arrow _arrowTemplate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerArcherAssistant player))
        {
            var quiver = player.GetComponent<Quiver>();
            var newArrowsCount = quiver.ArrowsCount * _coefficient;
            var count = newArrowsCount / _coefficient;

            for (int i = 0; i < count; i++)
            {
                var newArrow = Instantiate(_arrowTemplate, gameObject.transform);
                newArrow.gameObject.SetActive(false);

                quiver.Add(newArrow);
            }

            gameObject.SetActive(false);
        }
    }
}
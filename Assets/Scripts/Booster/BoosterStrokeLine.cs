using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoosterStrokeLine : MonoBehaviour
{
    private Image _strokeLine;

    private void Awake()
    {
        _strokeLine = GetComponent<Image>();
    }

    public void Draw(float drawTime)
    {
        Fill();

        StartCoroutine(DrawLine(drawTime));
    }

    private void Fill()
    {
        _strokeLine.fillAmount = 1;
    }

    private IEnumerator DrawLine(float drawTime)
    {
        float elapsed = 0;
        float line;

        while (elapsed < drawTime)
        {
            line = Mathf.Lerp(1, 0, elapsed / drawTime);

            _strokeLine.fillAmount = line;
            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}

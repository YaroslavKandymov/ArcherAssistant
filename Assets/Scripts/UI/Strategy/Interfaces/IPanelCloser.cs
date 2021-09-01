using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPanelCloser
{
    void Close(CanvasGroup canvasGroup, IEnumerable<Button> buttons);
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICloserPanel
{
    void Close(CanvasGroup canvasGroup, IEnumerable<Button> buttons);
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPanelOpener
{
    void Open(CanvasGroup canvasGroup, IEnumerable<Button> buttons);
}


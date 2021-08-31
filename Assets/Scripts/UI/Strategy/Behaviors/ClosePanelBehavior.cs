using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanelBehavior : ICloserPanel
{
    public void Close(CanvasGroup canvasGroup, IEnumerable<Button> buttons)
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in buttons)
            button.interactable = false;
    }
}

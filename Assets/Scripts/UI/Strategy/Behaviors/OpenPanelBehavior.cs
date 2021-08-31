using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanelBehavior : IPanelOpener
{
    public void Open(CanvasGroup canvasGroup, IEnumerable<Button> buttons)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        foreach (var button in buttons)
            button.interactable = true;
    }
}

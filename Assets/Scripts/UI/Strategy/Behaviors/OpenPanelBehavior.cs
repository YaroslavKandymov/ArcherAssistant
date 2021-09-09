using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanelBehavior : IPanelOpener
{
    public void Open(Panel panel)
    {
        var canvasGroup = panel.GetComponent<CanvasGroup>();
        var buttons = panel.GetComponentsInChildren<Button>();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        foreach (var button in buttons)
            button.interactable = true;
    }
}

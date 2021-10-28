using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanelBehavior : IPanelCloser
{
    public void Close(Panel panel, bool continuer = false)
    {
        var canvasGroup = panel.GetComponent<CanvasGroup>();
        var buttons = panel.GetComponentsInChildren<Button>();

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in buttons)
            button.interactable = false;

        if (continuer == true)
            Time.timeScale = 1;
    }
}

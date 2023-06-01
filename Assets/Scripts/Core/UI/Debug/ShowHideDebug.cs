using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideDebug : MonoBehaviour
{
    public ViewController DebugView;
    public idContainer DebugViewId;

    [ContextMenu("ShowDebugView")]
    public void ShowDebugView()
    {
        UISystem.Instance.ShowView(DebugViewId, DebugView);
    }

    [ContextMenu("HideDebugView")]
    public void HideDebugView()
    {
        UISystem.Instance.HideView(DebugViewId);
    }
}

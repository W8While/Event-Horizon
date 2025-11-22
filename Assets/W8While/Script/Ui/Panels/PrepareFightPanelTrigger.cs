using System;
using System.Collections.Generic;
using UnityEngine;

public class PrepareFightPanelTrigger : MonoBehaviour
{
    public Action LookFightClick;
    private List<PrepareFightPanelUi> _allPrepareFightPanelUi = new List<PrepareFightPanelUi>();
    public List<PrepareFightPanelUi> AllPrepareFightPanelUi => _allPrepareFightPanelUi;

    public void NewFightPanelUiWasCreater(PrepareFightPanelUi prepareFightPanelUi)
    {
        prepareFightPanelUi.LookFightClick += LookAtFightClick;
        _allPrepareFightPanelUi.Add(prepareFightPanelUi);
    }

    private void LookAtFightClick()
    {
        LookFightClick?.Invoke();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class WarrionPanelUiTrigger : MonoBehaviour
{
    private List<WarrionPanelUi> _allWarrionPanelUi = new List<WarrionPanelUi>();

    public void AddNewWarrionPanelUi(WarrionPanelUi warrionPanelUi, FightManager fightController)
    {
        _allWarrionPanelUi.Add(warrionPanelUi);
        fightController.ChangeWarrionPanelUi += ActiveWarrionUiPanel;
        warrionPanelUi.gameObject.SetActive(false);
    }

    public void ActiveWarrionUiPanel(WarrionPanelUi warrionPanelUi)
    {
        foreach (WarrionPanelUi warrionPanel in _allWarrionPanelUi)
        {
            warrionPanel.gameObject.SetActive(false);
            if (warrionPanel == warrionPanelUi)
            {
                warrionPanel.gameObject.SetActive(true);
                warrionPanel.RefreshHealthPointBar();
            }
        }
    }
}

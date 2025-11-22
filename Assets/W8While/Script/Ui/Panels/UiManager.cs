using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private WarrionPanelUiTrigger _allWarrionPanelUi;
    [SerializeField] private PrepareFightPanelTrigger _prepareFightPanelTrigger;
    [SerializeField] private GameObject _prepareFightPanelTriggerParrentObjects;
    [SerializeField] private PlayerPanelUi _playerPanelUi;


    private void OnEnable()
    {
        _prepareFightPanelTrigger.LookFightClick += LookFightClick;
    }

    private void OnDisable()
    {
        _prepareFightPanelTrigger.LookFightClick -= LookFightClick;
    }

    private void EndFight()
    {
        ActiveOnlyPlayerPanelUi();
    }


    private void LookFightClick()
    {
        ActiveOnlyPlayerPanelUi();
        ActiveWarrionUi();
        DisablePrepareFightPanelTrigger();
    }

    public void ChangePrepareFightPanelTrigger()
    {
        if (_prepareFightPanelTriggerParrentObjects.activeSelf)
            DisablePrepareFightPanelTrigger();
        else
            ActivePrepareFightPanelTrigger();
    }


    private void ActiveOnlyPlayerPanelUi()
    {
        HideAllPanelUi();
        _playerPanelUi.gameObject.SetActive(true);
    }
    private void ActivePlayerPanelUi()
    {
        _playerPanelUi.gameObject.SetActive(true);
    }
    private void DisablePlayerPanelUi()
    {
        _playerPanelUi.gameObject.SetActive(false);
    }

    private void ActiveOnlyWarrionUi()
    {
        HideAllPanelUi();
        _allWarrionPanelUi.gameObject.SetActive(true);
    }
    private void ActiveWarrionUi()
    {
        _allWarrionPanelUi.gameObject.SetActive(true);
    }
    private void DisableWarrionUi()
    {
        _allWarrionPanelUi.gameObject.SetActive(false);
    }

    private void ActiveOnlyPrepareFightPanelTrigger()
    {
        HideAllPanelUi();
        _prepareFightPanelTriggerParrentObjects.SetActive(true);
    }
    private void ActivePrepareFightPanelTrigger()
    {
        _prepareFightPanelTriggerParrentObjects.SetActive(true);
    }
    private void DisablePrepareFightPanelTrigger()
    {
        _prepareFightPanelTriggerParrentObjects.SetActive(false);
    }



    private void HideAllPanelUi()
    {
        _playerPanelUi.gameObject.SetActive(false);
        _allWarrionPanelUi.gameObject.SetActive(false);
        _prepareFightPanelTriggerParrentObjects.SetActive(false);
    }
}

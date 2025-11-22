using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrepareFightPanelUi : MonoBehaviour
{
    [SerializeField] private Image _firstWarrionSprite;
    [SerializeField] private TMP_Text _firstWarrionName;
    [SerializeField] private TMP_Text _firstWarrionStates;
    [SerializeField] private TMP_Text _firstWarrioncCoefficient;
    [SerializeField] private TMP_InputField _firstWarrionBetInputField;

    [SerializeField] private Image _secondWarrionSprite;
    [SerializeField] private TMP_Text _secondWarrionName;
    [SerializeField] private TMP_Text _secondWarrionStates;
    [SerializeField] private TMP_Text _secondWarrioncCoefficient;
    [SerializeField] private TMP_InputField _secondWarrionBetInputField;

    [SerializeField] private TMP_Text _timerBeforeFight;
    [SerializeField] private Button _lookFightButton;
    private FightManager _FightController;

    public event Action LookFightClick;
    public event Action<WarrionSide, int, float> TryBetsOnWarrion;

    public void Init(FightManager prepareFightController, WarrionStatsScriptibleObj firstWarrion, WarrionStatsScriptibleObj secondWarrion, float firstWarrionCoefficient, float secondWarrionCoefficient)
    {
        _FightController = prepareFightController;

        _FightController.OnTimeBeforeStartChange += TimeBegoreStartChange;
        _FightController.StartFight += StartFight;
        _FightController.BetsOnWarrion += HideBetOnWarrion;

        _firstWarrionSprite.sprite = firstWarrion.CharactersSprite;
        _firstWarrionName.text = firstWarrion.CharactersName;
        _firstWarrionStates.text = firstWarrion.GetStats();
        _firstWarrioncCoefficient.text = firstWarrionCoefficient.ToString();

        _secondWarrionSprite.sprite = secondWarrion.CharactersSprite;
        _secondWarrionName.text = secondWarrion.CharactersName;
        _secondWarrionStates.text = secondWarrion.GetStats();
        _secondWarrioncCoefficient.text = secondWarrionCoefficient.ToString();
    }

    private void StartFight()
    {
        _timerBeforeFight.gameObject.SetActive(false);
        _lookFightButton.gameObject.SetActive(true);
        HideBetOnWarrion();
    }

    private void TimeBegoreStartChange(float time)
    {
        float _currentTime = Mathf.CeilToInt(time);
        _timerBeforeFight.text = _currentTime.ToString();
    }

    private void HideBetOnWarrion() 
    {
        _firstWarrionBetInputField.gameObject.SetActive(false);
        _firstWarrioncCoefficient.transform.parent.gameObject.SetActive(false);
        _secondWarrionBetInputField.gameObject.SetActive(false);
        _secondWarrioncCoefficient.transform.parent.gameObject.SetActive(false);
    }

    public void LookFightButtonClick()
    {
        LookFightClick?.Invoke();
    }

    public void LeftBetsClick()
    {
        if (_firstWarrionBetInputField.text == string.Empty)
            return;
        TryBetsOnWarrion?.Invoke(WarrionSide.Left, Convert.ToInt32(_firstWarrionBetInputField.text), (float)Convert.ToDouble(_firstWarrioncCoefficient.text));
    }

    public void RightBetsClick()
    {
        if (_secondWarrionBetInputField.text == string.Empty)
            return;
        TryBetsOnWarrion?.Invoke(WarrionSide.Right, Convert.ToInt32(_secondWarrionBetInputField.text), (float)Convert.ToDouble(_secondWarrioncCoefficient.text));
    }

    private void OnDestroy()
    {
        if (_FightController != null) 
        {
            _FightController.OnTimeBeforeStartChange -= TimeBegoreStartChange;
            _FightController.StartFight -= StartFight;
            _FightController.BetsOnWarrion -= HideBetOnWarrion;
        }
    }

}

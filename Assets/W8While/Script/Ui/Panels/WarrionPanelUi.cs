using UnityEngine;
using UnityEngine.UI;

public class WarrionPanelUi : MonoBehaviour
{
    [SerializeField] private Slider _firstWarrionHealtPoint;
    [SerializeField] private Slider _secondWarrionHealtPoint;

    private FightManager _yourFightController;

    private float _firstCurrentHp;
    private float _secondCurrentHp;
    private float _firstMaxtHp;
    private float _secondMaxHp;

    public void Init(FightManager yourFightController)
    {
        _yourFightController = yourFightController;
        _yourFightController.GetDamageWarrion += GetDamage;
        _firstCurrentHp = _secondCurrentHp = _firstCurrentHp = _secondMaxHp = 1;
    }

    private void GetDamage(WarrionSide warrionSide, float newHp, float maxHp)
    {
        if (warrionSide == WarrionSide.Left)
        {
            ChangeHealtPointBar(_firstWarrionHealtPoint, newHp, maxHp);
            _firstCurrentHp = newHp;
            _firstMaxtHp = maxHp;
        }
        else
        {
            ChangeHealtPointBar(_secondWarrionHealtPoint, newHp, maxHp);
            _secondCurrentHp = newHp;
            _secondMaxHp = maxHp;
        }
    }

    private void ChangeHealtPointBar(Slider slider, float currentHealtPoint, float maxHealtPoint)
    {
        slider.value = currentHealtPoint / maxHealtPoint;
    }

    public void RefreshHealthPointBar()
    {
        ChangeHealtPointBar(_firstWarrionHealtPoint, _firstCurrentHp, _firstMaxtHp);
        ChangeHealtPointBar(_secondWarrionHealtPoint, _secondCurrentHp, _secondMaxHp);
    }

    private void OnDestroy()
    {
        _yourFightController.GetDamageWarrion -= GetDamage;
    }
}

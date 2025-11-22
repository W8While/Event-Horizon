using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    private float _timeBeforeFightStart;

    public event Action<float> OnTimeBeforeStartChange;
    public event Action StartFight;
    public event Action<WarrionPanelUi> ChangeWarrionPanelUi;
    public event Action<WarrionSide> EndFight;
    public event Action BetsOnWarrion;
    public event Action<WarrionSide, float, float> GetDamageWarrion;

    private PrepareFightPanelUi _prepareFightPanelUi;


    private GeneralFightController _generalFightController;


    private GameObject _firstWarrion;
    private GameObject _secondWarrion;

    [SerializeField] private GameObject _fightEntropoment;
    [SerializeField] private WarrionPanelUi _warrionPanelUi;


    private Vector3 positionSpawn;

    private bool _isDoingBets;
    private WarrionSide _betWarrionSide;
    private int _betWarrionAmount;
    private float _betWarrionCoefficient;

    private FightController _fightController;

    public void Init(float timeBeforeFightStart, PrepareFightPanelUi prepareFightPanelUi, GameObject firstWarrion, GameObject secondWarrion, GeneralFightController generalFightController)
    {
        _firstWarrion = firstWarrion;
        _secondWarrion = secondWarrion;
        _timeBeforeFightStart = timeBeforeFightStart;
        _generalFightController = generalFightController;

        _fightController = GetComponent<FightController>();

        WarrionStatsScriptibleObj firstWarrionScriptibleObject = _firstWarrion.GetComponent<WarrionStats>().WarrionStatsScriptibleObject;
        WarrionStatsScriptibleObj secondWarrionScriptibleObject = _secondWarrion.GetComponent<WarrionStats>().WarrionStatsScriptibleObject;

        float[] betsThisFight = BetsCalculation.FightCoefficientCalculation(firstWarrionScriptibleObject, secondWarrionScriptibleObject);

        _prepareFightPanelUi = Instantiate(prepareFightPanelUi, Vector3.zero, Quaternion.identity, _generalFightController.PrepareFightPanelTrigger.transform);
        _prepareFightPanelUi.Init(this, firstWarrionScriptibleObject, secondWarrionScriptibleObject, betsThisFight[0], betsThisFight[1]);
        _generalFightController.PrepareFightPanelTrigger.NewFightPanelUiWasCreater(_prepareFightPanelUi);



        _prepareFightPanelUi.LookFightClick += LookFight;
        _prepareFightPanelUi.TryBetsOnWarrion += TryBetOnWarrion;


        StartCoroutine(TimeBeforeStart());
    }

    private IEnumerator TimeBeforeStart()
    {
        while (_timeBeforeFightStart > 0)
        {
            _timeBeforeFightStart -= Time.deltaTime;
            OnTimeBeforeStartChange?.Invoke(_timeBeforeFightStart);
            yield return null;
        }
        _timeBeforeFightStart = 0;
        OnTimeBeforeStartChange?.Invoke(_timeBeforeFightStart);
        StartingFight();
    }

    private void LookFight()
    {
        _generalFightController.MoveCameraToFight(positionSpawn);
        ChangeWarrionPanelUi?.Invoke(_warrionPanelUi);
    }

    private void TryBetOnWarrion(WarrionSide warironSide, int betWarrionAmount, float coefficient)
    {
        if (_generalFightController.TryBetOnWarrion(betWarrionAmount))
            BetOnWarrion(warironSide, betWarrionAmount, coefficient);
    }

    public void BetOnWarrion(WarrionSide warrionSide, int betWarrionAmount, float coefficient)
    {
        _isDoingBets = true;
        _betWarrionSide = warrionSide;
        _betWarrionAmount = betWarrionAmount;
        _betWarrionCoefficient = coefficient;

        _generalFightController.BetOnWarrion(betWarrionAmount);
        BetsOnWarrion?.Invoke();
    }

    private void StartingFight()
    {
        positionSpawn = _generalFightController.GetFightPositionSpawn();
        Instantiate(_fightEntropoment, positionSpawn, Quaternion.identity);

        _warrionPanelUi = Instantiate(_warrionPanelUi, _generalFightController.WarrionPanelUiTrigger.transform);
        _generalFightController.WarrionPanelUiTrigger.AddNewWarrionPanelUi(_warrionPanelUi, this);
        _warrionPanelUi.Init(this);

        Vector3 warrionPositionSpawn = positionSpawn;
        warrionPositionSpawn.z = 0;

        _firstWarrion = Instantiate(_firstWarrion, warrionPositionSpawn + _generalFightController.FirstWarrionPositionSpawn, Quaternion.identity);
        _secondWarrion = Instantiate(_secondWarrion, warrionPositionSpawn + _generalFightController.SecondWarrionPositionSpawn, Quaternion.identity);

        _firstWarrion.GetComponent<WarrionFight>().StartFight(_secondWarrion.transform, WarrionSide.Left);
        _secondWarrion.GetComponent<WarrionFight>().StartFight(_firstWarrion.transform, WarrionSide.Right);


        StartFight?.Invoke();
        _fightController.StartFight(_firstWarrion.GetComponent<WarrionStats>(), _secondWarrion.GetComponent<WarrionStats>());
    }


    public void WarrionTakeDamage(WarrionStats warrionStast, float newHp, float maxHp)
    {
        WarrionSide takeDamageSide = warrionStast.GetComponent<WarrionFight>().SeldSide;
        GetDamageWarrion?.Invoke(takeDamageSide, newHp, maxHp);
    }

    public void FightEnding(WarrionStats warrionStats)
    {
        WarrionSide winnerSide = warrionStats.GetComponent<WarrionFight>().SeldSide;
        EndFight?.Invoke(winnerSide);
        if (winnerSide == WarrionSide.Left)
        {
            _firstWarrion.GetComponent<WarrionFight>().WinFight();
            _secondWarrion.GetComponent<WarrionFight>().LoseFight();
        }
        if (winnerSide == WarrionSide.Right)
        {
            _secondWarrion.GetComponent<WarrionFight>().WinFight();
            _firstWarrion.GetComponent<WarrionFight>().LoseFight();
        }
        if (!_isDoingBets)
            return;
        if (winnerSide == _betWarrionSide)
            _generalFightController.WinnBets(_betWarrionAmount, _betWarrionCoefficient);
    }

    private void OnDestroy()
    {
        if (_prepareFightPanelUi != null)
        {
            _prepareFightPanelUi.LookFightClick -= LookFight;
            _prepareFightPanelUi.TryBetsOnWarrion -= TryBetOnWarrion;
        }
    }
}

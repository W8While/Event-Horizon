using System;
using System.Collections.Generic;
using UnityEngine;

public enum WarrionSide { Left, Right };

public class GeneralFightController : MonoBehaviour
{
    private const float TIMEBEFORESTART = 10f;

    [SerializeField] private List<GameObject> _allWarrion;

    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _fightManager;
    [SerializeField] private PrepareFightPanelUi _prepareFightPanel;
    [SerializeField] private BetsController _betsController;

    [SerializeField] private PrepareFightPanelTrigger _prepareFightPanelTrigger;
    public PrepareFightPanelTrigger PrepareFightPanelTrigger => _prepareFightPanelTrigger;
    [SerializeField] private WarrionPanelUiTrigger _warrionPanelUiTrigger;
    public WarrionPanelUiTrigger WarrionPanelUiTrigger => _warrionPanelUiTrigger;

    private List<FightManager> AllFight = new List<FightManager>();
    private List<int> _fightYSpawnPosition = new List<int>();

    private Vector3 _startFightPosition;
    private int _yStepForNextFightPosition;

    private Vector3 _firstWarrionPositionSpawn;
    public Vector3 FirstWarrionPositionSpawn => _firstWarrionPositionSpawn;
    private Vector3 _secondWarrionPositionSpawn;
    public Vector3 SecondWarrionPositionSpawn => _secondWarrionPositionSpawn;


    private void Start()
    {
        _startFightPosition = new Vector3(-35, 0, 1);
        _yStepForNextFightPosition = -20;
        _firstWarrionPositionSpawn = new Vector3(-6, 0, 0);
        _secondWarrionPositionSpawn = new Vector3(6, 0, 0);
    }

    public void StartPrepareFight()
    {
        GameObject fightManager = Instantiate(_fightManager, Vector3.zero, Quaternion.identity, transform);
        FightManager currentFightManager = fightManager.GetComponent<FightManager>();

        GameObject _firstWarrion = _allWarrion[UnityEngine.Random.Range(0, _allWarrion.Count)];
        GameObject _secondWarrion = _allWarrion[UnityEngine.Random.Range(0, _allWarrion.Count)];

        currentFightManager.Init(TIMEBEFORESTART, _prepareFightPanel, _firstWarrion, _secondWarrion, this);
        currentFightManager.GetComponent<FightController>().Init();

        AllFight.Add(currentFightManager);
    }


    public bool TryBetOnWarrion(int betWarrionAmount)
    {
        return _betsController.TryBetsOnWarrion(betWarrionAmount);
    }

    public void BetOnWarrion(int betWarrionAmount)
    {
        _betsController.BetsOnWarrion(betWarrionAmount);
    }

    public void WinnBets(int betAmount, float betCoefficient)
    {
        _betsController.WinerBets(betAmount, betCoefficient);
    }







    public void MoveCameraToFight(Vector3 positionSpawn)
    {
        _mainCamera.transform.position = new Vector3(positionSpawn.x, positionSpawn.y, -1);
    }

    public Vector3 GetFightPositionSpawn()
    {
        int yPos;
        for (int i = (int)_startFightPosition.y; ; i += _yStepForNextFightPosition)
        {
            bool alreadyUse = false;
            foreach (int k in _fightYSpawnPosition)
            {
                if (k == i)
                {
                    alreadyUse = true;
                    break;
                }
            }
            if (alreadyUse)
                continue;
            _fightYSpawnPosition.Add(i);
            yPos = i;
            break;
        }
        return new Vector3(_startFightPosition.x, _startFightPosition.y + yPos, _startFightPosition.z);
    }
}

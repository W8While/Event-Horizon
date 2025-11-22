using System;
using UnityEngine;

public class WarrionStats : MonoBehaviour
{
    [SerializeField] private WarrionStatsScriptibleObj _warrionStatsScriptibleObject;
    public WarrionStatsScriptibleObj WarrionStatsScriptibleObject => _warrionStatsScriptibleObject;

    private float _currentHealthPoint;
    public float CurrentHealthPoint => _currentHealthPoint;
    private float _maxHealtPointThisFight;
    public float MaxHealthPointThisFight => _maxHealtPointThisFight;


    public event Action WarrionHealtPointChange;

    public void Init(float startHp)
    {
        _currentHealthPoint = _maxHealtPointThisFight = startHp;
    }

    public void HealtPointChange(float newHealthPoint)
    {
        _currentHealthPoint = newHealthPoint;
        WarrionHealtPointChange?.Invoke();
    }

}

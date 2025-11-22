using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _startGold;
    private int _currentGold;
    public int CurrentGold => _currentGold;


    public event Action<int, int> GoldChange;

    private void Start()
    {
        SetGold(_startGold);
    }

    public void SetGold(int amount)
    {
        GoldChange?.Invoke(_currentGold, amount);
        _currentGold = amount;
    }

    public void ChangeGold(int amount)
    {
        GoldChange?.Invoke(_currentGold, _currentGold + amount);
        _currentGold += amount;
    }
}

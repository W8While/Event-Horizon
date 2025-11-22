using System;
using UnityEngine;

public class BetsController : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    public bool TryBetsOnWarrion(int betAmount)
    {
        if (betAmount <= _playerStats.CurrentGold)
        {
            return true;
        }
        return false;
    }

    public void WinerBets(int betAmount, float betCoefficient)
    {
        int summWinning = (int)Mathf.Round(betAmount * betCoefficient);
        _playerStats.ChangeGold(summWinning);
    }

    public void BetsOnWarrion(int betAmount)
    {
        _playerStats.ChangeGold(-betAmount);
    }
}

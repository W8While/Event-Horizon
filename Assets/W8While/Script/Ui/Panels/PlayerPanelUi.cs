using TMPro;
using UnityEngine;

public class PlayerPanelUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentGold;
    [SerializeField] private TMP_Text _currentHourTime;
    [SerializeField] private TMP_Text _currentMonthsTime;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GlobalStats _globalStats;

    private void OnEnable()
    {
        _playerStats.GoldChange += GoldChange;
        _globalStats.DateUpdate += DateUpdate;
    }

    private void DateUpdate(Date date)
    {
        _currentHourTime.text = $"{date.Hour:D2}:{date.Minute:D2}";
        _currentMonthsTime.text = $"{date.Day:D2}/{date.Month:D2}";
    }

    private void GoldChange(int oldGold, int currentGold)
    {
        _currentGold.text = currentGold.ToString();
    }

    private void OnDisable()
    {
        _playerStats.GoldChange -= GoldChange;
        _globalStats.DateUpdate -= DateUpdate;
    }
}

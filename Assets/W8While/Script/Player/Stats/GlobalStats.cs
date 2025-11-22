using System;
using System.Collections;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    [SerializeField] private Date _startData;
    private Date _currentData;

    private float _dataSpeed;

    public event Action<Date> DateUpdate;

    private void Start()
    {
        _currentData = _startData;
        _dataSpeed = 30;

        StartCoroutine(DataChange());
    }

    private IEnumerator DataChange()
    {
        WaitForSecondsRealtime refreshDelay = new WaitForSecondsRealtime(1f / _dataSpeed);
        while (true)
        {
            yield return refreshDelay;
            _currentData.AddTime(1);
            DateUpdate?.Invoke(new Date(_currentData.Month, _currentData.Day, _currentData.Hour, _currentData.Minute));
        }
    }
}



[Serializable]
public class Date
{
    public Date(int month, int day, int hour, int minute)
    {
        _month = month;
        _day = day;
        _hour = hour;
        _minute = minute;
    }

    [SerializeField] private int _month;
    public int Month => _month;
    [SerializeField] private int _day;
    public int Day => _day;
    [SerializeField] private int _hour;
    public int Hour => _hour;
    [SerializeField] private int _minute;
    public int Minute => _minute;

    public void AddTime(int minute = 0, int hour = 0, int day = 0, int month = 0)
    {
        _minute += minute;
        _hour += hour;
        _day += day;
        _month += month;
        CheckTime();
    }
    private void CheckTime()
    {
        while (_minute >= 60)
        {
            _minute -= 60;
            _hour++;
        }
        while (_hour >= 24)
        {
            _hour -= 24;
            _day++;
        }
        while (_day >= 30)
        {
            _day -= 30;
            _month++;
        }
    }
}
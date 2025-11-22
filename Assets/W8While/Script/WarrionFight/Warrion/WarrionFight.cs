using System;
using System.Collections;
using UnityEngine;

public class WarrionFight : MonoBehaviour
{
    private WarrionStats _stats;
    private Transform _enemyPosition;
    private bool _isAttackReady;
    private bool _isFightEnd;


    private WarrionSide _selfSide;
    public WarrionSide SeldSide => _selfSide;

    public event Action<bool> WarrionRun;
    public event Action<WarrionStats> CanAttack;
    public event Action WarrionAttack;
    public event Action<bool> FightEnd;

    private void Awake()
    {
        _stats = GetComponent<WarrionStats>();
    }

    public void StartFight(Transform enemyPosition, WarrionSide selfSide)
    {
        _isAttackReady = true;
        _isFightEnd = false;

        _selfSide = selfSide;

        _enemyPosition = enemyPosition;


        if (_selfSide == WarrionSide.Right)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }


    private void Update()
    {
        if (_isFightEnd)
            return;

        float distance = Mathf.Abs(transform.position.x - _enemyPosition.position.x);
        if (distance > _stats.WarrionStatsScriptibleObject.AttackRange)
        {
            Move(_enemyPosition.position.x);
            return;
        }
        WarrionRun?.Invoke(false);

        if (_isAttackReady)
            CanAttack?.Invoke(_stats);
        return;
    }
    private void Move(float xPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(xPosition, transform.position.y), _stats.WarrionStatsScriptibleObject.MoveSpeed * Time.deltaTime);
        WarrionRun?.Invoke(true); ;
    }

    public void ChangeAttackReady(bool what)
    {
        _isAttackReady = what;
    }

    public void AttackEnemy()
    {
        WarrionAttack?.Invoke();
    }

    public void LoseFight()
    {
        _isFightEnd = true;
        FightEnd?.Invoke(false);
    }

    public void WinFight()
    {
        _isFightEnd = true;
        FightEnd?.Invoke(true);
    }
}

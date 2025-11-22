using System;
using System.Collections;
using UnityEngine;

public class FightController : MonoBehaviour
{
    private WarrionStats _firstStats;
    private WarrionStats _secondStats;

    private FightManager _fightManager;

    public void Init()
    {
        _fightManager = GetComponent<FightManager>();
    }

    public void StartFight(WarrionStats firstStats, WarrionStats secondStats)
    {
        _firstStats = firstStats;
        _secondStats = secondStats;

        _firstStats.Init(GetStartHealtPoint(_firstStats.WarrionStatsScriptibleObject));
        _secondStats.Init(GetStartHealtPoint(_secondStats.WarrionStatsScriptibleObject));

        _firstStats.GetComponent<WarrionFight>().CanAttack += WarrionAttack;
        _secondStats.GetComponent<WarrionFight>().CanAttack += WarrionAttack;
    }

    // -----------------------------------------ATTACK---------------------------------------------

    private void WarrionAttack(WarrionStats warrionStats)
    {
        WarrionStats enemyStats = warrionStats == _firstStats ? _secondStats : _firstStats;

        float damage = CalculateDamage(warrionStats.WarrionStatsScriptibleObject);

        warrionStats.GetComponent<WarrionFight>().AttackEnemy();
        warrionStats.GetComponent<WarrionFight>().ChangeAttackReady(false);
        StartCoroutine(ReloadAttack(warrionStats.GetComponent<WarrionFight>(), warrionStats.WarrionStatsScriptibleObject.AttackSpeed));

        StartCoroutine(AttackEnemyAfterDelay(enemyStats, damage));
    }

    private IEnumerator AttackEnemyAfterDelay(WarrionStats warrionStats, float damage)
    {
        yield return new WaitForSeconds(0.2f);
        WarrionTakeDamage(warrionStats, damage);
        yield break;
    }

    private float CalculateDamage(WarrionStatsScriptibleObj _warrionStatsScriptibleObject)
    {
        float damage = UnityEngine.Random.Range(_warrionStatsScriptibleObject.MinDamage, _warrionStatsScriptibleObject.MaxDamage);
        float newDamage = AddCritDamage(_warrionStatsScriptibleObject, damage);
        return newDamage;
    }

    private float AddCritDamage(WarrionStatsScriptibleObj warrionStatsScriptibleObj, float damage)
    {
        if (warrionStatsScriptibleObj.MaxCritChance == 0)
            return damage;

        float critChance = UnityEngine.Random.Range(warrionStatsScriptibleObj.MinCritChance, warrionStatsScriptibleObj.MaxCritChance+1);
        bool isCritycalDamage = UnityEngine.Random.Range(1, 101) <= critChance;
        if (!isCritycalDamage)
            return damage;
        float critDamage = UnityEngine.Random.Range(warrionStatsScriptibleObj.MinCritDamage, warrionStatsScriptibleObj.MaxCritDamage);
        float newDamage = damage * critDamage;
        return newDamage;

    }

    private IEnumerator ReloadAttack(WarrionFight warrionFight, float attackSpeed)
    {;
        yield return new WaitForSeconds(attackSpeed);
        warrionFight.ChangeAttackReady(true);
        yield break;
    }

    // ----------------------------------------------------------------------------------------------


    // -----------------------------------------DEFENSE---------------------------------------------

    private float GetStartHealtPoint(WarrionStatsScriptibleObj warrionStatsScriptibleObj)
    {
        return UnityEngine.Random.Range(warrionStatsScriptibleObj.MinHealtPoint, warrionStatsScriptibleObj.MaxHealtPoint);
    }


    private void WarrionTakeDamage(WarrionStats warrionStats, float damage)
    {
        WarrionStats enemyStats = warrionStats == _firstStats ? _secondStats : _firstStats;

        float newDamage = DamageReduce(warrionStats.WarrionStatsScriptibleObject, damage);
        float newHealtPoint = warrionStats.CurrentHealthPoint - newDamage;

        _fightManager.WarrionTakeDamage(warrionStats, newHealtPoint, warrionStats.MaxHealthPointThisFight);

        if (newHealtPoint <= 0)
        {
            newHealtPoint = 0;
            warrionStats.HealtPointChange(newHealtPoint);
            _fightManager.FightEnding(enemyStats);
            return;
        }
        warrionStats.HealtPointChange(newHealtPoint);

    }

    private float DamageReduce(WarrionStatsScriptibleObj warrionStatsScriptibleObj, float damage)
    {
        if (EvisionChance(warrionStatsScriptibleObj))
            return 0;
        float newDamage = DamageReduceByArmor(warrionStatsScriptibleObj, damage);
        return newDamage;
    }

    private float DamageReduceByArmor(WarrionStatsScriptibleObj warrionStatsScriptibleObj, float damage)
    {
        float currentArmor = UnityEngine.Random.Range(warrionStatsScriptibleObj.MinArmor, warrionStatsScriptibleObj.MaxArmor);
        float newDamage = damage - currentArmor * 0.01f * damage;
        return newDamage;
    }

    private bool EvisionChance(WarrionStatsScriptibleObj warrionStatsScriptibleObj)
    {
        if (warrionStatsScriptibleObj.MaxEvasionChance == 0)
            return false;
        int evisionChance = UnityEngine.Random.Range(warrionStatsScriptibleObj.MinEvasionChance, warrionStatsScriptibleObj.MaxEvasionChance+1);
        bool isEvision = UnityEngine.Random.Range(1, 101) <= evisionChance;
        return isEvision;
    }


    // ----------------------------------------------------------------------------------------------
    private void OnDestroy()
    {
        if (_firstStats != null)
        {
            _firstStats.GetComponent<WarrionFight>().CanAttack -= WarrionAttack;
        }
        if (_secondStats != null)
        {
            _secondStats.GetComponent<WarrionFight>().CanAttack -= WarrionAttack;
        }
    }
}

using UnityEngine;

public class WarrionAnimation : MonoBehaviour
{
    private WarrionStats _warrionStats;
    private WarrionFight _warrionFight;
    [SerializeField] private Animator _animator;
    private void Awake()
    {
        _warrionStats = GetComponent<WarrionStats>();
        _warrionFight = GetComponent<WarrionFight>();
    }

    private void OnEnable()
    {
        _warrionStats.WarrionHealtPointChange += WarrionGetDamage;
        _warrionFight.WarrionAttack += WarrionAttack;
        _warrionFight.WarrionRun += WarrionMove;
        _warrionFight.FightEnd += EndFight;
    }

    private void WarrionMove(bool isMove)
    {
        _animator.SetBool("isRun", isMove);
    }

    private void WinFight()
    {
        _animator.SetTrigger("Win");
    }

    private void WarrionGetDamage()
    {
        //_animator.SetTrigger("—ƒ≈À¿… ¿Õ»Ã¿÷»ﬁ ƒÀﬂ œŒÀ”◊≈Õ»ﬂ ”–ŒÕ¿");
    }

    private void EndFight(bool isWin)
    {
        if (isWin)
            WinFight();
        else
            WarrionDead();
    }

    private void WarrionDead()
    {
        _animator.SetTrigger("Dead");
    }

    private void WarrionAttack()
    {
        _animator.SetTrigger("Attack_1");
    }

    private void OnDisable()
    {
        _warrionStats.WarrionHealtPointChange -= WarrionGetDamage;
        _warrionFight.WarrionAttack -= WarrionAttack;
        _warrionFight.WarrionRun -= WarrionMove;
        _warrionFight.FightEnd -= EndFight;
    }

}

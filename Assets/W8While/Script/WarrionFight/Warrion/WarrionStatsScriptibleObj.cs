using UnityEngine;

[CreateAssetMenu(fileName = "NewWarrionStats", menuName = "Warrion/Stats")]
public class WarrionStatsScriptibleObj : ScriptableObject
{
    [SerializeField] private float _minHealtPoint;
    public float MinHealtPoint => _minHealtPoint;
    [SerializeField] private float _maxHealtPoint;
    public float MaxHealtPoint => _maxHealtPoint;
    [SerializeField] private float _minDamage;
    public float MinDamage => _minDamage;
    [SerializeField] private float _maxDamage;
    public float MaxDamage => _maxDamage;
    [SerializeField] private float _minArmor;
    public float MinArmor => _minArmor;
    [SerializeField] private float _maxArmor;
    public float MaxArmor => _maxArmor;
    [SerializeField] private int _minCritChance;
    public int MinCritChance => _minCritChance;
    [SerializeField] private int _maxCritChance;
    public int MaxCritChance => _maxCritChance;
    [SerializeField] private float _minCritDamage;
    public float MinCritDamage => _minCritDamage;
    [SerializeField] private float _maxCritDamage;
    public float MaxCritDamage => _maxCritDamage;
    [SerializeField] private int _minEvasionChance;
    public int MinEvasionChance => _minEvasionChance;
    [SerializeField] private int _maxEvasionChance;
    public int MaxEvasionChance => _maxEvasionChance;
    [SerializeField] private string _charactersName;
    public string CharactersName => _charactersName;
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] private float _attackRange;
    public float AttackRange => _attackRange;
    [SerializeField] private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;
    [SerializeField] private Sprite _charactersSprite;
    public Sprite CharactersSprite => _charactersSprite;
    [SerializeField] private string _description;
    public string Description => _description;

    public string GetStats()
    {
        string allStats = $"HP: {_minHealtPoint} - {_maxHealtPoint} \nDamage: {_minDamage} - {_maxDamage}\nAttackSpeed: {_attackSpeed} \n";
        allStats += _description;
        return allStats;
    }
}

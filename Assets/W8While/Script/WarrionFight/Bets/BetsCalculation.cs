using System;

public class BetsCalculation
{

    static public float[] FightCoefficientCalculation(WarrionStatsScriptibleObj firstWarrion, WarrionStatsScriptibleObj secondWarrion)
    {
        float[] firstWarrionStats = StatsCalculation(firstWarrion);
        float[] secondWarrionStats = StatsCalculation(secondWarrion);

        int firstWarrionHitsToKill = (int)MathF.Ceiling(secondWarrionStats[1] / firstWarrionStats[0]);
        int secondWarrionHitsToKill = (int)MathF.Ceiling(firstWarrionStats[1] / secondWarrionStats[0]);

        float firstWarrionCoefficient = MathF.Round(((float)firstWarrionHitsToKill / secondWarrionHitsToKill + 1), 2);
        float secondWarrionCoefficient = MathF.Round(((float)secondWarrionHitsToKill / firstWarrionHitsToKill + 1), 2);
        return new float[] { firstWarrionCoefficient, secondWarrionCoefficient };
    }

    static private float[] StatsCalculation(WarrionStatsScriptibleObj warrion)
    {
        float warrionAvarageDamage = (warrion.MinDamage + warrion.MaxDamage) / 2;
        float warrionAvarageHealthPoint = (warrion.MinHealtPoint + warrion.MaxHealtPoint) / 2;
        float warrionDps = warrionAvarageDamage / warrion.AttackSpeed;
        return new float[] { warrionDps, warrionAvarageHealthPoint };
    }
}

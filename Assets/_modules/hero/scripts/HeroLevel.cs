using UnityEngine;

public class HeroLevel : MonoBehaviour
{
    public static int GetLevelFromXP(int experience)
    {
        const int XP_PER_LEVEL = 100;

        if (experience < 0)
        {
            return 1;
        }

        return (int)System.Math.Floor(System.Math.Sqrt(experience / (double)XP_PER_LEVEL)) + 1;
    }

    public static int GetXPForLevel(int level)
    {
        const int XP_PER_LEVEL = 100;

        if (level <= 1)
        {
            return 0;
        }

        return XP_PER_LEVEL * (level - 1) * (level - 1);
    }

    public static float GetLevelProgress(int experience)
    {
        int level = GetLevelFromXP(experience);
        int currentLevelXP = GetXPForLevel(level);
        int nextLevelXP = GetXPForLevel(level + 1);

        return (float)(experience - currentLevelXP) / (nextLevelXP - currentLevelXP);
    }
}
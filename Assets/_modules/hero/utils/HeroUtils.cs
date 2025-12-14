using UnityEngine;

public class HeroUtils : MonoBehaviour
{
    public static HeroUtils Instance { get; private set; }

    private const int MAX_LEVEL = 30;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static int GetHeroLevelByExperience(long totalExperience)
    {
        if (totalExperience < 0) totalExperience = 0;

        int level = 1;
        long xpRemaining = totalExperience;

        while (level < MAX_LEVEL)
        {
            long xpToNext = XpToNext(level);
            if (xpRemaining < xpToNext)
                break;

            xpRemaining -= xpToNext;
            level++;
        }

        return level;
    }

    private static long XpToNext(int level)
    {
        const float A = 60f;
        const float P = 1.8f;
        const float B = 40f;

        return (long)Mathf.Floor(A * Mathf.Pow(level, P) + B);
    }
}

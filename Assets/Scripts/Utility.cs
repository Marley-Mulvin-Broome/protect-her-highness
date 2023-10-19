using UnityEngine;

public static class Utility
{
    public static bool CoinFlip()
    {
        float random = Random.Range(0, 1);
        
        return random >= 0.5;
    }
}
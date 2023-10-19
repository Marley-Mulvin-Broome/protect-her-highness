using UnityEngine;

public static class Utility
{
    public static bool CoinFlip()
    {
        float random = Random.Range(0, 101);
        
        return random >= 50;
    }

    public static bool OnCamera(Camera camera, Vector3 position)
    {
        if (position.x < -camera.orthographicSize || 
            position.x > camera.orthographicSize ||
            position.y < -camera.orthographicSize ||
            position.y > camera.orthographicSize)
        {
            return false;
        }
        
        return true;
    }
}
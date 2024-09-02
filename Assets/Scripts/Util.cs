using UnityEngine;
public class Util
{
    public const float CANVAS_LIMIT_X = 10 / 2;
    public const float CANVAS_LIMIT_Y = 9 / 2;

    public static bool OutOfBound(Vector3 position)
    {
        return (position.x < -CANVAS_LIMIT_X || position.x > CANVAS_LIMIT_X
            || position.y < -CANVAS_LIMIT_Y || position.y > CANVAS_LIMIT_Y);
    }
}

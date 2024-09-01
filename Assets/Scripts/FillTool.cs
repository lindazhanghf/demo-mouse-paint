using UnityEngine;
using NaughtyAttributes;

public class FillTool : ITool
{
    private const float CANVAS_LIMIT_X = 10 / 2;
    private const float CANVAS_LIMIT_Y = 9 / 2;
    private Vector3 m_previousPos;

    public void Draw(Vector3 currentPos)
    {
        m_previousPos = currentPos;
    }
    public void EndDraw()
    {
        if (m_previousPos.x < -CANVAS_LIMIT_X || m_previousPos.x > CANVAS_LIMIT_X
            || m_previousPos.y < -CANVAS_LIMIT_Y || m_previousPos.y > CANVAS_LIMIT_Y) return;

        DrawLine found = FindClosestLine(m_previousPos);
        if (found != null) found.GetComponent<LineRenderer>().endColor = Color.red;
    }

    private DrawLine FindClosestLine(Vector3 startPos)
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(startPos, 0.1f);
        if (collided.Length > 0) return GetLine();

        for (float radius = 0; radius < 10; radius += 0.2f)
        {
            collided = Physics2D.OverlapCircleAll(startPos + Vector3.up * radius, 0.1f);
            if (collided.Length > 0) return GetLine();

            collided = Physics2D.OverlapCircleAll(startPos + Vector3.down * radius, 0.1f);
            if (collided.Length > 0) return GetLine();

            collided = Physics2D.OverlapCircleAll(startPos + Vector3.left * radius, 0.1f);
            if (collided.Length > 0) return GetLine();

            collided = Physics2D.OverlapCircleAll(startPos + Vector3.right * radius, 0.1f);
            if (collided.Length > 0) return GetLine();
        }
        return null;

        DrawLine GetLine()
        {
            return collided[0].transform.GetComponentInParent<DrawLine>();
        }
    }
}

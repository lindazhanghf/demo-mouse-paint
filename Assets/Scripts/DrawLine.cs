using UnityEngine;
using NaughtyAttributes;

public class DrawLine : MonoBehaviour
{
    static int s_TotalLines = 0;
    public float LineWidth = 0.1f;

    private LineRenderer m_lineRenderer;
    private Transform m_lineColliders;
    private Vector3 m_previousColliderPos;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 0;

        m_lineColliders = new GameObject("Colliders").transform;
        m_lineColliders.SetParent(transform);
    }

    public void Draw(Vector3 currentPos)
    {
        if (m_lineRenderer.positionCount < 1)
        {
            InitLineRenderer();
            m_lineRenderer.SetPosition(0, currentPos);
            return;
        }
        m_lineRenderer.positionCount++;
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, currentPos);


        return;
        if (m_previousColliderPos == Vector3.zero) m_previousColliderPos = currentPos;
        else if (Vector3.Distance(currentPos, m_previousColliderPos) > LineWidth * 2)
        {
            CircleCollider2D circleCollider = m_lineColliders.gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = LineWidth;
            circleCollider.offset = currentPos;
            m_previousColliderPos = currentPos;
        }
    }

    public void EndDraw()
    {
        Vector3[] points = new Vector3[m_lineRenderer.positionCount];
        m_lineRenderer.GetPositions(points);
        EdgeCollider2D edgeCollider = m_lineColliders.gameObject.AddComponent<EdgeCollider2D>();
        edgeCollider.points = ToVector2Array(points);
        edgeCollider.isTrigger = true;
        edgeCollider.edgeRadius = LineWidth/2 < 0.1f ? 0.1f : LineWidth/2;
    }

    #region Private Methods
    private void InitLineRenderer()
    {
        m_lineRenderer.positionCount = 1;

        m_lineRenderer.startWidth = LineWidth;
        m_lineRenderer.endWidth = LineWidth;

        s_TotalLines++;
        name = "Line " + s_TotalLines;
    }

    private Vector2[] ToVector2Array(Vector3[] vector3Array)
    {
        Vector2[] vector2Array = new Vector2[vector3Array.Length];
        for (int i = 0; i < vector3Array.Length; i++)
        {
            vector2Array[i] = new Vector2(vector3Array[i].x, vector3Array[i].y);
        }
        return vector2Array;
    }
    #endregion
}

using UnityEngine;
using NaughtyAttributes;

public class DrawLine : MonoBehaviour
{
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
            m_lineRenderer.positionCount = 1;
            m_lineRenderer.SetPosition(0, currentPos);
            return;
        }
        m_lineRenderer.positionCount++;
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, currentPos);


        if (m_previousColliderPos == Vector3.zero) m_previousColliderPos = currentPos;
        else if (Vector3.Distance(currentPos, m_previousColliderPos) > LineWidth * 2)
        {
            CircleCollider2D circleCollider = m_lineColliders.gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = LineWidth;
            circleCollider.offset = currentPos;
            m_previousColliderPos = currentPos;
        }
    }
}

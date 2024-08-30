using UnityEngine;
using NaughtyAttributes;

public class DrawLine : MonoBehaviour
{
    private LineRenderer m_lineRenderer;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 0;
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
    }
}

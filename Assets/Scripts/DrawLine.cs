using UnityEngine;
using NaughtyAttributes;

public class DrawLine : MonoBehaviour
{
    public float MinDistance = 0.1f;
    private LineRenderer m_lineRenderer;
    private Vector3 m_previousPos;

    [ShowNonSerializedField]
    private bool Debug_IsMouseButtonDown;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_previousPos = transform.position;
    }

    private void Update()
    {
        Debug_IsMouseButtonDown = Input.GetMouseButton(0);
        if (!Input.GetMouseButton(0)) return;

        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = 0;

        if (m_previousPos == Vector3.zero) m_lineRenderer.SetPosition(0, currentPos);

        if (Vector3.Distance(currentPos, m_previousPos) > MinDistance)
        {
            m_lineRenderer.positionCount++;
            m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, currentPos);
            m_previousPos = currentPos;
        }
    }
}

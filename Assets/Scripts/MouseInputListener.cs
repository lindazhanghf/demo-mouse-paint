using UnityEngine;
using NaughtyAttributes;

public class MouseInputListener : MonoBehaviour
{
	#region Editor
	[SerializeField] [Required] private DrawLine m_linePrefab;
    public float MinDistance = 0.1f;
	#endregion

	private Transform m_linesParent;
    [ShowNonSerializedField]
    private DrawLine m_newLine;
    private Vector3 m_previousPos;

    [ShowNonSerializedField]
    private bool m_IsMouseButtonDown;

    private void Awake()
    {
		var emptyParent = new GameObject("Lines");
		m_linesParent = emptyParent.transform;

        m_newLine = Instantiate(m_linePrefab, m_linesParent, false);
    }

    private void Update()
    {
        m_IsMouseButtonDown = Input.GetMouseButton(0);
        if (!Input.GetMouseButton(0)) return;

        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = 0;


        if (Vector3.Distance(currentPos, m_previousPos) > MinDistance)
        {
            m_newLine.Draw(currentPos);
            m_previousPos = currentPos;
        }
    }
}

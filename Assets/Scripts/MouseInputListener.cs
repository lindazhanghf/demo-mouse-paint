using UnityEngine;
using NaughtyAttributes;

public class MouseInputListener : MonoBehaviour
{
    #region Editor
    [SerializeField] [Required] private DrawLine m_linePrefab;
    public float MinDistance = 0.1f;
    #endregion
    private Transform m_linesParent;

    #region Tools
    private ITool m_currentTool;
    private EraseLine m_eraseTool = new EraseLine();

    [ShowNonSerializedField]
    private DrawLine m_newLine;

    [ShowNonSerializedField]
    private bool m_eraseMode = false;
    public void ToggleEraseMode(bool value)
    {
        m_eraseMode = value;
        if (m_eraseMode) m_currentTool = m_eraseTool;
        else m_currentTool = m_newLine;
    }
    #endregion
    private Vector3 m_previousPos;

    private bool MouseButtonDown
    {
        get { return m_IsMouseButtonDown; }
        set
        {
            if (m_IsMouseButtonDown == true && value == false) // Button up
            {
                m_newLine.EndDraw();
                m_newLine = Instantiate(m_linePrefab, m_linesParent, false);
                ToggleEraseMode(m_eraseMode); // Update current tool
            }
            m_IsMouseButtonDown = value;
        }
    }

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
        MouseButtonDown = Input.GetMouseButton(0);
        if (m_IsMouseButtonDown == false) return;

        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = 0;


        if (Vector3.Distance(currentPos, m_previousPos) > MinDistance)
        {
            m_currentTool.Draw(currentPos);
            m_previousPos = currentPos;
        }
    }
}

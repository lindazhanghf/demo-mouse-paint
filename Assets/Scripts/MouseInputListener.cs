using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

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
    private FillTool m_fillTool = new FillTool();

    [SerializeField] private Button m_eraseButton;
    [SerializeField] private Button m_drawButton;
    [SerializeField] private Button m_undoButton;
    [SerializeField] private Button m_fillButton;
    [ShowNonSerializedField]
    private DrawLine m_newLine;

    [ShowNonSerializedField]
    private int m_currentToolMode = 1;
    public void SetTool(int value)
    {
        m_currentToolMode = value;
        if (value == 0) m_currentTool = m_eraseTool;
        if (value == 1) m_currentTool = m_newLine;
        if (value == 2) m_currentTool = m_fillTool;
    }
    #endregion
    private Vector3 m_previousPos;

    private bool MouseButtonDown
    {
        get { return m_IsMouseButtonDown; }
        set
        {
            if (Util.OutOfBound(Camera.main.ScreenToWorldPoint(Input.mousePosition))) return;

            if (m_IsMouseButtonDown && !value) // Button up
            {
                m_currentTool.EndDraw();
                if (m_currentToolMode == 1)
                {
                    m_newLine = Instantiate(m_linePrefab, m_linesParent, false);
                    m_currentTool = m_newLine; // Update current tool
                }
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

        m_currentTool = m_newLine;
        m_eraseButton.onClick.AddListener(delegate{ SetTool(0); });
        m_drawButton.onClick.AddListener(delegate{ SetTool(1); });
        m_undoButton.onClick.AddListener(delegate{ m_eraseTool.UndoLast(); });
        m_fillButton.onClick.AddListener(delegate{ SetTool(2); });
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

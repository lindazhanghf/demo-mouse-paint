using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class EraseLine : ITool
{
    #region Undo
    private const int MAX_UNDO_COUNT = 10;
    private List<GameObject> m_erasedLines = new List<GameObject>();
    #endregion

    public void Draw(Vector3 currentPos)
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(currentPos, 0.1f);
        foreach (Collider2D collider in collided)
        {
            Debug.Log("Erasing: " + collider.transform.parent.gameObject.name);
            GameObject line = collider.transform.parent.gameObject;
            line.SetActive(false);
            AddErasedLine(line);
        }
    }
    public void EndDraw()
    {
        
    }

    public void UndoLast()
    {
        if (m_erasedLines.Count > 0)
        {
            GameObject obj = m_erasedLines[m_erasedLines.Count - 1];
            obj.SetActive(true);
            m_erasedLines.RemoveAt(m_erasedLines.Count - 1);
        }
    }

    private void AddErasedLine(GameObject obj)
    {
        if (m_erasedLines.Count >= MAX_UNDO_COUNT)
        {
            GameObject.Destroy(m_erasedLines[0]);
            m_erasedLines.RemoveAt(0);
        }
        m_erasedLines.Add(obj);
    }
}

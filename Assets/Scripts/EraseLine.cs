using UnityEngine;
using NaughtyAttributes;

public class EraseLine : ITool
{
    public void Draw(Vector3 currentPos)
    {
        Collider2D[] collided = Physics2D.OverlapCircleAll(currentPos, 0.1f);
        foreach (Collider2D collider in collided)
        {
            Debug.Log("Erasing: " + collider.transform.parent.gameObject.name);
            GameObject.Destroy(collider.transform.parent.gameObject);
        }
    }
    public void EndDraw()
    {
        
    }
}
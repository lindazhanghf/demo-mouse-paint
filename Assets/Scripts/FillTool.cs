using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class FillTool : ITool
{
    private Vector3 m_previousPos;

    public void Draw(Vector3 currentPos)
    {
        m_previousPos = currentPos;
    }
    public void EndDraw()
    {
        if (Util.OutOfBound(m_previousPos)) return;

        DrawLine found = FindClosestLine(m_previousPos);
        if (found == null) return;
        Vector3[] points = new Vector3[found.Line.positionCount];
        found.Line.GetPositions(points);
        CreateMesh(points);
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
    private void CreateMesh(Vector3[] vertices)
    {
        FillID = s_TotalFills++;
        GameObject meshObject = new GameObject("FilledMesh " + FillID);
        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;

        List<int> triangles = new List<int>();
        for (int i = 1; i < vertices.Length - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.red;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMesh2 : MonoBehaviour
{
    Color beginColor = Color.yellow;
    Color endColor = Color.red;
    float highlightsize = 0.01f;


    // Update is called once per frame
    void Update()
    {
        CreateEdgeLineOnModel();   
    }

    void CreateEdgeLineOnModel()
    {
        EdgeVertices2 allVertices = FindAllVertices();

        //Get the points from the Vertices
        Vector3[] verticesToDraw = allVertices.vertices.ToArray();
        Vector3 move = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3[] vertex2 = allVertices.vertices.ToArray();
        for (int i = 0; i < verticesToDraw.Length; i++)
        {
            vertex2[i] = Quaternion.AngleAxis(-45, Vector3.right) * verticesToDraw[i];

        }

        DrawLine(verticesToDraw);
        DrawLine(vertex2);
    }

    void DrawLine(Vector3[] verticesToDraw)
    {
        //Create a line renderer obj then make it this gameobj a parent of it
        GameObject childRendererObj = new GameObject("LineObj");
        childRendererObj.transform.SetParent(transform);

        //Create new line renderer if it does not exist
        LineRenderer line = childRendererObj.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = childRendererObj.AddComponent<LineRenderer>();
        }

        //assign material to the new line renderer
        line.material = new Material(Shader.Find("Unlit/connection"));

        //set color and width
        line.startColor = beginColor;
        line.endColor = endColor;
        line.startWidth = highlightsize;
        line.endWidth = highlightsize;

        //convert local to world points
        for (int i = 0; i < verticesToDraw.Length; i++)
        {
            verticesToDraw[i] = gameObject.transform.TransformPoint(verticesToDraw[i]);
        }

        //set the setvertexcount of the linerenderer to the length of the points
        line.positionCount = verticesToDraw.Length + 1;
        for (int i = 0; i < verticesToDraw.Length; i++)
        {
            //draw the line
            Vector3 finalLine = verticesToDraw[i];
            line.SetPosition(i, finalLine);

            //Check if this is the last loop. Now Close the Line drawn
            if (i == (verticesToDraw.Length - 1))
            {
                finalLine = verticesToDraw[0];
                line.SetPosition(verticesToDraw.Length, finalLine);
                Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
                line.transform.localScale += scaleChange;
            }
        }
        Destroy(line.gameObject, 0.1f);
    }

    EdgeVertices2 FindAllVertices()
    {
        //Get Meshfilter from cube
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] meshVerts = mesh.vertices;

        //get temp vert array
        float[][] meshVertsArray;
        meshVertsArray = new float[meshVerts.Length][];

        //where to store the vertex points
        EdgeVertices2 allVertices = new EdgeVertices2();
        allVertices.vertices = new List<Vector3>();

        int indexCounter = 1;

        //get x,y,z vertex point
        while (indexCounter < meshVerts.Length)
        {
            meshVertsArray[indexCounter] = new float[3];

            meshVertsArray[indexCounter][0] = meshVerts[indexCounter].x;
            meshVertsArray[indexCounter][1] = meshVerts[indexCounter].y;
            meshVertsArray[indexCounter][2] = meshVerts[indexCounter].z;

            Vector3 tempVect = new Vector3(meshVertsArray[indexCounter][0], meshVertsArray[indexCounter][1], meshVertsArray[indexCounter][2]);

            //store the vertec point
            allVertices.vertices.Add(tempVect);

            indexCounter++;
        }
        return allVertices;
    }
}

public struct EdgeVertices2
{
    public List<Vector3> vertices;
}

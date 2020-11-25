using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EarthMesh : MonoBehaviour
{
    Color beginColor = Color.yellow;
    Color endColor = Color.red;
    float hightlightSize = 0.01f;
    double timer;
    //public LineRenderer line2;

    private void Start()
    {
        timer = Time.time + 1;
    }

    void Update()
    {
        if (timer < Time.time)
        {
            createEdgeLineOnModel();
        }
    }

    


    void createEdgeLineOnModel()
    {
        EdgeVertices allVertices = findAllVertices();

        //Get the points from the Vertices
        Vector3[] verticesToDraw = allVertices.vertices.ToArray();
        Vector3 move = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3[] vertex2 = allVertices.vertices.ToArray();
        for (int i = 0; i < verticesToDraw.Length; i++)
        {
           vertex2[i] = Quaternion.AngleAxis(-45, Vector3.right) * verticesToDraw[i];
            //vertex2[i - 1].Equals(null);
        }

            drawLine(verticesToDraw);
        drawLine(vertex2);
        
        //drawLine(vertex2);
        
    }

    //Draws lines through the provided vertices
    void drawLine(Vector3[] verticesToDraw)
    {
        //Create a Line Renderer Obj then make it this GameObject a parent of it
        GameObject childLineRendererObj = new GameObject("LineObj");
        childLineRendererObj.transform.SetParent(transform);
        //line2.transform.SetParent(transform);

        //Create new Line Renderer if it does not exist
        LineRenderer line = childLineRendererObj.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = childLineRendererObj.AddComponent<LineRenderer>();

        }

        //Assign Material to the new Line Renderer
        //Hidden/Internal-Colored
        //Particles/Additive
        line.material = new Material(Shader.Find("Unlit/connection"));


        //Set color and width
        line.startColor = beginColor;
        line.endColor = endColor;
        //line.SetWidth(hightlightSize, hightlightSize);
        line.startWidth = hightlightSize;
        line.endWidth = hightlightSize;

        //Convert local to world points
        for (int i = 0; i < verticesToDraw.Length; i++)
        {
            verticesToDraw[i] = gameObject.transform.TransformPoint(verticesToDraw[i]);
        }

        //5. Set the SetVertexCount of the LineRenderer to the Length of the points
        line.positionCount = verticesToDraw.Length + 1;
        for (int i = 0; i < verticesToDraw.Length; i++)
        {
            //Draw the  line
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
        Destroy(line.gameObject, 0.08f);
    }

    EdgeVertices findAllVertices()
    {
        //Get MeshFilter from Cube
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] meshVerts = mesh.vertices;

        //Get temp vert array
        float[][] meshVertsArray;
        meshVertsArray = new float[meshVerts.Length][];

        //Where to store the vertex points
       
        EdgeVertices allVertices = new EdgeVertices();
        allVertices.vertices = new List<Vector3>();

        int indexCounter = 1;

        //Get x,y,z vertex point
        while (indexCounter < meshVerts.Length)
        {
            meshVertsArray[indexCounter] = new float[3];

            meshVertsArray[indexCounter][0] = meshVerts[indexCounter].x;
            meshVertsArray[indexCounter][1] = meshVerts[indexCounter].y;
            meshVertsArray[indexCounter][2] = meshVerts[indexCounter].z;

            Vector3 tempVect = new Vector3(meshVertsArray[indexCounter][0], meshVertsArray[indexCounter][1], meshVertsArray[indexCounter][2]);

            //Store the vertex pont
            allVertices.vertices.Add(tempVect);

            indexCounter++;
        }
        return allVertices;
    }
}

public struct EdgeVertices
{
    public List<Vector3> vertices;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lines2 : MonoBehaviour
{
   public Transform point1;
   public Transform point2;
   public Transform point3;
    public LineRenderer line;
    public float vertexCount = 12;
    public float point2Ypos = 2;
    public GameObject earth;
    //Vector3 earthV;
    Mesh mesh;
    Vector3[] vertices;
    //float time;
    //float timer = 0.5f;
    float maxStartingVelocity = 0.02f;
    float maxAcceleration = 0.05f;
    float linegravity = 0.03f;
    float lineDrag = -0.01f;

    

    // Start is called before the first frame update

    void Start()
    {

        mesh = earth.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    public void Update()
    {
        int amount = 10;
        Vector3[] positions = new Vector3[amount];
        mesh = earth.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 lastPostion = vertices[i];
            Vector3 velocity = vertices[i]* maxStartingVelocity;
            for (int y = 0; y < amount; y++)
            {
                positions[y] = lastPostion;
                point1.transform.position = positions[y];
                point3.transform.position = positions[y];
                lastPostion += velocity;
                velocity += Random.insideUnitSphere* maxAcceleration;
                velocity -= lastPostion * linegravity;
                velocity *= 1 - lineDrag;
            }
            
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        point2.transform.position = new Vector3((point1.position.x + point3.transform.position.x), point2Ypos, (point1.transform.position.z + point3.transform.position.z) / 2);

        var pointList = new List<Vector3>();
        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(point1.position, point2.position, i);
            var tangent2 = Vector3.Lerp(point2.position, point3.position, i);
            var curve = Vector3.Lerp(tangent1, tangent2, i);

            pointList.Add(curve);
        }
        line.positionCount = amount;
        line.SetPositions(positions);
        line.transform.RotateAround(earth.transform.position, Vector3.up, 12.3f * Time.deltaTime);
        


        /*time += Time.deltaTime;
        for (int i = 0; i < vertices.Length; i++)
        {

            //vertices[i] += Vector3.up * Time.deltaTime;
            if (time > timer)
            {
                Debug.Log("timer " + timer);
                point1.transform.position = new Vector3(Random.Range(0, vertices[i].x), Random.Range(0, vertices[i].y), Random.Range(0, vertices[i].z));
                point3.transform.position = new Vector3(Random.Range(0, vertices[i].x), Random.Range(0, vertices[i].y), Random.Range(0, vertices[i].z));
                timer += Time.deltaTime + 0.5f;

            }
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        point2.transform.position = new Vector3((point1.position.x + point3.transform.position.x), point2Ypos, (point1.transform.position.z + point3.transform.position.z) / 2);

        var pointList = new List<Vector3>();
        var pointList2 = new List<Vector3>();

        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(point1.position, point2.position, i);
            var tangent2 = Vector3.Lerp(point2.position, point3.position, i);
            var curve = Vector3.Lerp(tangent1, tangent2, i);

            pointList.Add(curve);
        }

        line.positionCount = pointList.Count;
        line.SetPositions(pointList.ToArray());
        line.transform.RotateAround(earth.transform.position, Vector3.up, 12.3f * Time.deltaTime);
        //Debug.Log("random" + Random.Range(0, 4));*/

    }
    


}

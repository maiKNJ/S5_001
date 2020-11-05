using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lines : MonoBehaviour
{
    public List <Transform> point1;
    public List <Transform> point2;
    public List <Transform> point3;
    public List <LineRenderer> line;
    public float vertexCount = 12;
    public float point2Ypos = 2;
    public GameObject earth;
    Vector3 earthV;
    Mesh mesh;
    Vector3[] vertices;
    float time;
    float timer = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    private void Update()
    {
        for (int x = 0; x < line.Count; x++)
        {
            time += Time.deltaTime;
            for (int i = 0; i < vertices.Length; i++)
            {

                //vertices[i] += Vector3.up * Time.deltaTime;
                if (time > timer)
                {
                    Debug.Log("timer " + timer);
                    point1[x].transform.position = new Vector3(Random.Range(0, vertices[i].x), Random.Range(0, vertices[i].y), Random.Range(0, vertices[i].z));
                    point3[x].transform.position = new Vector3(Random.Range(0, vertices[i].x), Random.Range(0, vertices[i].y), Random.Range(0, vertices[i].z));
                    timer += Time.deltaTime + 0.5f;

                }
            }
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            point2[x].transform.position = new Vector3((point1[x].position.x + point3[x].transform.position.x), point2Ypos, (point1[x].transform.position.z + point3[x].transform.position.z) / 2);
            var pointList = new List<Vector3>();

            for (float i = 0; i <= 1; i += 1 / vertexCount)
            {
                var tangent1 = Vector3.Lerp(point1[x].position, point2[x].position, i);
                var tangent2 = Vector3.Lerp(point2[x].position, point3[x].position, i);
                var curve = Vector3.Lerp(tangent1, tangent2, i);

                pointList.Add(curve);
            }

            line[x].positionCount = pointList.Count;
            line[x].SetPositions(pointList.ToArray());
            
        }
    }




}

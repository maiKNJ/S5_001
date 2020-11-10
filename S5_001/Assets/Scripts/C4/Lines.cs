using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lines : MonoBehaviour
{
    public List <Transform> point1;
    public List <Transform> point2;
    public List <Transform> point3;
    public List<Transform> point4;
    public List <LineRenderer> line;
    public LineRenderer line5;
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
        
        mesh = earth.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    private void Update()
    {
        mesh = earth.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
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
            point4[x].transform.position = new Vector3((point1[Random.Range(0, 4)].position.x + point3[x].transform.position.x), point2Ypos, (point1[Random.Range(0,4)].transform.position.z + point3[x].transform.position.z) / 2);
            var pointList = new List<Vector3>();
            var pointList2 = new List<Vector3>();

            for (float i = 0; i <= 1; i += 1 / vertexCount)
            {
                var tangent1 = Vector3.Lerp(point1[x].position, point2[x].position, i);
                var tangent2 = Vector3.Lerp(point2[x].position, point3[x].position, i);
                var tangent3 = Vector3.Lerp(point3[x].position, point4[x].position, i);
                var tangent4 = Vector3.Lerp(point4[x].position, point1[Random.Range(0,4)].position, i);
                var curve = Vector3.Lerp(tangent1, tangent2, i);
                var curve2 = Vector3.Lerp(tangent3, tangent4, i);
                
                pointList.Add(curve);
                pointList2.Add(curve2);
            }

            line[Random.Range(0,4)].positionCount = pointList.Count;
            line[Random.Range(0, 4)].SetPositions(pointList.ToArray());
            line[x].transform.RotateAround(earth.transform.position, Vector3.up, 12.3f * Time.deltaTime);
            line5.positionCount = pointList2.Count;
            line5.SetPositions(pointList2.ToArray());
            line5.transform.RotateAround(earth.transform.position,Vector3.up, 2.3f*Time.deltaTime);
            //Debug.Log("random" + Random.Range(0, 4));
            
        }
    }




}

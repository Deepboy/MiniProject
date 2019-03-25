using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDraw : MonoBehaviour
{
    public float water = 50;
    public float fire = 50;
    public float wind = 50;
    public float thunder = 50;
    public float earth = 50;
    public float dark = 50;

    public float maxWater = 600;
    public float maxFire = 600;
    public float maxWind = 600;
    public float maxThunder = 600;
    public float maxEarth = 600;
    public float maxDark = 600;

    public float size = 265;

    private float sin30 = 0.5f;
    private float cos30 = 0.866f;

    public void DrawTriangle()
    {
        gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material/RadarMaterial");

        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        /*
        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 500, 0), new Vector3(500, 500, 0) };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[] { 0, 1, 2 };
        */

        Vector3 waterVector = new Vector3(0, water / maxWater * size, 0);
        Vector3 fireVector = new Vector3(-((fire * cos30) / maxFire) * size, ((fire * sin30) / maxWater) * size, 0);
        Vector3 windVector = new Vector3(((wind * cos30) / maxWind) * size, ((wind * sin30) / maxWind) * size, 0);
        Vector3 darkVector = new Vector3(0, -dark / maxDark * size, 0);
        Vector3 thunderVector = new Vector3(((thunder * cos30) / maxThunder) * size, -((thunder * sin30) / maxThunder) * size, 0);
        Vector3 earthVector = new Vector3(-((earth * cos30) / maxEarth) * size, -((earth * sin30) / maxEarth) * size, 0);

        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), waterVector, windVector, thunderVector, darkVector, earthVector, fireVector };
        mesh.triangles = new int[]
        {
            0,1,2,
            0,2,3,
            0,3,4,
            0,4,5,
            0,5,6,
            0,6,1
        };
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        DrawTriangle();
    }
}

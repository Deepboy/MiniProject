using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{

    public class Position
    {
        public float radius = 0f, angle = 0f;
        public Position(float r, float a)
        {
            // 半径
            radius = r;
            // 半径
            angle = a;                          
        }
    }

    // 粒子系统  
    private ParticleSystem parSys;              
    // 粒子数组  
    private ParticleSystem.Particle[] parArr;   
    // 粒子位置数组  
    private Position[] parPos;                  

    // 粒子数量  
    public int num = 10000;
    // 粒子大小  
    public float size = 0.03f;
    // 内半径及外半径  
    public float r = 1f, R = 2f;   

    void SetRandom()
    {
        for (int i = 0; i < num; i++)
        {
            // 生成随机半径，主要集中在环中间
            float min = Random.Range(1f, (r + R) / (2 * r)) * r;
            float max = Random.Range((r + R) / (2 * R), 1.2f) * R;
            float radius = Random.Range(min, max);
            // 生成随机角度
            float angle = Random.Range(0f, 360f);
            float radian = angle * 180 / Mathf.PI;
            // 设定粒子的随机位置
            parPos[i] = new Position(radius, angle);
            parArr[i].position = new Vector3(parPos[i].radius * Mathf.Cos(radian), parPos[i].radius * Mathf.Sin(radian), 0f);
        }
        // 将所有生成的随机粒子放入粒子系统里
        parSys.SetParticles(parArr, parArr.Length);
    }

    // Use this for initialization
    void Start()
    {
        parArr = new ParticleSystem.Particle[num];
        parPos = new Position[num];

        parSys = this.GetComponent<ParticleSystem>();
        var main = parSys.main;
        main.startSpeed = 0;
        main.startSize = size;
        main.loop = false;
        main.maxParticles = num;
        parSys.Emit(num);
        parSys.GetParticles(parArr);

        SetRandom();
    }

    // Update is called once per frame
    void Update()
    {
        // 更新每个粒子位置和角度
        for (int i = 0; i < num; i++)
        {
            // 顺时针旋转  
            parPos[i].angle = (parPos[i].angle - Random.Range(0.5f, 3f)) % 360f;
            float radian = parPos[i].angle / 180 * Mathf.PI;
            parArr[i].position = new Vector3(parPos[i].radius * Mathf.Cos(radian), parPos[i].radius * Mathf.Sin(radian), 0f);
        }
        parSys.SetParticles(parArr, parArr.Length);
    }
}

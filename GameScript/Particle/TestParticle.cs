using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    //出牌数
    private static int cardCount;

    // 粒子系统  
    private ParticleSystem particleSys;
    // 粒子数组  
    private ParticleSystem.Particle[] particleArray;
    // 粒子位置数组  
    private Position[] particlePositonArray;
    // 粒子大小  
    private float particleSize = 3f;
    // 内半径及外半径  
    private float r = 1f, R = 2f;
    //粒子位置
    private class Position
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

    /// <summary>
    /// 初始化出牌数
    /// </summary>
    /// <param name="count"></param>
    public static void InitDrawCardCount(int count)
    {
        if(count < 0 || count > 5)
        {
            cardCount = 0;
            return;
        }
        cardCount = count;
    }

    // Use this for initialization
    void Start()
    {
        particleSys = gameObject.GetComponent<ParticleSystem>();
        particleArray = new ParticleSystem.Particle[5];
        particlePositonArray = new Position[5];
        var main = particleSys.main;
        main.startSpeed = 0;
        main.startSize = particleSize;
        main.loop = false;
        main.maxParticles = 5;
        particleSys.Emit(5);
        particleSys.GetParticles(particleArray);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

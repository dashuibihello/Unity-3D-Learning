using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloTest : MonoBehaviour
{
    public class Circle
    {
        public float radius;
        public float angle;
        public Circle(float myradius, float myangle)
        {
            radius = myradius;
            angle = myangle;
        }
    }
    private ParticleSystem myParticleSystem;
    private ParticleSystem.Particle[] myParticles;
    private Circle[] mycircle;

    private int Parcount = 40000;                   //粒子数量
    private float Parsize = 0.1f;                   //粒子大小
    private float bigCircleRadius = 20f;            //最大半径
    private float smallCircleRadius = 10f;          //最小半径

    void Start()
    {
        myParticles = new ParticleSystem.Particle[Parcount];
        mycircle = new Circle[Parcount];

        myParticleSystem = this.GetComponent<ParticleSystem>();
        var mymain = myParticleSystem.main;
        mymain.startSpeed = 0;
        mymain.startSize = Parsize;
        mymain.loop = false;
        mymain.maxParticles = Parcount;
        myParticleSystem.Emit(Parcount);
        myParticleSystem.GetParticles(myParticles);
        myRandom();
    }

    void myRandom()
    {
        for (int i = 0; i < Parcount; ++i)
        {   
            float midRadius = (bigCircleRadius + smallCircleRadius) / 2;
            float min = Random.Range(1.0f, midRadius / smallCircleRadius) * smallCircleRadius;
            float max= Random.Range(midRadius / bigCircleRadius, 1.0f) * bigCircleRadius;
            float radius = Random.Range(min, max);

            
            float angle = Random.Range(0.0f, 360.0f);
            float theangle = angle / 180 * Mathf.PI;

            float height = Random.Range(0.0f, 20.0f);

            mycircle[i] = new Circle(radius, angle);

            myParticles[i].position = new Vector3(mycircle[i].radius * Mathf.Cos(theangle) - height, height, mycircle[i].radius * Mathf.Sin(theangle) - height);
        }

        myParticleSystem.SetParticles(myParticles, myParticles.Length);
    }

    void Update()
    {
        
        for (int i = 0; i < Parcount; i++)
        {              
            mycircle[i].angle = (mycircle[i].angle - Random.Range(1f, 6f)) % 360f;
            float radian = mycircle[i].angle / 180 * Mathf.PI;
            myParticles[i].position = new Vector3(mycircle[i].radius * Mathf.Cos(radian) * (myParticles[i].position.y - 20) / 20, myParticles[i].position.y, mycircle[i].radius * Mathf.Sin(radian) * (myParticles[i].position.y - 20) / 20);
        }
        myParticleSystem.SetParticles(myParticles, myParticles.Length);
    }

}

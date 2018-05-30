# Unity3d 粒子系统
-----

## 设计思路
在这里准备做一个锥形的粒子光环，即从下到上的粒子半径逐渐变小

### 1.创建一个空对象并添加Particle System控件

### 2.创建一个脚本HaloTest.cs

### 3.创建一个类保存半径和角度
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

### 4.设置粒子数量，粒子大小，最大半径和最小半径
	private int Parcount = 40000;                   //粒子数量
    private float Parsize = 0.1f;                   //粒子大小
    private float bigCircleRadius = 20f;            //最大半径
    private float smallCircleRadius = 10f;          //最小半径

### 5.设置开始时每个粒子的位置
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

### 6.让粒子旋转
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

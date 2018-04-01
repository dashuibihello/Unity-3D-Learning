# 1.解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。
  游戏对象 (GameObjects) 是一种容器。它们是空盒，能够容纳组成一个光照贴图的岛屿或物理驱动的小车的不同部分。 资源（Assets）就是你游戏会用到的资源，比如，模型文件，贴图文件，声音文件等等都算assets。游戏对象可以是资源但是两者并不等同。

----------

# 2.下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）

----------

# 3.编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件
## 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class InitBeh : MonoBehaviour {
	
    	void Awake() {
    	    Debug.Log("Init Awake");
    	}
	
    	// Use this for initialization
    	void Start () {
    	    Debug.Log("Init Start");
    	}
		
		// Update is called once per frame
		void Update () {
    	    Debug.Log("Init Update");
    	}
    	void FixedUpdate() {
    	    Debug.Log("Init FixedUpdate");
    	}
	
    	void LateUpdate() {
    	    Debug.Log("Init LateUpdate");
    	}
	}
发现最开始是Awake和Start,接下来则是以FixedUpdate，Update，LateUpdate出现。
##常用事件包括 OnGUI() OnDisable() OnEnable()

----------

# 4.查找脚本手册，了解 GameObject，Transform，Component 对象
## 分别翻译官方对三个对象的描述（Description）
GameObject: Unity场景中所有实体的基类<br>
Transform:  对象的位置，旋转和缩放。<br>
Component:	所有附加到GameObject的基类。<br>
## 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件

## 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）

----------

# 5.整理相关学习资料，编写简单代码验证以下技术的实现：
## 查找对象
## 添加子对象
## 遍历对象树
## 清除所有子对象
# 6.资源预设（Prefabs）与 对象克隆 (clone)
## 预设（Prefabs）有什么好处？
预设是指创建出一个游戏对象，然后把它作为一个游戏模板或原型，之后我们在使用类似的游戏对象的时候就，就可以把他拖拽到游戏场景中，来创建出一个新的游戏对象。

预设 的作用是：

1. 使游戏对象和资源重复利用；
2. 相同的游戏对象可以使用同一个预设来创建；
3. 对预设进行修改后，所有的游戏对象都会发生改变。

## 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
预设类似于一个模板，通过预设可以创建相同属性的对象，这些对象和预设关联。，对象克隆也可以创建相同属性的对象，但是对象克隆与本体无关联。即对于预设，一旦预设发生改变，所有通过预设实例化的对象都会产生相应的变化，而对象克隆不受克隆本体的影响，因此A对象克隆的对象B不会因为A的改变而相应改变。预设就有适于批量处理的好处。
## 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象
	    void Start () {
        GameObject table = (GameObject)Instantiate(Resources.Load("table"));
        table.transform.position = new Vector3(0, 3, 0);
        table.transform.parent = this.transform;
        Debug.Log("AnotherInit Start");
    }
# 7.尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法
## 向子对象发送消息
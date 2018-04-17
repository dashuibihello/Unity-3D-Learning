using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiskData : MonoBehaviour
{                             
    public Color color;                   
    public Vector3 direction;
    public float speed;
    public Dictionary<Color, int> scoreDictionary = new Dictionary<Color, int>();

    void Start()
    {
        scoreDictionary.Add(Color.yellow, 1);
        scoreDictionary.Add(Color.green, 2);
        scoreDictionary.Add(Color.black, 4);
        scoreDictionary.Add(Color.red, 8);
    }
}

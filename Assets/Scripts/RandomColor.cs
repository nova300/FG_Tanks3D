using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] GameObject colorObject;
    void Start()
    {
        Color randomColor = GetRandomColor();
        colorObject.GetComponent<MeshRenderer>().material.color = randomColor;
    }

    private Color GetRandomColor()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        return color;
    }
}

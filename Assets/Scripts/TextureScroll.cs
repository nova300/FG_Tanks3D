using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer s_renderer;
    private Vector2 savedOffset;

    void Start () {
        s_renderer = GetComponent<Renderer> ();
    }

    void Update () {
	float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
    float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
	Vector2 offset = new Vector2 (x, y);
	s_renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}

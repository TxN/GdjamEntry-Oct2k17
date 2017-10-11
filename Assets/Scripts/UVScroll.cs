using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour {
	public float ScrollSpeed_X = 0.5f;
	public float ScrollSpeed_Y = 0.5f;

	Renderer _ren;
	Material _mat;

	void Start () {
		_ren = GetComponent<Renderer>();
		_mat = _ren.material;
	}
	
	void Update () {
		_mat.mainTextureOffset = new Vector2(ScrollSpeed_X * Time.time, ScrollSpeed_Y * Time.time);
	}
}

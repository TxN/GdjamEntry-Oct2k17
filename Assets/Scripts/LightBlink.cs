using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightBlink : MonoBehaviour {

    Light _light;

    Sequence _seq;
    Material _mat;
    Renderer _renderer;
    Color _initColor;
    public float CycleTime = 0.5f;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
        _light = GetComponent<Light>();

        var ren = GetComponent<MeshRenderer>();


        if (ren) {
            _mat = ren.materials[0];
            _initColor = _mat.GetColor("_EmissionColor");
        }

        _seq = DOTween.Sequence();
        float initIntensity = _light.intensity;
       // _light.intensity = 0;
        _seq.Append(_light.DOIntensity(0, CycleTime * 0.5f));

        _seq.Append(_light.DOIntensity(initIntensity, CycleTime * 0.5f));
        _seq.SetLoops(-1);
    }
	
	// Update is called once per frame
	void Update () {
        if (_mat) {
            Color finalColor = _initColor * Mathf.LinearToGammaSpace(_light.intensity);
            _renderer.material.SetColor("_EmissionColor", finalColor);
            DynamicGI.SetEmissive(_renderer, finalColor);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSys;

public class EnergyBar : MonoBehaviour {
    public Image Bar;
    public float WarningLevel = 0.05f;

    SpaceGameState _state;

    Vector3 _initScale;

    void Start() {
        _state = SpaceGameState.Instance;
        _initScale = Bar.transform.localScale;
    }

    void Update() {
        float coef = _state.FuelLevel / _state.MaxFuelCapacity;
        Vector3 barScale = new Vector3(_initScale.x * coef, _initScale.y, _initScale.z);
        Bar.transform.localScale = barScale;
    }
}

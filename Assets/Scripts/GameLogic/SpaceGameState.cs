﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;
using DG.Tweening;

public enum WindowState {
    Shown,
    Hidden,
    Busy
}

public class SpaceGameState : MonoBehaviour {

    public GameObject UICanvas;
    public BBTManager BBTManager;


    public BoardingDialog PassengerDialog = null;

    public static SpaceGameState Instance = null;

    public List<string> CollectedCapusles = new List<string>();

    ControlsState _controlState = ControlsState.Unlocked;

    Rigidbody2D _playerShip = null;

    float _interactTimeout = 1f;

    public float MaxFuelCapacity = 250f;

    public float FuelPerBurn = 1f;

    private float _fuelLevel = 0f;

    private CapsuleManager _capsuleManager;

    public CanvasGroup Fader;
    Sequence _fadeSeq = null;

    public float FuelLevel {
        get {
            return _fuelLevel;
        }
    }

    public bool CanBurn {
        get {
            return FuelLevel > FuelPerBurn;
        }
    }

    public void BurnFuel() {
        if (CanBurn) {
            _fuelLevel -= FuelPerBurn;
        }
    }

    public int MaxCargo {
        get {
            return 10;
        }
    }

    public int CargoCount {
        get {
            return CollectedCapusles.Count;
        }
    }

    public bool CanBoard {
        get {
            return CargoCount <= MaxCargo;
        }
    }

    public float GetShipVelocity {
        get {
            return _playerShip.velocity.magnitude;
        }
    }
	public Dictionary<string, GameObject> SpawnedCapsules{
		get{
			return _capsuleManager.SpawnedCapsules;
		}
	}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        _capsuleManager = GetComponent<CapsuleManager>();

        _playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        _fuelLevel = MaxFuelCapacity;
    }

    void Start() {
        _capsuleManager.SpawnCapsules();
        Fader.gameObject.SetActive(true);
        _fadeSeq = TweenHelper.ReplaceSequence(_fadeSeq);
        _fadeSeq.AppendInterval(0.5f);
        _fadeSeq.Append(Fader.DOFade(0, 0.5f));
        _fadeSeq.AppendCallback(() => { Fader.gameObject.SetActive(false); });
    }

    void Update() {
        if (LockState == ControlsState.Unlocked) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            }
            
        }
    }

    private void OnDestroy() {
        _fadeSeq = TweenHelper.ReplaceSequence(_fadeSeq);
    }

    public ControlsState LockState {
        get {
            return _controlState;
        }
        set {
            if (value != _controlState) {
                _controlState = value;
                OnChangeControlsState();
            }
        }
    }

    void OnChangeControlsState() {
        if (_controlState == ControlsState.Locked) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }


    SafeCapsule _currentCapsule = null;
    public void InteractWithCapsule(SafeCapsule capsule) {

        if (Time.time < capsule.LastInteractTime + _interactTimeout) {
            return;
        }

        LockState = ControlsState.Locked;
        _currentCapsule = capsule;
        if (_currentCapsule) {
            _currentCapsule.LastInteractTime = Time.time;
            PassengerDialog.ShowWindow(_currentCapsule.CharacterId);
        }
        
    }

    public void DropCapsule(string capsuleId) {
        var ev = new Event_CapsuleDrop();
        ev.CapsuleId = capsuleId;
        EventManager.Fire<Event_CapsuleDrop>(ev);   
    }

    public void CollectCapsule() {
        if (_currentCapsule == null || !CanBoard) {
            return;
        }

        CollectedCapusles.Add(_currentCapsule.CharacterId);
        EventManager.Fire<Event_CapsuleCollect>(new Event_CapsuleCollect { CapsuleId = _currentCapsule.CharacterId });
        Destroy(_currentCapsule.gameObject);
        _currentCapsule = null;
    }



    public void Jump() {
        EventManager.Fire(new Event_Jump());
        WarpEffect effect = _playerShip.GetComponent<WarpEffect>();
        effect.enabled = true;
    }



}

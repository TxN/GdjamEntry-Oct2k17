using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;

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

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        _playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnDestroy() {

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
        Destroy(_currentCapsule);
        _currentCapsule = null;
    }






}

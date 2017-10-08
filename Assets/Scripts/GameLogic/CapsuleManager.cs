using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;

public class CapsuleManager : MonoBehaviour {
    public Dictionary<string, GameObject> SpawnedCapsules = new Dictionary<string, GameObject>();

    public List<PassengerInfo> _passengerInfoList = new List<PassengerInfo>();

    public GameObject BasicCapsuleFab = null;

    public float MaxSpawnRadius = 150;
    public float CapsuleBoundingRadius = 1f;

    bool _init = false;

    void Awake() {
        EventManager.Subscribe<Event_CapsuleCollect>(this, OnCapsuleCollect);
    }

    void OnDestroy() {
        EventManager.Unsubscribe<Event_CapsuleCollect>(OnCapsuleCollect);
    }

    void Start() {
        if (!_init) {
            Init();
        }
    }

    void Init() {
        _init = true;
        Object[] _capsuleInfoGos;
        _capsuleInfoGos = Resources.LoadAll("PassengerInfo", typeof(GameObject));
        foreach (GameObject item in _capsuleInfoGos) {
            var info = item.GetComponent<PassengerInfo>();
            _passengerInfoList.Add(info);
        }
    }

    public PassengerInfo GetInfo(string Id) {
        foreach (var item in _passengerInfoList) {
            if (item.Id == Id) {
                return item;
            }
        }
        return null;
    }

    public void SpawnCapsules() {
        if (!_init) {
            Init();
        }

        foreach (var item in _passengerInfoList) {
            if (item.SkipOnSpawn) {
                continue;
            }
            GameObject caps = Instantiate(BasicCapsuleFab);
            SafeCapsule sc = caps.GetComponent<SafeCapsule>();
            sc.IsVisibleOnRadar = true;
            sc.CharacterId = item.Id;
            Vector2 pos = Random.insideUnitCircle * MaxSpawnRadius + (Vector2)transform.position;
            while (Physics2D.OverlapCircle(pos, CapsuleBoundingRadius)) {
                pos = Random.insideUnitCircle * MaxSpawnRadius + (Vector2)transform.position;
            }
            caps.transform.position = pos;
            caps.transform.parent = transform;
            caps.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-100f, 100f);
            SpawnedCapsules.Add(item.Id, caps);
        }
    }

    public bool IsInSpace(string passengerID) {
        GameObject c = null;
        if (SpawnedCapsules.TryGetValue(passengerID, out c)) {
            return true;
        }
        return false;
    }

    void OnCapsuleCollect(Event_CapsuleCollect e) {
        SpawnedCapsules.Remove(e.CapsuleId);
    }
}

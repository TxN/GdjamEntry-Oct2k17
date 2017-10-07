using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarpEffect : MonoBehaviour {

    public GameObject EffectObj = null;

    void Start() {
        AsteroidsSpawn.Instance.enabled = false;
        SpaceGameState.Instance.LockState = ControlsState.Locked;
        Time.timeScale = 1f;
        CamControl.Instance.enabled = false;
        Sequence _seq = null;
        Vector3 curPos = transform.position;
        _seq = TweenHelper.ReplaceSequence(_seq);
        _seq.Append(transform.DOScale(1.1f, 0.2f));
        _seq.Append(transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f), 0.2f, 8, 1));
        _seq.AppendInterval(0.3f);
        _seq.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f, RotateMode.Fast));
        _seq.Append(transform.DOLocalMoveY((curPos.y + 150), 0.75f));

    }

    void Update() {
        
    }
}

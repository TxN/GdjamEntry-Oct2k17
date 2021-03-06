﻿using UnityEngine;
using DG.Tweening;

public class WarpEffect : MonoBehaviour {

    public GameObject EffectObj = null;

    void Start() {
        SpaceGameState.Instance.LockState = ControlsState.Locked;
        Time.timeScale = 1f;
        
        Sequence _seq = null;
        Vector3 curPos = transform.position;
        _seq = TweenHelper.ReplaceSequence(_seq);
        EffectObj.SetActive(true);
        _seq.Append(EffectObj.transform.DOScale(1.15f, 0.2f));
        _seq.Append(EffectObj.transform.DOPunchScale(new Vector3(0.2f, 0.15f, 0.15f), 0.2f, 8, 1));
        _seq.AppendInterval(0.5f);
        _seq.Append(transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f, RotateMode.Fast));
        _seq.AppendCallback(() => { CamControl.Instance.enabled = false; });
        _seq.Append(transform.DOLocalMoveX((curPos.x + 150), 0.75f));

    }

    void Update() {
        
    }
}

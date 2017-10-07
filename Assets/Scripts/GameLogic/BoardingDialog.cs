using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardingDialog : MonoBehaviour {

    public Button AcceptButton = null;
    public Button DeclineButton = null;

    public Image PortraitSprite = null;
    public Text UpperText  = null;
    public Text SecondText = null;

    WindowState _windowState = WindowState.Hidden;

    bool _isInit = false;

    SpaceGameState _state;

    void Init() {
        _isInit = true;
        _state = SpaceGameState.Instance;
    }

   public void ShowWindow(string passengerID) {
        if (!_isInit) {
            Init();
        }

        if (_windowState != WindowState.Hidden) {
            return;
        }

        PassengerInfo info = Resources.Load<GameObject>("PassengerInfo/"+ passengerID).GetComponent<PassengerInfo>();

        if (info.Portrait != null) {
            PortraitSprite.sprite = info.Portrait;
            PortraitSprite.gameObject.SetActive(true);
        } else {
            PortraitSprite.gameObject.SetActive(false);
            PortraitSprite.sprite = null;
        }

        UpperText.text = info.FirstDesc;
        SecondText.text = info.SecondDesc;
       

        _windowState = WindowState.Shown;
        gameObject.SetActive(true);

        AcceptButton.enabled = _state.CanBoard;
    }

    public void HideWindow() {
        if (!_isInit) {
            Init();
        }

        if (_windowState != WindowState.Shown) {
            return;
        }

        _windowState = WindowState.Hidden;

        _state.LockState = ControlsState.Unlocked;

        gameObject.SetActive(false);
    }

    public void CollectCapsule() {
        if (!_isInit) {
            Init();
        }

        if (_windowState != WindowState.Shown) {
            return;
        }

        _state.CollectCapsule();
        HideWindow();
    }
    
}

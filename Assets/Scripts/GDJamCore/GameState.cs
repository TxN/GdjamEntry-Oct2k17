using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum ControlsState {
	Locked,
	Unlocked	
}

public class GameState : MonoBehaviour {

	public GameObject UICanvas;
	public BBTManager BBTManager;

	public static GameState Instance = null;

	private void Awake(){
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this);
		}	
	}

	ControlsState _controlState = ControlsState.Unlocked;

	private List<InventoryItemInfo> _inventoryItems = new List<InventoryItemInfo>();

	public void AddInventoryItem(InventoryItemInfo item) {
		var itemInInv =  _inventoryItems.Find( x => x.Id.Equals(item.Id));
		if ( itemInInv == null ) {
			_inventoryItems.Add(item);
		} else {
			itemInInv.count += item.count;
		}
	}

	public InventoryItemInfo GetInventoryItem(string id) {
		var itemInInv = _inventoryItems.Find(x => x.Id.Equals(id));
		return itemInInv;
	}

	public bool RemoveInventoryItem(string id, int count = -1) {
		var itemInInv = _inventoryItems.Find(x => x.Id.Equals(id));
		if ( itemInInv == null ) {
			return false;
		} else {
			if ( count == -1 ) {
				_inventoryItems.Remove(itemInInv);
			} else {
				if ( count > itemInInv.count) {
					return false;
				}
				if ( count == itemInInv.count) {
					_inventoryItems.Remove(itemInInv);
				} else {
					itemInInv.count -= count;
				}

			}
			return true;
		}
	}

	public ControlsState LockState {
		get {
			return _controlState;
		}
		set {
			if ( value != _controlState ) {
				_controlState = value;
				OnChangeControlsState();
			}
		}
	}

	void OnChangeControlsState() {

	}
    void Test() {
        
    }


}

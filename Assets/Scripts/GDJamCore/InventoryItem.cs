using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {
	public string Id;
	public string ResourcesPath;
	public int Count;

	public InventoryItemInfo GetItemInfo() {
		return new InventoryItemInfo {
			Id = this.Id,
			ResourcesPath = this.ResourcesPath,
			count = this.Count
		};
	}
}

[System.Serializable]
public class InventoryItemInfo {
	public string Id;
	public string ResourcesPath;
	public int count;
}

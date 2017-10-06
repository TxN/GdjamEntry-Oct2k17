public interface IInteractable  {
	void Interact();
	InteractableType GetInteractableType();
	void IsInteractable();
}
public enum InteractableType {
	Narrative,
	Pickup,
	Action,
	Other
}

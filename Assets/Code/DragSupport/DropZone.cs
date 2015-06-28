using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IDropCallback {
	 void OnDrop (GameObject target);
}

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public GameObject controller;

	bool chosen = false;

	public void OnDrop(PointerEventData eventData){
		Debug.Log (eventData.pointerDrag.name +"was dropped on " + gameObject.name); 
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null && chosen == false) {
			d.parentToReturnTo = this.transform;
			d.transform.SetAsLastSibling();
			chosen = true;
			controller.GetComponent<IDropCallback>().OnDrop(eventData.pointerDrag);
		}

	}

	public void OnPointerEnter(PointerEventData eventData){
		
		//Debug.Log ("OnDPointEnter"); 
	}

	public void OnPointerExit(PointerEventData eventData){
		
		//Debug.Log ("OnPointExit"); 
	}
}

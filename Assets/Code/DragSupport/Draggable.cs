using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler {

	public Transform parentToReturnTo = null;

	GameObject placeholder = null;

	//public Color startcolor;

	public DropZone zone;

	
	public void OnBeginDrag(PointerEventData eventData) {

		//Debug.Log ("OnBeginDrag");
		placeholder = new GameObject ();
		placeholder.transform.SetParent (this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement> ();
		le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
		le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
		le.flexibleHeight = 0;
		le.flexibleWidth = 0;

		placeholder.transform.SetSiblingIndex (this.transform.GetSiblingIndex ());


		parentToReturnTo = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;



		//zone = GameObject.FindObjectOfType<DropZone>();
		//startcolor = zone.GetComponentInParent<Renderer>().material.color;
		//zone.GetComponentInParent<Renderer> ().material.color = Color.yellow;
	
	}

	public void OnDrag(PointerEventData eventData){
		this.transform.position = eventData.position;

		RectTransform transform = ((RectTransform)this.transform);
		Debug.Log (transform.position);

		//int newSiblingIndex = placeholder.transform.GetSiblingIndex;
		int newSiblingIndex = parentToReturnTo.childCount; 

		for (int i=0; i< parentToReturnTo.childCount; i++) {

			if(this.transform.position.x < parentToReturnTo.GetChild(i).position.x){

				newSiblingIndex = i;

				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;
				break;
			}
		}
		placeholder.transform.SetSiblingIndex(newSiblingIndex);
	}

	public void OnEndDrag(PointerEventData eventData){
		
		Debug.Log ("OnEndDrag");
		this.transform.SetParent(parentToReturnTo);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (zone != null && this.transform.parent == zone.transform) {
			// Dropped in drop zone
			// Stay where it is
			RectTransform transform = ((RectTransform)this.transform);
			RectTransform parent = ((RectTransform)this.transform.parent);
			transform.position = parent.position;
		} else {
			this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ()); 
		}
		Destroy (placeholder);

		//zone.GetComponentInParent<Renderer> ().material.color = startcolor;
	}
}

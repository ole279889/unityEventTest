using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Text pressBtnMessage;
	[SerializeField] public Text pressedBtnsCounterMessage;
	private UnityEvent myEvent = new UnityEvent();
	
    void Start ()
	{
		pressBtnMessage.enabled = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ButtonManager buttonManager = ButtonManager.instance;
		pressBtnMessage.enabled = true;
		buttonManager.setTargetButton(eventData.pointerEnter);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ButtonManager buttonManager = ButtonManager.instance;
		pressBtnMessage.enabled = false;
		buttonManager.clearTargetButton();
	}
	
	void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("F")))
        {
			ButtonManager buttonManager = ButtonManager.instance;
			buttonManager.executeTargetButtonAction();
			buttonManager.rememberTargetIfFirst();
			pressedBtnsCounterMessage.text = "Buttons pressed: " + buttonManager.getPressedCount();
        }
    }
}

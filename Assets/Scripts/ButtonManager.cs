using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
	[SerializeField] public GameObject showTextButton;
	[SerializeField] public GameObject openDoorButton;
	[SerializeField] public GameObject playSoundButton;
	[SerializeField] public GameObject delayedExplosionButton;
	[SerializeField] public AudioClip delayedExplosionClip;
	
    public static ButtonManager instance = null;
	
    private GameObject targetButton = null;
	private HashSet<GameObject> pressedButtons = new HashSet<GameObject>();
	private Animator _animator;
	private GameObject door;
	private Text pressBtn1Message;
	private AudioSource audio;
	private CameraShake cameraShake;
	private bool isCoroutineExecuting = false;
	
	void Start () {
		if (instance == null) {
	        instance = this;
	    } else if(instance == this){
	        Destroy(gameObject);
	    }
		InitManager();
    }
	 
	public void setTargetButton(GameObject button) {
		targetButton = button;
	}
	
	public void clearTargetButton() {
		targetButton = null;
	}
	
	public GameObject getTargetButton() {
		return targetButton;
	}
	
	public void rememberTargetIfFirst() {
		if (targetButton != null) {
			pressedButtons.Add(targetButton);
		}
	}
	
	public int getPressedCount() {
		return pressedButtons.Count;
	}
	
	public void executeTargetButtonAction() {
		
		if (targetButton == null) return;
		_animator = targetButton.GetComponent<Animator>();
		_animator.SetTrigger("PressTrigger");
		
		if (GameObject.Equals(targetButton, showTextButton)) {
			StartCoroutine(ShowPressBtn1Message(2));
		} else if (GameObject.Equals(targetButton, openDoorButton)) {
            door.GetComponent<Animator>().SetTrigger("OpenDoor");
		} else if (GameObject.Equals(targetButton, playSoundButton)) {
            audio.Play();
		} else if (GameObject.Equals(targetButton, delayedExplosionButton)) {
			StartCoroutine(ExecuteAfterTime(3f, () =>
            {     
                audio.PlayOneShot(delayedExplosionClip, 0.5f);	
                StartCoroutine(cameraShake.shake(2f, 0.2f));
            }));
        } else {
			print("unknown button object!");            
		}
	}
	
	private void InitManager() {
		door = GameObject.Find("01_low");
	    pressBtn1Message = GameObject.Find("Btn1PressedText").GetComponent<Text>();
	    audio = GameObject.Find("MsgCanvas").GetComponent<AudioSource>();
		cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
	    DontDestroyOnLoad(gameObject);
	}
	
	private IEnumerator ExecuteAfterTime(float time, Action action)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        action();
        isCoroutineExecuting = false;
    }
	
	private IEnumerator ShowPressBtn1Message(float duration) {
	    pressBtn1Message.enabled = true;
		yield return new WaitForSeconds(duration);
		pressBtn1Message.enabled = false;
    }
}

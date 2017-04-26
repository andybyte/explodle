using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureController : MonoBehaviour {
	public Transform tempPointer;
	private Vector3 startPos;
	private Vector3 currentPos;
	private Vector3 overheatPos;
	private Vector3 maxPos;
	private Vector3 minPos;
	private bool overheat;
	public float decrementSpeed;
	public float incrementSpeed;
	public bool cool;
	private float y;
	private float z;
	private CountDown countDown;
	public Code codeScript;
	private bool alreadyRun;
	private string tempCodeString;
	public AudioSource overheatSound;
	private bool audioPlaying;
	private bool overheating;
	public GameObject overheatEffect;
	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		countDown = GameObject.Find ("GameManager").GetComponent<CountDown> ();
		GameManager gameManager = GetComponent<GameManager> ();
		Code codeScript = GetComponent<Code> ();
		AudioSource overheatSound = GetComponent<AudioSource>();

		overheatEffect.SetActive (false);

		audioPlaying = false;

		alreadyRun = false;

		overheat = false;
		overheating = false;
		y = 0.39f;
		z = -2.523f;
		overheatPos = new Vector3 (-3.32f, y, z);

		startPos = new Vector3 (-3.8f, y, z);
		currentPos = startPos;

		maxPos = new Vector3 (-2.445f, y, z);
		minPos = new Vector3 (-3.87f, y, z);

		tempPointer = GameObject.Find("Temperature Pointer").GetComponent<Transform> ();
		transform.position = currentPos;

		incrementSpeed = 0.02f;
		decrementSpeed = 0.0002f; 

		cool = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!overheat) {
			if (currentPos.x >= minPos.x) {
				if (!overheating) {
					currentPos = new Vector3 (transform.position.x + decrementSpeed, y, z);
					transform.position = currentPos;
				} else if (overheating) {
					currentPos = new Vector3 (transform.position.x + decrementSpeed * 3, y, z);
					transform.position = currentPos;
				}
			}

			if (currentPos.x > minPos.x) {//allows to cool down if not all the way cool
				if (Input.GetKeyDown ("space") || cool) {
					Debug.Log ("cool down dammit");
					currentPos = new Vector3 (transform.position.x - incrementSpeed, y, z);
					transform.position = currentPos;
					cool = false;
				}
			} else if (currentPos.x < minPos.x) {
				currentPos = minPos;
				transform.position = currentPos;
			}

			if (currentPos.x > overheatPos.x) {
				if (audioPlaying == false) {
					overheatSound.Play ();
					overheating = true;
					audioPlaying = true;
				}
				overheatEffect.SetActive (true);
				StartCoroutine (OverheatCheck ());
			}else if((currentPos.x < overheatPos.x) && overheating){
				if (audioPlaying == true) {
					overheating = false;
					audioPlaying = false;
					overheatSound.Stop ();
				}
				overheatEffect.SetActive (false);
			}

			/*if((countDown.CountDownTime > 0) && !alreadyRun){
				tempCodeString = codeScript.CodeStringGetter;
				if (tempCodeString.Length == 3) {
					codeScript.AddToCode ();
				}
			} */
		}
	}

	IEnumerator OverheatCheck(){
		yield return new WaitForSecondsRealtime (10);
		if (currentPos.x > overheatPos.x) {
			overheat = true;
			overheatEffect.SetActive (false);
			overheatSound.volume = 0.0f;
			gameManager.GameOver ();
			countDown.CountDownTime = 0.0f;
		} else {
			yield return new WaitForSecondsRealtime (0.01f);
		}
	}
}

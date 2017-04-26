using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
	public static float startTime;
	public static float currentTime;
	private string currentTimeString;
	private int seconds;
	private int minutes;
	public AudioSource tickingSound;
	public GameManager gameManager;
	public TextMesh countdownTime;
	private string countdownString;


	// Use this for initialization
	void Start () {
		startTime = (float)(60 * 5);
		currentTime = startTime;
		countdownString = "";

		TextMesh countdownTime = (TextMesh) gameObject.GetComponent(typeof(TextMesh));
		GameManager gameManager = GetComponent<GameManager> ();
		AudioSource tickingSound = GetComponent<AudioSource> ();
		AudioSource gameOverSound = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (currentTime > 0) {
			currentTime -= Time.deltaTime;
			//Debug.Log (currentTime);
		} else {
			currentTime = (float)0;
			gameManager.GameOver ();
		}

		minutes = (int)(currentTime / 60);
		seconds = (int)(currentTime % 60);

		if (seconds < 10) {
			currentTimeString = minutes + ":0" + seconds;
		} else {
			//currentTimeString = (int)(currentTime / 60) + ":" + (int)(currentTime % 60);
			currentTimeString = minutes + ":" + seconds;
		}


		if(countdownTime.text != currentTimeString.ToString()){
			countdownTime.text = currentTimeString;
		}


			
	}

	public float CountDownTime{
		get{
			return currentTime;
		}
		set{
			currentTime = value;
		}
	}

	public void StopCountdownSound(){
		tickingSound.Stop ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour {
	public AudioSource gameOverSound;
	public GameObject gameOverUI;
	public GameObject winUI;
	public CountDown countdown;
	public Code code; 

	// Use this for initialization
	void Start () {
		gameOverUI.SetActive (false);
		winUI.SetActive (false);
		CountDown countdown = GetComponent<CountDown> ();
		Code code = GetComponent<Code> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LCDChange(string newLCD,string winOrLose){
		code.CodeStringGetter = "";
		code.CodeStringGetter = newLCD;
		yield return new WaitForSecondsRealtime (2);

		if (winOrLose == "win") {
			winUI.SetActive (true);
		} else if (winOrLose == "game over") {
			gameOverUI.SetActive (true);
		}
	}

	public void GameOver(){
		countdown.StopCountdownSound ();
		gameOverSound.Play ();
		Time.timeScale = 0.0f;
		StartCoroutine (LCDChange ("Defeat", "game over"));
	}

	public void Win(){
		countdown.StopCountdownSound ();
		Time.timeScale = 0.0f;
		StartCoroutine (LCDChange ("Victory", "win"));
	}

	public void Quit(){
		Debug.Log ("APPLICATION QUIT!");
		Application.Quit ();
	}

	public void Restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}

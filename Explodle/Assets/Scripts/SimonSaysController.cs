using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysController : MonoBehaviour {
	public static List<GameObject> colors;
	public static List<string> keys;
	public static List<int> pattern;//will hold randomly generated numbers(0-3) to reference color and key association
	public string stringOfInput;
	public static string patternString;
	public string currentPatternString;
	public static int round;
	private bool roundComplete;
	public GameObject redLight;
	public GameObject blueLight;
	public GameObject yellowLight;
	public GameObject greenLight;
	public GameObject noLight;
	private int frameNum;
	private bool displayFinished;
	private bool displayInProgress;
	private bool simonComplete;
	//public GameObject completeIndicator;
	//public GameObject notCompleteIndicator;
	public Code codeScript;
	private bool alreadyRun;
	public AudioSource correct;
	public AudioSource wrong;
	public AudioSource beep;
	public AudioSource boop;
	private int colorNum;
	public CountDown countDown;

	public string player2String;
	public GameObject player2;

	// Use this for initialization
	void Start () {

		player2 = GameObject.Find ("ArduinoLogic");
		Code codeScript = GetComponent<Code> ();
		CountDown countDown = GetComponent<CountDown> ();
		AudioSource correct = GetComponent<AudioSource> ();
		AudioSource wrong = GetComponent<AudioSource> ();
		AudioSource beep = GetComponent<AudioSource> ();
		AudioSource boop = GetComponent<AudioSource> ();

		alreadyRun = false;

		redLight.SetActive(false);
		blueLight.SetActive (false);
		yellowLight.SetActive(false);
		greenLight.SetActive (false);
		noLight.SetActive (true);

		//notCompleteIndicator.SetActive (true);
		//completeIndicator.SetActive (false);

		stringOfInput = "";
		patternString = "";
		currentPatternString = "";
		displayInProgress = false;

		colors = new List<GameObject> ();
		colors.Insert (0,redLight);
		colors.Insert (1,blueLight);
		colors.Insert (2,yellowLight);
		colors.Insert (3,greenLight);

		keys = new List<string> ();
		keys.Insert (0,"r");
		keys.Insert (1,"b");
		keys.Insert (2,"y");
		keys.Insert (3,"g");

		pattern = new List<int> ();
		colorNum = 7;

		for (int i = 0; i < colorNum; i++) {
			int num = Random.Range (0, 4);
			pattern.Add(num);//picks num 0-3 and adds to pattern list
		}

		for (int j = 0; j < colorNum; j++) {
			patternString += keys[pattern[j]];
		}

		round = 1;
		roundComplete = false;
		simonComplete = false;

		currentPatternString += patternString [0];

		displayFinished = false;

		player2String = "";
	}

	// Update is called once per frame
	void Update () {
		frameNum += 1;

		if(((frameNum % (60*6)) == 0) && (frameNum != 0) && !simonComplete && !displayInProgress){
			displayInProgress = true;

			Debug.Log ("Pattern: " + currentPatternString);

			if (!displayFinished) {
				StartCoroutine (Display ());
			} else {
				StartCoroutine (Redisplay ());
			}
		}

		if(displayFinished){
//			if (Input.anyKeyDown && !Input.GetKeyDown ("space")) {
//				stringOfInput += Input.inputString;
//			}

			if (stringOfInput.Length == currentPatternString.Length) {
				if (stringOfInput == currentPatternString) {
					player2.GetComponent<ButtonController>().rightAnswer = true;
					round += 1;
					Debug.Log ("Round: " + round);
					currentPatternString += keys [pattern [round - 1]];
					stringOfInput = "";
					displayFinished = false;
					roundComplete = true;
				} else {
					player2.GetComponent<ButtonController>().wrongAnswer = true;
					wrong.Play ();
					stringOfInput = "";
					countDown.CountDownTime -= 5.0f;
				}
			}
		}//end of input key

//		Debug.Log (round.ToString () + " " + colorNum.ToString () + " " + stringOfInput);

		if (round == colorNum) {
			simonComplete = true;

			colorNum += 1;
			simonComplete = false;

			SimonReset (colorNum);
		}

		if (simonComplete && !alreadyRun) {
			correct.Play ();
			alreadyRun = true;
			//codeScript.AddToCode ();
		}

	}

	IEnumerator Display(){
		roundComplete = false;
		if((round < 8)){
				for (int r = 0; r < round; r++) {
					yield return new WaitForSecondsRealtime (1f);//1.75
					StartCoroutine (Pause (.75f));//1.5
					colors[pattern[r]].SetActive(true);

					if (r == 0) {
						boop.Play ();
					}

					StartCoroutine (TurnOff(colors[pattern[r]]));

					if (r == (round-1) && (r != 0)){
						beep.Play();
					}
				}
				displayFinished = true;
		}
		displayInProgress = false;

	}

	IEnumerator TurnOff(GameObject light){
		yield return new WaitForSecondsRealtime (.75f);//1.5
		light.SetActive (false);
	}

	IEnumerator Pause(float secs){
		yield return new WaitForSecondsRealtime (secs);
	}

	IEnumerator Redisplay(){
		yield return new WaitForSecondsRealtime ((float)(1.5f * round));
		StartCoroutine(Display ());
	}

	void SimonReset(int numOfColors){
		Debug.Log (countDown.CountDownTime.ToString ());
		countDown.CountDownTime += 30.0f;
		Debug.Log (countDown.CountDownTime.ToString ());

		round = 1;
		roundComplete = false;

		pattern.Clear ();
		for (int i = 0; i < numOfColors; i++) {
			int num = Random.Range (0, 4);
			pattern.Add(num);//picks num 0-3 and adds to pattern list
		}

		patternString = "";
		for (int j = 0; j < numOfColors; j++) {
			patternString += keys[pattern[j]];
		}

		currentPatternString = "";
		currentPatternString += patternString [0];
	}

	/* void TurnOnCompleteLight(){
		completeIndicator.SetActive (true);
	}*/

}

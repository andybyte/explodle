using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysController : MonoBehaviour {
	public static List<GameObject> colors;
	public static List<string> keys;
	public List<int> pattern;//will hold randomly generated numbers(0-3) to reference color and key association
	private int round;
	private bool roundComplete;
	public GameObject redLight;
	public GameObject blueLight;
	public GameObject yellowLight;
	public GameObject greenLight;
	public GameObject noLight;

	// Use this for initialization
	void Start () {
		redLight.SetActive(false);
		blueLight.SetActive (false);
		yellowLight.SetActive(false);
		greenLight.SetActive (false);
		noLight.SetActive (true);

		List<GameObject> colors = new List<GameObject> ();
		colors.Insert (0,redLight);
		colors.Insert (1,blueLight);
		colors.Insert (2,yellowLight);
		colors.Insert (3,greenLight);

		List<string> keys = new List<string> ();
		keys.Insert (0,"Keycode.R");
		keys.Insert (1,"Keycode.B");
		keys.Insert (2,"Keycode.Y");
		keys.Insert (3,"Keycode.G");

//		List<int> pattern = new List<int> ();

		round = 1;
		roundComplete = false;

		for (int i = 0; i < 10; i++) {
			int num = Random.Range (0, 4);
			pattern.Add(num);//picks num 0-3 and adds to pattern list
		}

		for (int j = 0; j < 10; j++) {
			Debug.Log (pattern[j]);//picks num 1-4 and adds to pattern list
		}
	}
	
	// Update is called once per frame
	void Update () {
		if((round < 11) && (roundComplete != true)){
			if (!roundComplete) {
				for (int r = 0; r < round; r++) {
					int patternNum = pattern[r]; //null reference exception, why????
					noLight.SetActive (false);
					StartCoroutine (DisplayPattern (patternNum));
					noLight.SetActive (true);
				}
				//detect input
			}
			round++;
		}

	}

	IEnumerator DisplayPattern(int patternNum){
		
			switch (patternNum) {
			case 3:
				greenLight.SetActive (true);
				yield return new WaitForSecondsRealtime (2);
				greenLight.SetActive (false);
				break;
			case 2: 
				yellowLight.SetActive (true);
				yield return new WaitForSecondsRealtime (2);
				yellowLight.SetActive (false);
				break;
			case 1:
				blueLight.SetActive (true);
				yield return new WaitForSecondsRealtime (2);
				blueLight.SetActive (false);
				break;
			case 0:
				redLight.SetActive (true);
				yield return new WaitForSecondsRealtime (2);
				redLight.SetActive (false);
				break;
			}
	}
}

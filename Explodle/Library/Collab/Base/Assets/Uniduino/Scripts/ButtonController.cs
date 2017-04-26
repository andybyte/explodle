using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ButtonController : MonoBehaviour {

	public Arduino arduino;

	public GameObject simonsays;
	public GameObject temp;

	// Buttons
	public int btnRed;
	public int btnYlw;
	public int btnGrn;
	public int btnBlu;
	public int btnBlow;

	// Leds
	public int redLed;
	public int yellowLed;
	public int greenLed;
	public int blueLed;

	void Start () {

		simonsays = GameObject.Find ("Simon Says");
		temp = GameObject.Find ("Temperature Pointer");
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);

		StartCoroutine (ButtonReader());
	}

	void ConfigurePins() {
		// Buttons
		arduino.pinMode(btnRed, PinMode.INPUT);
		arduino.reportDigital ((byte)(btnRed / 8), 1);
		arduino.pinMode(btnYlw, PinMode.INPUT);
		arduino.reportDigital ((byte)(btnYlw / 8), 1);
		arduino.pinMode(btnGrn, PinMode.INPUT);
		arduino.reportDigital ((byte)(btnGrn / 8), 1);
		arduino.pinMode(btnBlu, PinMode.INPUT);
		arduino.reportDigital ((byte)(btnBlu / 8), 1);
		arduino.pinMode(btnBlow, PinMode.INPUT);
		arduino.reportDigital ((byte)(btnBlow / 8), 1);
		// Leds
		arduino.pinMode(redLed, PinMode.OUTPUT);
		arduino.pinMode(yellowLed, PinMode.OUTPUT);
		arduino.pinMode(greenLed, PinMode.OUTPUT);
		arduino.pinMode(blueLed, PinMode.OUTPUT);
	}

	// Leds
	IEnumerator ButtonReader() {
		while (true) {
			if (arduino.digitalRead (btnRed) == 1) {
				Debug.Log ("pressed Red");
				arduino.digitalWrite (redLed, Arduino.HIGH); // led ON
				yield return new WaitForSeconds (1);
				arduino.digitalWrite (redLed, Arduino.LOW); // led OFF
				simonsays.GetComponent<SimonSaysController> ().stringOfInput = simonsays.GetComponent<SimonSaysController> ().stringOfInput + "r";
			} else if (arduino.digitalRead (btnYlw) == 1) {
				Debug.Log ("pressed Yellow");
				arduino.digitalWrite (yellowLed, Arduino.HIGH); // led ON
				yield return new WaitForSeconds (1);
				arduino.digitalWrite (yellowLed, Arduino.LOW); // led OFF
				simonsays.GetComponent<SimonSaysController> ().stringOfInput = simonsays.GetComponent<SimonSaysController> ().stringOfInput + "y";
			} else if (arduino.digitalRead (btnGrn) == 1) {
				Debug.Log ("pressed Green");
				arduino.digitalWrite (greenLed, Arduino.HIGH); // led ON
				yield return new WaitForSeconds (1);
				arduino.digitalWrite (greenLed, Arduino.LOW); // led OFF
				simonsays.GetComponent<SimonSaysController> ().stringOfInput = simonsays.GetComponent<SimonSaysController> ().stringOfInput + "g";
			} else if (arduino.digitalRead (btnBlu) == 1) {
				Debug.Log ("pressed Blue");
				arduino.digitalWrite (blueLed, Arduino.HIGH); // led ON
				yield return new WaitForSeconds (1);
				arduino.digitalWrite (blueLed, Arduino.LOW); // led OFF
				simonsays.GetComponent<SimonSaysController> ().stringOfInput = simonsays.GetComponent<SimonSaysController> ().stringOfInput + "b";
			} else if (arduino.digitalRead (btnBlow) == 1) {
				Debug.Log ("pressed Blow");
				yield return new WaitForSeconds (0.25f);
				simonsays.GetComponent<SimonSaysController> ().stringOfInput = "";
				temp.GetComponent<TemperatureController> ().cool = true;
			} else {
				yield return new WaitForSeconds (0.01f);
			}	
		}
	}

	void Update () {       
//		if (colors.Length == 4) {
//			Debug.Log ("You entered: " + colors);
//			colors = "";
//		}
	}
}
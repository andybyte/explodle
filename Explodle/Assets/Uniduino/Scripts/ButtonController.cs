using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ButtonController : MonoBehaviour {

	public Arduino arduino;

	public GameObject simonsays;
	public GameObject temp;

	// Controller feedback
	public string inputPattern;
	public float inputPatternWait;
	private int inputPointer = 0;
	public int buzzerPin;
	public bool rightAnswer = false;
	public bool wrongAnswer = false;

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
		StartCoroutine (ColorReader());
		StartCoroutine (BuzzerFeedback());
	}

	void ConfigurePins() {
		// Feedback
		arduino.pinMode(buzzerPin, PinMode.PWM);
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

	// Buzzer
	IEnumerator BuzzerFeedback() {
		while (true) {

			if (wrongAnswer) { // Wrong
				arduino.analogWrite(buzzerPin, 20);
				yield return new WaitForSeconds (0.5f);
				arduino.analogWrite(buzzerPin, 0);
				wrongAnswer = false;
			} else if (rightAnswer) { // Success
				yield return new WaitForSeconds (0.2f);
				arduino.analogWrite(buzzerPin, 450);
				yield return new WaitForSeconds (0.2f);
				arduino.analogWrite(buzzerPin, 650);
				yield return new WaitForSeconds (0.2f);
				arduino.analogWrite(buzzerPin, 750);
				yield return new WaitForSeconds (0.2f);
				arduino.analogWrite(buzzerPin, 0);
				yield return new WaitForSeconds (0.2f);
				rightAnswer = false;
			} else {
				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	// Leds
	// Give Led feedback to what has been entered so far.
	IEnumerator ColorReader() {
			while(true) {
				if (inputPattern.Length > 0 && inputPointer < inputPattern.Length) {
					switch (inputPattern[inputPointer]) {
						case 'r':
							arduino.digitalWrite (redLed, Arduino.HIGH); // led ON
							yield return new WaitForSeconds (inputPatternWait);
							arduino.digitalWrite (redLed, Arduino.LOW); // led OFF
							break;
						case 'y':
							arduino.digitalWrite (yellowLed, Arduino.HIGH); // led ON
							yield return new WaitForSeconds (inputPatternWait);
							arduino.digitalWrite (yellowLed, Arduino.LOW); // led OFF
							break;
						case 'g':
							arduino.digitalWrite (greenLed, Arduino.HIGH); // led ON
							yield return new WaitForSeconds (inputPatternWait);
							arduino.digitalWrite (greenLed, Arduino.LOW); // led OFF
							break;
						case 'b':
							arduino.digitalWrite (blueLed, Arduino.HIGH); // led ON
							yield return new WaitForSeconds (inputPatternWait);
							arduino.digitalWrite (blueLed, Arduino.LOW); // led OFF
							break;
						default:
							break;
					}
					yield return new WaitForSeconds (0.1f);
					inputPointer += 1;
				} else {
					// After giving pattern feedback, start over and show it again
					yield return new WaitForSeconds (0.5f);
					inputPattern = simonsays.GetComponent<SimonSaysController> ().stringOfInput;
					if (inputPattern.Length == 1 || inputPointer == inputPattern.Length) {
						inputPointer = 0;
					}
				}
			}
	}

	// Give feedback and record what player enters
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Code : MonoBehaviour {
	public TextMesh codeText;
	public TextMesh countdownTime;
	private string codeString;
	private string countdownString;
	private string inputCodeString;
	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		TextMesh codeText = (TextMesh) gameObject.GetComponent(typeof(TextMesh));//GameObject.Find("LCD Text").GetComponent<Text>();
		TextMesh countdownTime = (TextMesh) gameObject.GetComponent(typeof(TextMesh));
		GameManager gameManager = GetComponent<GameManager>();
		codeString = "";
		countdownString = "";
	}
	
	// Update is called once per frame
	void Update () {
		codeText.text = codeString;

		if(Input.anyKeyDown){
			inputCodeString += Input.inputString;
			Debug.Log ("Code input: " + inputCodeString);
		}

		if (codeString.Length == 5) {
			if(inputCodeString.Contains(codeString)){
				gameManager.Win ();
			}
		}
	}

	public void AddToCode(){
		for(int i = 0; i < 5; i++){
			int randCodeNum = Random.Range (0, 10);
			codeString += randCodeNum;
		}
	}

	public string CodeStringGetter{
		get{
			return codeString;
		}
		set{
			codeString = value;
		}
	}
}

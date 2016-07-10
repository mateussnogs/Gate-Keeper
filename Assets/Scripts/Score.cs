using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public static int score = 0;
	public Text scoreText;
	public Text copyText;
	// Use this for initialization
	void Start () {
		
		Object.DontDestroyOnLoad (transform.root.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString ();
	}


	public static void IncreaseScore(int howMuch) {
		score += howMuch;
	}


}

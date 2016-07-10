using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartListener() {
		SceneManager.UnloadScene ("Scene01");
		SceneManager.LoadScene ("Scene01");
	}
}

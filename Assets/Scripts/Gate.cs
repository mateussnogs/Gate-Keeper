using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	public int hp = 10;
	public GameObject lifeBar;

	float mfX;

	// Use this for initialization
	void Start () {
		mfX = lifeBar.transform.position.x - lifeBar.transform.localScale.x/2.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetHit(int dmg) {
		hp -= dmg;

		Vector3 locScale = lifeBar.transform.localScale;
		locScale.x = locScale.x * hp / 10;
		lifeBar.transform.localScale = locScale;
	}
}

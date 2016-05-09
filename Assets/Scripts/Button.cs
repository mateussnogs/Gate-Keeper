using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	bool active;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	public void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		SetTransparent (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive(bool active) {
		this.active = active;
		if (active) {
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<Collider2D> ().enabled = true;
		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<Collider2D> ().enabled = false; 
		}				
	}

	public void SetTransparent(bool set) {
		Color temp = spriteRenderer.color;
		if (!set) {			
			temp.a = 1;
			StartCoroutine (UnsetTransparency());

		} else {
			temp.a = 0.5f;
		}
		spriteRenderer.color = temp;
			
	}	

	IEnumerator UnsetTransparency() {
		yield return new WaitForSeconds (0.15f);
		SetTransparent (true);
	}
		
}

using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {
	private Amelia amelia;
	Knight knight;

	public float cooldownToFind;
	public float findTimeAcum;
	// Use this for initialization
	void Start () {
		GameObject ameliaGO = GameObject.FindGameObjectWithTag ("Amelia");
		amelia = ameliaGO.GetComponent<Amelia>();
		knight = GetComponent<Knight>();
	}
	void Update () {
		if (FoundAmelia ())
			knight.Attack ();
		if (CooldownDone())
			FindAmelia ();
		if (knight.movingLeft)
			knight.Move (false);
		else if (knight.movingRight)
			knight.Move (true);
	}

	void FindAmelia() {
		if (amelia.transform.position.x > transform.position.x) {
			knight.movingRight = true;
			knight.movingLeft = false;
		} else {
			knight.movingLeft = true;
			knight.movingRight = false;
		}
	}

	bool FoundAmelia() {
		if (knight.EnemyInReach(amelia.transform.position))
			return true;			
		return false;
	}

	bool CooldownDone() {
		if (Time.time > findTimeAcum) {
			findTimeAcum = Time.time + cooldownToFind;
			return true;
		}
		return false;
	}		



}

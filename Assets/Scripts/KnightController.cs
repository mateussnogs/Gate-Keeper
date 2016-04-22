using UnityEngine;
using System.Collections;

public class KnightController : MonoBehaviour {
	private Amelia amelia;
	Knight knight;

	Animator anim;
	public float cooldownToFind, cooldownToWalk, cooldownToWaitToWalk;
	float findTimeAcum, walkingTime, waitingWalkTime;
	public bool waitingToWalkState = true;

	bool beginTimerWalk = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		GameObject ameliaGO = GameObject.FindGameObjectWithTag ("Amelia");
		amelia = ameliaGO.GetComponent<Amelia>();
		knight = GetComponent<Knight>();
	}
	void Update () {
		if (FoundAmelia ())
			knight.Attack ();
		if (CooldownDone())
			FindAmelia ();
		if (knight.backingOff) {
			knight.BackOff ();
		}
		if (WalkMachineState()) {
			if (knight.movingLeft)
				knight.Move (false);
			else if (knight.movingRight)
				knight.Move (true);
		}
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

	bool WalkMachineState() {
		anim.SetBool ("Moving", false);
		if (waitingToWalkState) {
			if (beginTimerWalk) {
				beginTimerWalk = false;
				waitingWalkTime = Time.time + cooldownToWalk;
			}
			if (Time.time > waitingWalkTime) {
				waitingToWalkState = false; // Troco de estado
				beginTimerWalk = true;
			}
		}

		else {
			if (beginTimerWalk) {
				beginTimerWalk = false;
				walkingTime = Time.time + cooldownToWaitToWalk;
			}
				
			if (Time.time > walkingTime) {
				waitingToWalkState = true; // Troco de estado
				beginTimerWalk = true;
			}
		}

		if (waitingToWalkState)
			return true;
		else
			return false;
	}



}

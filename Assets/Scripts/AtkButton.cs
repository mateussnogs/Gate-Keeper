﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtkButton : Button {
	public enum AtkButtonID{Sword, Axe, Spear};
	public AtkButtonID id;
	float timerAcc;
	float initialCd;
	public float cd;
	public Text numberCdText;
	public bool activated;
	Amelia amelia;
	public 
	// Use this for initialization
	void Start () {
		base.Start ();
		amelia = GameObject.FindGameObjectWithTag ("Amelia").GetComponent<Amelia> ();
		initialCd = cd;
		numberCdText.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timerAcc && activated) {
			cd--;
			numberCdText.text = cd.ToString ();
			timerAcc = Time.time + 1;
		}
		if (cd == 0) {
			activated = false;
			numberCdText.text = cd.ToString ();
			cd = initialCd;

		}
	}

	/*void OnMouseDown() {
		if (cd == initialCd) {
			if (id == AtkButtonID.Axe)
				amelia.Attack (AttackMode.AxeDown, 0.75f);
			else if(id == AtkButtonID.Spear)
				amelia.Attack (AttackMode.SpearDown, 0.66f);
			else if (id == AtkButtonID.Sword)
				amelia.Attack (AttackMode.SwordUp, 1);
			activated = true;
		}
	}*/

	public void OnTouch(bool range = false) {
		if (cd == initialCd) {
			if (id == AtkButtonID.Axe)
				amelia.Attack (AttackMode.Axe, 0.75f);
			else if (id == AtkButtonID.Sword)
				amelia.Attack (AttackMode.Sword, 1);
			else if (id == AtkButtonID.Spear) {
				if (range)
					amelia.Attack (AttackMode.ThrowSpear, 0.5f);
				else
					amelia.Attack (AttackMode.Spear, 0.66f);
			}
			activated = true;
		}
	}
}
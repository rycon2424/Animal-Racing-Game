﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRaycast : MonoBehaviour {

	RacePlayer wolfScript;
	
	Ray myRay1;
	RaycastHit hit;
	public float speed;

	bool startTimer = true;
	bool lookat = false;
	public GameObject Enemy;
	public GameObject Wolf;

	void Start () 
	{
		wolfScript = Wolf.gameObject.GetComponent<RacePlayer>();
	}

	void Update ()
	{
		
		transform.Rotate (new Vector3 (0, speed, 0));

		if (startTimer == true && lookat == false)
			{
				StartCoroutine(Timer());
				startTimer = false;
			}

		if (lookat == true) 
		{
			transform.LookAt(Enemy.transform.position);
			transform.Rotate(new Vector3(-3.2f, 0, 0), Space.Self);
		}
			
			myRay1 = new Ray (transform.position, transform.forward);
			Debug.DrawRay (transform.position, transform.forward * 30f);
			if (Physics.Raycast(myRay1, out hit, 30f))
			{
				if (hit.collider.tag == "Player")
				{
					Enemy = hit.collider.gameObject;
					lookat = true;
					wolfScript.gainSpeedPerSecond = 0.05f;
					wolfScript.maxSpeed = 0.26f;
				}
			}
	}

	IEnumerator Timer()
	{
		
	yield return new WaitForSecondsRealtime(2);
	speed = speed * -1;
	startTimer = true;

	}

}
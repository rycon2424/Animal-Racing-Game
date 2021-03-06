﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RacePlayer : NetworkBehaviour {

	[Header("SpeedTopics")]
	public float rotationSpeed;				//the speed how quick the animal can make turns
	public float speedDecrease;				//the speed the animal loses every second
	public float brakeSpeed;				//the speed of how quick the animal brakes
	public float maxSpeed;					//max speed the animal can reach
	public float gainSpeedPerSecond;		//speed that the animal gains every second

	public float hiddenMaxSpeed;			//save point for the max speed

	[Header("INGAMESPEED")]
	public float speed;						//the speed ingame

	[Header("WhenAnimations")]
	public float switchWalkRun;				//the variable when the animation switches to run
	bool gainSpeed = true;

	[Header("CharacterState")]				//status effects for every effect implemented in the game
	public bool slowed = false;
	bool slowPlayer = true;
	public bool fearPlayer = true;
	public bool stunned;
	bool canBe180ed = true;

	public GameObject stunIndicator;		//indicator above the animal that displays if the animal is stunned

	public Animator anim;

	void Start ()
	{
		if (isLocalPlayer) {
			this.transform.GetChild (0).gameObject.GetComponent<Camera> ().enabled = true;
		} else {
			this.transform.GetChild (0).gameObject.GetComponent<Camera> ().enabled = false;
		}
		anim = GetComponent<Animator> ();
		hiddenMaxSpeed = maxSpeed;
		stunIndicator.SetActive (false);
	}

	void Update ()
	{
		#region movement

		transform.Translate (Vector3.forward * speed);

		if (slowed == true && slowPlayer == true)
		{
			maxSpeed = maxSpeed * 0.5f;
			slowPlayer = false;
		}

		if (stunned == true)
		{
			speed = 0;
		}

		if (Input.GetKey(KeyCode.W) && gainSpeed == true)
		{
			speed = speed + gainSpeedPerSecond;
			StartCoroutine(Gain());
			gainSpeed = false;
		}
		else 
		{
			speed = speed - speedDecrease;
		}

		if (Input.GetKey(KeyCode.S))
		{
			speed = speed - brakeSpeed;
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate (0 ,-rotationSpeed ,0);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate (0 ,rotationSpeed ,0);
		}

		#endregion   //All the movement

		#region speedcap

		if (speed > maxSpeed)
		{
			speed = maxSpeed;
		}

		if (speed < 0.00001f)
		{
			speed = 0;
		}


        #endregion   //The maxSpeed the animal can reach

		#region animation

		if (speed == 0)
		{
			anim.SetInteger ("State", 0);	
		}

		if (speed > 0.001 && speed < switchWalkRun)
		{
			anim.SetInteger ("State", 1);
		}
		else if (speed > switchWalkRun)
		{
			anim.SetInteger ("State", 2);
		}

		#endregion  //When what animation plays and at what speed it goes to running
	}

	#region speedGain

	IEnumerator Gain()
	{
		yield return new WaitForSeconds (0.3f);
		speed = speed - speedDecrease;
		gainSpeed = true;
	}

	#endregion	  //The speed the animal gains every second

	#region Triggers


	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.tag == "Stun")
		{
			stunned = true;
			StartCoroutine(StunDuration());
		}

		if (col.gameObject.tag == "wolfSword" && canBe180ed == true)
		{
			canBe180ed = false;
			StartCoroutine(WolfSword());
			transform.Rotate (0,180,0);
		}

	}

	void OnTriggerStay(Collider col)
	{

		if (col.gameObject.tag == "Slow")
		{
			slowed = true;
		}

		if (col.gameObject.tag == "Fear" && fearPlayer == true) 
		{
			StartCoroutine(Feared());
			fearPlayer = false;
		}
	
	}

	void OnTriggerExit(Collider col)
	{

		if (col.gameObject.tag == "Slow" )
		{
			slowed = false;
			slowPlayer = true;
			maxSpeed = hiddenMaxSpeed;
		}

	}

	#endregion	   //Trigger Colliders

	#region Enums

	IEnumerator Feared()
	{
		maxSpeed = maxSpeed * 0.3f;
		yield return new WaitForSeconds (2f);
		maxSpeed = hiddenMaxSpeed;
	}

	IEnumerator WolfSword()
	{
		yield return new WaitForSeconds (2f);
		canBe180ed = true;
	}

	IEnumerator StunDuration()
	{
		stunIndicator.SetActive (true);
		yield return new WaitForSeconds (1.3f);
		stunned = false;
		stunIndicator.SetActive (false);
	}

	#endregion		 //Every time related event
}
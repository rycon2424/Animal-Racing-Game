using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer : MonoBehaviour {

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

	public GameObject stunIndicator;		//indicator above the animal that displays if the animal is stunned

	Animator anim;

	void Start ()
	{
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

		#endregion

		#region speedcap

		if (speed > maxSpeed)
		{
			speed = maxSpeed;
		}

		if (speed < 0.00001f)
		{
			speed = 0;
		}


        #endregion

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

		#endregion
	}

	#region speedGain

	IEnumerator Gain()
	{
		yield return new WaitForSeconds (0.3f);
		speed = speed - speedDecrease;
		gainSpeed = true;
	}

	#endregion

	#region Triggers


	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.tag == "Stun")
		{
			stunned = true;
			StartCoroutine(StunDuration());
		}

		if (col.gameObject.tag == "wolfSword")
		{
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

	#endregion

	#region Enums

	IEnumerator Feared()
	{
		maxSpeed = maxSpeed * 0.3f;
		yield return new WaitForSeconds (2f);
		maxSpeed = hiddenMaxSpeed;
	}

	IEnumerator StunDuration()
	{
		stunIndicator.SetActive (true);
		yield return new WaitForSeconds (2f);
		stunned = false;
		stunIndicator.SetActive (false);
	}

	#endregion
}
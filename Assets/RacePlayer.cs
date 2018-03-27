using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePlayer : MonoBehaviour {

	[Header("SpeedTopics")]
	public float rotationSpeed;
	public float speedDecrease;
	public float brakeSpeed;
	public float maxSpeed;
	public float gainSpeedPerSecond;

	public float hiddenMaxSpeed;

	[Header("INGAMESPEED")]
	public float speed;

	[Header("WhenAnimations")]
	public float switchWalkRun;
	bool gainSpeed = true;

	[Header("CharacterState")]
	public bool slowed = false;
	bool slowPlayer = true;
	public bool fearPlayer = true;
	public bool stunned;

	public GameObject stunIndicator;

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
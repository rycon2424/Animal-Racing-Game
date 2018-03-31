using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAb : MonoBehaviour {

	RacePlayer slowResist;

	[Header("Q ABILITY")]
	public float qCooldown;
	public bool qUse = true;
	public Transform fearBall;
	public Transform bearMouth;
	public float currentSpeed;

	[Header("E ABILITY")]
	public bool slowResistant = false;
	public float eCooldown;
	public float eDuration;
	public bool eUse = true;
	bool slowImune = false;

	void Start () 
	{
		slowResist = this.gameObject.GetComponent<RacePlayer>();
	}

	void Update () 
	{

		if (Input.GetKey(KeyCode.Q) && qUse == true)
		{
			StartCoroutine(Qcooldown());
			qUse = false;
		}

		if (Input.GetKey(KeyCode.E) && eUse == true)
		{
			currentSpeed = slowResist.speed;
			StartCoroutine(Ecooldown());
			eUse = false;
			slowImune = true;
		}

		if (slowResist.slowed == true && eUse == false && slowImune == true)
		{
			slowResist.speed = currentSpeed;
			slowResist.slowed = false;
		}

		if (slowResistant == true)
		{
			slowResist.maxSpeed = slowResist.hiddenMaxSpeed;	
		}
	}

	IEnumerator Qcooldown()
	{
		Instantiate (fearBall, bearMouth.transform.position, bearMouth.transform.rotation);
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

	IEnumerator Ecooldown()
	{
		slowResistant = true;
		yield return new WaitForSeconds (eDuration);
		slowImune = false;
		slowResistant = false;
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}
}
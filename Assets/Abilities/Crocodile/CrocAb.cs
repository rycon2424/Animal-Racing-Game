using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocAb : MonoBehaviour {

	RacePlayer CrocScript;

	[Header("Q ABILITY")]
	public bool qUse = true;
	public float qCooldown;
	public float qDuration;
	public GameObject Bite;

	[Header("E ABILITY")]
	public bool eUse = true;
	public float eCooldown;
	public float eDuration;

	void Start () 
	{
		Bite.SetActive(false);
		CrocScript = this.gameObject.GetComponent<RacePlayer>();	
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
			StartCoroutine(Ecooldown());
			eUse = false;
		}
	}

	IEnumerator Qcooldown()
	{
		Bite.SetActive(true);
		yield return new WaitForSeconds (qDuration);
		Bite.SetActive(false);
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

	IEnumerator Ecooldown()
	{
		CrocScript.anim.enabled = false;
		CrocScript.maxSpeed = 0.30f;
		CrocScript.rotationSpeed = 0.8f;
		CrocScript.speed = CrocScript.speed + 0.1f;
		yield return new WaitForSeconds (eDuration);
		CrocScript.anim.enabled = true;
		CrocScript.rotationSpeed = 2.2f;
		CrocScript.maxSpeed = CrocScript.hiddenMaxSpeed;
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}

}

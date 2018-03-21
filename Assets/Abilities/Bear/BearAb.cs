using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAb : MonoBehaviour {

	RacePlayer slowResist;

	[Header("Q ABILITY")]
	public float qCooldown;
	public bool qUse = true;

	[Header("E ABILITY")]
	public bool slowResistant = false;
	public float eCooldown;
	public float eDuration;
	public bool eUse = true;

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
			StartCoroutine(Ecooldown());
			eUse = false;
		}

		if (slowResistant == true)
		{
			slowResist.maxSpeed = slowResist.hiddenMaxSpeed;	
		}
	}

	IEnumerator Qcooldown()
	{
		yield return new WaitForSeconds (qCooldown);
		eUse = true;
	}

	IEnumerator Ecooldown()
	{

		slowResistant = true;
		yield return new WaitForSeconds (eDuration);
		slowResistant = false;
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}
}

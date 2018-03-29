using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAb : MonoBehaviour {

	RacePlayer wolfScript;

	[Header("Q ABILITY")]
	public bool qUse = true;
	public float qCooldown;
	public float qDuration;
	public GameObject sword;

	void Start () 
	{
		wolfScript = this.gameObject.GetComponent<RacePlayer>();
		sword.SetActive (false);
	}

	void Update () 
	{
		if (Input.GetKey(KeyCode.Q) && qUse == true)
		{
			StartCoroutine(Qcooldown());
			qUse = false;
		}
	}

	IEnumerator Qcooldown()
	{
		sword.SetActive (true);
		yield return new WaitForSeconds (qDuration);
		sword.SetActive (false);
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

}

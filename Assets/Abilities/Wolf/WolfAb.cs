using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAb : MonoBehaviour {

	[Header("Q ABILITY")]
	public bool qUse = true;
	public float qCooldown;
	public float qDuration;
	public GameObject sword;

	[Header("E ABILITY")]
	public bool eUse = true;
	public float eCooldown;
	public float eDuration;

	void Start () 
	{
		sword.SetActive (false);
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
			//StartCoroutine(Ecooldown());
			eUse = false;
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

	/*IEnumerator Ecooldown()
	{

	}*/

}

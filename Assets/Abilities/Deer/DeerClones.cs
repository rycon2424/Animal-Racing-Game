using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerClones : MonoBehaviour {

	public float duration;
	float speed = 0.5f;
	GameObject Deer;


	void Start () 
	{
		Deer = GameObject.Find("Deer");
		StartCoroutine(Qcooldown());
		this.gameObject.transform.parent = Deer.transform;
	}

	void Update () 
	{
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	IEnumerator Qcooldown()
	{
		yield return new WaitForSeconds (duration);
		Destroy (this.gameObject);
	}
}

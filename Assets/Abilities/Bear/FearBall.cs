using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearBall : MonoBehaviour {

	GameObject Bear;

	void Start () 
	{
		Bear = GameObject.Find("Bear");
		StartCoroutine (Destroy ());
		this.gameObject.transform.parent = Bear.transform;
	}

	void Update () 
	{
		transform.localScale += new Vector3 (20, 20, 20) * Time.deltaTime;
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds (0.35f);
		Destroy (this.gameObject);
	}
}

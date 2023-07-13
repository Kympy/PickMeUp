using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	private GameObject item;
	private void Awake()
	{
		item = Resources.Load<GameObject>("Item");
	}
	private float timer = 0f;
	private GameObject obj;
	public float force = 1f;
	public float radius = 1f;
	private void Update()
	{
		//this.transform.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * Time.deltaTime * 10f;
		timer += Time.deltaTime;
		if (timer > 0.1f)
		{
			timer = 0f;
			obj = Instantiate(item, this.transform.position, Quaternion.identity);
			obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			obj.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
		}
	}
}

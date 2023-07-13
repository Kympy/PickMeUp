using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
	private GameObject item;
	private void Awake()
	{
		item = Resources.Load<GameObject>("Item");
	}
	private float timer = 0f;
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > 0.2f)
		{
			Instantiate(item, new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10f)), Quaternion.Euler(Random.Range(-360, 360f), Random.Range(-360, 360f), Random.Range(-360f, 360f))).GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			timer = 0f;
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Item(Clone)")
		{
			Destroy(collision.gameObject, 3f);
		}
	}
}

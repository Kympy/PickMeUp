using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	[SerializeField] private GameObject item;
	[SerializeField] private Transform spawnPivot;
	public int randomCount;
	private void Start()
	{
		if (item == null) item = Resources.Load<GameObject>("Item");
		GameObject obj;
		for(int i = 0; i < randomCount; i++)
		{
			obj = Instantiate(item, spawnPivot.position + new Vector3(Random.Range(-4f, 4f), Random.Range(0f, 2f), Random.Range(-4f, 4f)), Quaternion.identity);
			obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		}
	}
}

using Unity.VisualScripting;
using UnityEngine;

public class JointTest : MonoBehaviour
{
	private GameObject item;
	private void Awake()
	{
		item = Resources.Load<GameObject>("Item");
	}
	private void Start()
	{
		Rigidbody[] bodies = new Rigidbody[5];
		var startPos = Vector3.zero;
		for(int i = 0; i < 5; i++)
		{
			var obj = Instantiate(item, startPos + new Vector3(0f, -i, 0f), Quaternion.identity);
			obj.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			bodies[i] = obj.GetComponent<Rigidbody>();
			if (i == 0)
			{
				bodies[0].useGravity = false;
				bodies[0].isKinematic = true;
				head = bodies[0].transform;
				//head.AddComponent<TrailRenderer>();
				//Camera.main.transform.SetParent(head.transform);
				continue;
			}
			obj.AddComponent<CharacterJoint>().connectedBody = bodies[i - 1];
		}

		ren = this.AddComponent<LineRenderer>();
	}
	private LineRenderer ren;
	private Transform head;
	private float timer = 0f;
	private int index = 0;
	private void Update()
	{
		timer += Time.deltaTime;
		head.transform.position = new Vector3(timer, Mathf.Sin(timer), 0f);
		if (ren == null) return;
		ren.positionCount++;
		ren.SetColors(Color.red, Color.blue);
		ren.SetPosition(index, new Vector3(timer, Mathf.Sin(timer), 0f));
		index++;
	}
}
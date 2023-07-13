using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Transform body;
	[SerializeField] private Transform sphere;
	[SerializeField] private Rigidbody[] finger;
	[SerializeField] private float speed;
	public float force = 0.4f;
	public float sensitivity = 0.5f;
	public bool hold = false;
	public bool locked = false;
	private float originBodyY = 0f;
	private void Awake()
	{
		originBodyY = body.transform.position.y;
	}
	private RaycastHit hit;
	private void Update()
	{
		Debug.DrawRay(sphere.transform.position, Vector3.down * sensitivity, Color.blue);
		if (Input.GetKeyDown(KeyCode.H))
		{
			StartCoroutine(FingerNarrow());
		}
		else if (Input.GetKeyDown(KeyCode.J))
		{
			StartCoroutine(FingerWide());
		}
		Physics.Raycast(transform.position, Vector3.down, out hit, 50f);
		if (hold == true)
		{
			finger[0].AddRelativeTorque(new Vector3(0f, 0f, force));
			finger[1].AddRelativeTorque(new Vector3(0f, 0f, force));
			finger[2].AddRelativeTorque(new Vector3(0f, 0f, force));
			finger[3].AddRelativeTorque(new Vector3(0f, 0f, force));
		}
		else
		{
			finger[0].AddRelativeTorque(new Vector3(0f, 0f, -force));
			finger[1].AddRelativeTorque(new Vector3(0f, 0f, -force));
			finger[2].AddRelativeTorque(new Vector3(0f, 0f, -force));
			finger[3].AddRelativeTorque(new Vector3(0f, 0f, -force));
		}
		if (locked == true) return;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(Down());
		}
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		bool isMove = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f);
		if (isMove == false) return;
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		transform.position += speed * Time.deltaTime * direction;
	}
	public float downSpeed;
	public float downDeep;
	public WaitForSeconds pauseTime = new WaitForSeconds(2f);
	private IEnumerator Down()
	{
		locked = true;
		float amount = 0f;
		//StartCoroutine(FingerWide());
		while (true)
		{
			if (Physics.Raycast(sphere.transform.position, Vector3.down, out RaycastHit hits, sensitivity))
			{
				Debug.Log($"Hit item : {hits.transform.gameObject.name}");
				hold = true;
				yield return pauseTime;
				StartCoroutine(Up());
				yield break;
			}
			var delta = (downSpeed * Time.deltaTime * Vector3.up);
			body.transform.position -= delta;
			amount += Mathf.Abs(delta.y);
			if (amount >= downDeep)
			{
				//StartCoroutine(FingerNarrow());
				hold = true;
				yield return pauseTime;
				StartCoroutine(Up());
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator Up()
	{
		while (true)
		{
			body.transform.position += downSpeed * Time.deltaTime * Vector3.up;
			if (body.transform.position.y > originBodyY)
			{
				body.transform.position = new Vector3(body.transform.position.x, originBodyY, body.transform.position.z);
				if (Physics.CheckBox(sphere.transform.position + new Vector3(0f, -sensitivity, 0f), Vector3.one * sensitivity))
				{
					//Debug.Log($"Finall item : {hits.transform.gameObject.name}");
					StartCoroutine(GoTarget());
				}
				else
				{
					Debug.Log($"Finall item : No Item");
					hold = false;
					locked = false;
				}
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator FingerWide()
	{
		float timer = 0f;
		while (true)
		{
			finger[0].AddRelativeTorque(new Vector3(0f, 0f, -0.3f));
			finger[1].AddRelativeTorque(new Vector3(0f, 0f, -0.3f));
			finger[2].AddRelativeTorque(new Vector3(0f, 0f, -0.3f));
			finger[3].AddRelativeTorque(new Vector3(0f, 0f, -0.3f));
			//finger[0].transform.Rotate(new Vector3(0f, 0f, -0.2f));
			//finger[1].transform.Rotate(new Vector3(0f, 0f, -0.2f));
			//finger[2].transform.Rotate(new Vector3(0f, 0f, -0.2f));
			//finger[3].transform.Rotate(new Vector3(0f, 0f, -0.2f));

			timer += Time.deltaTime;
			if (timer > 2f)
			{
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator FingerNarrow()
	{
		float timer = 0f;
		while (true)
		{
			finger[0].AddRelativeTorque(new Vector3(0f, 0f, 0.3f));
			finger[1].AddRelativeTorque(new Vector3(0f, 0f, 0.3f));
			finger[2].AddRelativeTorque(new Vector3(0f, 0f, 0.3f));
			finger[3].AddRelativeTorque(new Vector3(0f, 0f, 0.3f));
			//finger[0].transform.Rotate(new Vector3(0f, 0f, 0.2f));
			//finger[1].transform.Rotate(new Vector3(0f, 0f, 0.2f));
			//finger[2].transform.Rotate(new Vector3(0f, 0f, 0.2f));
			//finger[3].transform.Rotate(new Vector3(0f, 0f, 0.2f));

			timer += Time.deltaTime;
			if (timer > 2f)
			{
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator GoTarget()
	{
		Vector3 distance = transform.position - target.transform.position;
		while (true)
		{
			transform.position -= speed * Time.deltaTime * new Vector3(distance.x, 0f, 0f).normalized;
			if (transform.position.x <= target.transform.position.x)
			{
				transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
				break;
			}
			yield return null;
		}
		while (true)
		{
			transform.position -= speed * Time.deltaTime * new Vector3(0f, 0f, distance.z).normalized;
			if (transform.position.z <= target.transform.position.z)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
				break;
			}
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		hold = false;
		locked = false;
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, hit.point);
		Gizmos.DrawSphere(hit.point, 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawCube(sphere.transform.position + new Vector3(0f, -sensitivity, 0f), Vector3.one * sensitivity);
	}
}

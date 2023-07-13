using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] private Transform body;
	[SerializeField] private Transform[] finger;
	[SerializeField] private float speed;

	public bool isDown = false;
	private Coroutine downCoroutine = null;
	private float originBodyY = 0f;
	private void Awake()
	{
		originBodyY = body.transform.position.y;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isDown == true) return;
			downCoroutine = StartCoroutine(Down());
		}
		if (isDown == true) return;
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
		isDown = true;
		float amount = 0f;
		StartCoroutine(FingerWide());
		while(true)
		{
			var delta = (downSpeed * Time.deltaTime * Vector3.up);
			body.transform.position -= delta;
			amount += Mathf.Abs(delta.y);
			if (amount >= downDeep)
			{
				StartCoroutine(FingerNarrow());
				yield return pauseTime;
				downCoroutine = StartCoroutine(Up());
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator Up()
	{
		while(true)
		{
			body.transform.position += downSpeed * Time.deltaTime * Vector3.up;
			if (body.transform.position.y > originBodyY)
			{
				body.transform.position = new Vector3(body.transform.position.x, originBodyY, body.transform.position.z);
				downCoroutine = null;
				isDown = false;
				yield break;
			}
			yield return null;
		}
	}
	private IEnumerator FingerWide()
	{
		float timer = 0f;
		while(true)
		{
			finger[0].transform.Rotate(new Vector3(0f, 0f, -0.1f));
			finger[1].transform.Rotate(new Vector3(0f, 0f, -0.1f));
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
			finger[0].transform.Rotate(new Vector3(0f, 0f, 0.1f));
			finger[1].transform.Rotate(new Vector3(0f, 0f, 0.1f));
			timer += Time.deltaTime;
			if (timer > 2f)
			{
				yield break;
			}
			yield return null;
		}
	}
}

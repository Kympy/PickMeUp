using UnityEngine;

public class Target : MonoBehaviour
{
	private Generator generator;
	private void Awake()
	{
		generator = FindObjectOfType<Generator>();	
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Item(Clone)")
		{
			other.gameObject.name = "Got";
			generator.AddScore();
		}
	}
}

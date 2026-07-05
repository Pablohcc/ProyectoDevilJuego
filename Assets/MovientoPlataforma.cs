using Unity.VisualScripting;
using UnityEngine;

public class MovientoPlataforma : MonoBehaviour
{
	public Vector3 PlayerVector;
	public GameObject PlayerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		PlayerTransform = GameObject.FindGameObjectWithTag("Player");
		PlayerVector = PlayerTransform.transform.localScale;
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			
			other.transform.SetParent(transform);
			//other.transform.localScale = PlayerVector;
			



		}
	}


	public void OnTriggerExit(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			other.transform.SetParent(null);
		}
	}



	/*
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.SetParent(transform);
		}
	}

	public void OnCollisionExit(Collision collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.SetParent(null);
		}
	}
	*/
}

using UnityEngine;

public class HumoRoca : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.CompareTag("RocaVolcanica"))
        {
            Destroy(gameObject, 2f);
        }
	}

}

using UnityEngine;

public class CamaraExplosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
	}


	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("DronTerrestre"))
		{
			CineMachibeMovimientoCamara.Instance.MoverCamara(30,30,1);
		}
	}
}

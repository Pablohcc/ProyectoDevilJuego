using UnityEngine;

public class TriggerJefeFinal : MonoBehaviour
{
    public GameObject Spawn1;
    public GameObject Spawn2;
   // public GameObject Mecha;
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
		if (other.CompareTag("Player"))
		{
            Spawn1.SetActive(true);
			Spawn2.SetActive(true);
           // Mecha.SetActive(true);
		}
	}
}

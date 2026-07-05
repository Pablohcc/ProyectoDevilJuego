using UnityEngine;
using UnityEngine.Rendering;

public class StatsEnemy : MonoBehaviour
{
	

	public Transform PointerBala;
	public Transform jugador;
	public GameObject Bala;
	public float VidaEnemigo = 100;
	public float VidaMaximaEnemigo;
	public GameObject[] Recompensas;
	public float SpeedObjeto;
	public Transform PointerObjeto;


	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//DispararBala();
	}

	public void InstanciaBala()
	{
		Instantiate(Bala, PointerBala.transform.position, PointerBala.transform.rotation);
		//InstanciarRecompensa();


	}

	public void InstanciarRecompensa()
	{
		//if (VidaEnemigo <= 0)
		//{
			if (Recompensas.Length > 0)
			{
				int indice = Random.Range(0, Recompensas.Length);
				GameObject CopiaObjeto = Instantiate(Recompensas[indice], PointerObjeto.position, PointerObjeto.rotation);
				CopiaObjeto.GetComponent<Rigidbody>().AddForce(PointerObjeto.up * SpeedObjeto, ForceMode.Impulse);
		}
			//Destroy(gameObject, 5f);
		//}
	}

	

	public void TakeDamage(int daño)
	{
		VidaEnemigo -= daño;

		
		/*if(VidaEnemigo <= 0f)
		{
			InstanciarRecompensa();
		}
		*/
	}


	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			TakeDamage(100);
		
		}
	}


	


	/*
	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			VidaEnemigo = 0;

			//InstanciarRecompensa();
		}
	}

	*/


	/*
	void DispararBala()
	{
		// 1. Instanciar la bala en la posición y rotación del punto de disparo
		GameObject bala = Instantiate(Bala, PointerBala.position, PointerBala.rotation);

		// 2. Calcular la dirección hacia el jugador
		Vector3 direccion = (jugador.position - PointerBala.position).normalized;

		// 3. Obtener el componente Rigidbody de la bala y empujarla
		Rigidbody rbBala = bala.GetComponent<Rigidbody>();
		if (rbBala != null)
		{
			rbBala.linearVelocity = direccion * 15f; // Cambia 15f por la velocidad que necesites
		}
	}
	*/
}


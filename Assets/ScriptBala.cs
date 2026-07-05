using System;
using UnityEngine;

public class ScriptBala : MonoBehaviour
{
	public float velocidad = 10f;


	//public GameObject balaPrefab; // Arrastra aquí tu prefab de la bala
	public Transform puntoDeDisparo; // El lugar exacto de donde sale la bala
	public Transform jugador;

	//public float Velocidad;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		jugador = GameObject.FindGameObjectWithTag("PlayerImpactoBala").transform;
		puntoDeDisparo = GameObject.FindGameObjectWithTag("Pointerbala").transform;
		//transform.Find("PointerBala");
		//GameObject.FindGameObjectWithTag("Pointerbala").transform;
		/*BalaTransform = GameObject.FindGameObjectWithTag("Player");
		transform.rotation = BalaTransform.transform.rotation;
		transform.position = BalaTransform.transform.position;
		*/
		Destroy(gameObject, 2f);

	}

	
	// Update is called once per frame
	void Update()
	{

		if (puntoDeDisparo == null)
		{
			Destroy(gameObject);
		} else {
			DispararBala();
		}
		

		/*
		//transform.Translate(MovimientoBala);
		//transform.Translate(MovimientoBala*Time.deltaTime);
		transform.position = transform.position * velocidad;
		*/

	}

	public void OnCollisionEnter(Collision collision)
	{

		if (collision.transform.CompareTag("Player"))
		{
			Destroy(gameObject);
		}

		if (collision.transform.CompareTag("Suelo"))
		{
			Destroy(gameObject);
		}

		
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}




	public void DispararBala()
	{

		// 2. Calcular la dirección hacia el jugador
		Vector3 direccion = (jugador.position - puntoDeDisparo.position).normalized;

		// 3. Obtener el componente Rigidbody de la bala y empujarla
		Rigidbody rbBala = transform.GetComponent<Rigidbody>();
		if (rbBala != null)
		{
			rbBala.linearVelocity = direccion * velocidad * Time.deltaTime; 
		
		}
	}

}

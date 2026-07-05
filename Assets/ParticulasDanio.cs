using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ParticulasDanio : MonoBehaviour
{
	public ParticleSystem plasmaSystem;
	public GameObject player;
	public Transform PlayerTansform;

	public float Cronometro;

	public float RangoDisparo = 300;

	public GameObject Suelo;

	private List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		plasmaSystem = GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    void Update()
    {
		float distancia1 = Vector3.Distance(transform.position, PlayerTansform.position);

		if (distancia1 <= RangoDisparo)
		{
			ContadorParticulas();
		}

		
    }

	private void OnParticleCollision(GameObject other)
	{
		

	}

	private void OnParticleTrigger()
	{
		player = GameObject.FindWithTag("Player");

		VidaJugador.Instance.TakeDamage(0.1f);
		Debug.Log("Le di al Player" + VidaJugador.vidaActual);

		

	}

	


	public void ContadorParticulas ()
	{
		Cronometro -= Time.deltaTime;

		if (Cronometro<=0f)
		{
			plasmaSystem.Play();
			Cronometro = 3f;
		}
	}

	private void OnDrawGizmosSelected()
	{
		// Radio de persecución en Rojo
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, RangoDisparo);

	}


}

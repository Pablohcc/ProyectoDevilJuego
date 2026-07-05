using UnityEngine;

public class RotacionPointerBala : MonoBehaviour
{
	public GameObject prefabBala;   // El prefab de tu bala
	public Transform puntoDisparo;   // Un objeto vacío donde nace la bala
	public Transform jugador;        // Referencia al Transform del Player
	public float fuerzaBala = 20f;
	public bool debeAplicarFuerza;

	private void Start()
	{
		jugador = GameObject.FindGameObjectWithTag("PlayerImpactoBala").transform;
	}

	
	void DispararAlJugador()
	{
		if (jugador == null) return;

		// 1. Calcular la dirección hacia el jugador
		Vector3 direccion = (jugador.position - puntoDisparo.position).normalized;

		// 2. Instanciar la bala
		GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);

		// 3. Obtener el Rigidbody y aplicarle fuerza en esa dirección
		Rigidbody rb = bala.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.linearVelocity = direccion * fuerzaBala; // En versiones antiguas de Unity usa: rb.velocity
		}
	}

	public void ActivarEventoFisico()
	{
		debeAplicarFuerza = true; // Activa el flag
		Debug.Log("Evento de animación recibido, listo para FixedUpdate");
	}
}
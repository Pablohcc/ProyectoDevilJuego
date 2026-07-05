using UnityEngine;

public class SpawnDronTerrestre : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	[Header("Configuración del Spawner")]
	[Tooltip("El Prefab que deseas instanciar.")]
	public GameObject prefabAInstanciar;

	[Tooltip("La cantidad máxima 'X' de objetos permitidos.")]
	public int limiteMaximo = 10;

	[Tooltip("Tiempo en segundos entre cada aparición.")]
	public float tiempoEntreSpawns = 2f;

	// Nuestro contador interno
	private int cantidadActual = 0;

	void Start()
	{
		// Esto iniciará un temporizador que llama al método "InstanciarObjeto" repetidamente
		InvokeRepeating(nameof(InstanciarObjeto), 0f, tiempoEntreSpawns);
	}

	// Este es el método central de la lógica
	public void InstanciarObjeto()
	{
		// 1. Verificamos si aún NO hemos llegado al límite
		if (cantidadActual < limiteMaximo)
		{
			// 2. Instanciamos el objeto en la posición de este Spawner
			Instantiate(prefabAInstanciar, transform.position, Quaternion.identity);

			// 3. Sumamos 1 al contador
			cantidadActual++;

			Debug.Log("Objeto creado. Total: " + cantidadActual + " de " + limiteMaximo);
		}
		else
		{
			// 4. Lo que sucede cuando llegamos al tope
			Debug.Log("Límite alcanzado. Dejando de instanciar.");

			// Como ya no necesitamos crear más, detenemos la repetición para ahorrar memoria
			CancelInvoke(nameof(InstanciarObjeto));
		}
	}


	
}
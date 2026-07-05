using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class SpawDronTerrestreTiempo : MonoBehaviour
{
    
	[Header("Configuración del Spawner")]
	[Tooltip("El Prefab que deseas instanciar.")]
	public GameObject prefabAInstanciar;
	public GameObject Mecha;

	[Tooltip("Cada cuántos segundos aparece un objeto.")]
	public float tiempoEntreSpawns = 2f;

	[Tooltip("Tiempo total del cronómetro antes de detenerse (en segundos).")]
	public float tiempoTotalCronometro = 180f;

	// Variables internas para controlar el tiempo
	private float tiempoRestante;
	private float temporizadorSpawn;

	void Start()
	{
		//Mecha = GameObject.FindGameObjectWithTag("Mecha");
		Mecha.SetActive(false);
		
		

		// Al iniciar, nuestro cronómetro arranca con el tiempo máximo establecido
		tiempoRestante = tiempoTotalCronometro;

		// El temporizador de aparición inicia en 0
		temporizadorSpawn = 0f;
	}

	void Update()
	{
		// 1. Si el cronómetro ya llegó a 0 (o menos), salimos del Update y no hacemos nada más.
		if (tiempoRestante <= 0f)
		{
			Mecha.SetActive(true);
			return;
		}

		// 2. Reducimos el tiempo del cronómetro en cada frame
		// Time.deltaTime es el tiempo que pasó desde el último frame (ej. 0.016 segundos)
		tiempoRestante -= Time.deltaTime;

		// 3. Aumentamos nuestro temporizador de instanciación
		temporizadorSpawn += Time.deltaTime;

		// 4. Verificamos si nuestro temporizador alcanzó o superó los 2 segundos
		if (temporizadorSpawn >= tiempoEntreSpawns)
		{
			// Instanciamos el objeto
			Instantiate(prefabAInstanciar, transform.position, Quaternion.identity);

			Debug.Log("Objeto creado. Tiempo restante en el cronómetro: " + Mathf.Round(tiempoRestante) + "s");

			// Reiniciamos el temporizador de spawn (le restamos los 2 segundos para ser exactos)
			temporizadorSpawn -= tiempoEntreSpawns;
		}

		// 5. Un aviso visual en la consola justo en el momento en que se acaba el tiempo
		if (tiempoRestante <= 0f)
		{
			Debug.Log("¡Cronómetro llegó a 0! El Spawner se ha detenido.");
		}
	}

	
}
using UnityEngine;

public class ParticulaDistancia : MonoBehaviour
{
	public ParticleSystem ps;
	public ParticleSystem.MainModule main;

	[Header("Valores Actuales")]
	public float currentMin = 0.4f;
	public float currentMax = 0.6f;

	[Header("Configuración de Cambio")]
	public float changeRate = 0.1f; // Cuánto cambia por segundo

	[Header("Límites Máximos")]
	public float targetMaxLimitMin = 0.4f; // Límite máximo para el valor Min
	public float targetMaxLimitMax = 0.6f; // Límite máximo para el valor Max

	[Header("Player")]
	public Transform player;

	void Start()
	{
		ps = GetComponent<ParticleSystem>();
		main = ps.main;

		ApplyChangesToParticles();
	}

	void Update()
	{
		MirarAlJugador();
	}

	/// <summary>
	/// Reduce los valores de tiempo de vida hasta llegar a 0.
	/// </summary>
	public void ReduceLifetime()
	{
		currentMin -= changeRate * Time.deltaTime;
		currentMax -= changeRate * Time.deltaTime;

		// Mathf.Max asegura que el valor nunca sea menor que 0
		currentMin = Mathf.Max(0f, currentMin);
		currentMax = Mathf.Max(0f, currentMax);

		

		ApplyChangesToParticles();
	}

	/// <summary>
	/// Aumenta los valores de tiempo de vida hasta el límite configurado.
	/// </summary>
	public void IncreaseLifetime()
	{
		currentMin += changeRate * Time.deltaTime;
		currentMax += changeRate * Time.deltaTime;

		// Mathf.Min asegura que el valor nunca supere el límite que estableciste
		currentMin = Mathf.Min(currentMin, targetMaxLimitMin);
		currentMax = Mathf.Min(currentMax, targetMaxLimitMax);

		if (currentMin >= 0.4)
		{
			currentMin = 0.4f;
		}

		if (currentMax>= 0.6f)
		{
			currentMax = 0.6f;
		}

		ApplyChangesToParticles();
	}

	/// <summary>
	/// Método auxiliar para reasignar la curva y no repetir código.
	/// </summary>
	private void ApplyChangesToParticles()
	{
		main.startLifetime = new ParticleSystem.MinMaxCurve(currentMin, currentMax);
	}

	void MirarAlJugador()
	{
		Vector3 direccion = (player.position - transform.position).normalized;
		//direccion.y = 0; // Bloqueamos el eje Y para que no se incline hacia el piso/cielo

		if (direccion != Vector3.zero)
		{
			Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 2f);
		}
	}
}
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMecha : MonoBehaviour
{
	[Header("Referencias")]
	public Transform player;
	private NavMeshAgent agent;
	private Animator anim;
	public float VidaMecha = 100;
	public bool ActivarMecha;

	[Header("Zonas de Distancia")]
	public float distanciaMelee = 2.5f;       // Zona 4: Ataca cuerpo a cuerpo (Roar 0)
	public float distanciaDisparoQuieto = 6f; // Zona 3: Dispara quieto (Attack6_Shoot 0)
	public float distanciaDisparoMov = 12f;   // Zona 2: Avanza y dispara (Walk_F_Shoot 0)
	public float distanciaPersecucion = 20f;  // Zona 1: Solo persigue (Walk_F 0)
											  // Más de 20f = Zona 0: Idle

	[Header("Particulas")]
	public ParticulaDistancia miSistemaDeParticulas;
	public Transform ParticulasTransform;

	void Start()
	{
		miSistemaDeParticulas = GetComponentInChildren<ParticulaDistancia>();

		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();

		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player").transform;
			
	}

	void Update()
	{
		
			if (player == null) return;

			float distancia = Vector3.Distance(transform.position, player.position);


			// Evaluamos las distancias de menor a mayor
			if (distancia <= distanciaMelee)
			{
				// ESTADO 4: ROAR (Cuerpo a cuerpo)
				DetenerAgente(true);
				MirarAlJugador();
				anim.SetInteger("EstadoIA", 4);
				miSistemaDeParticulas.ReduceLifetime();
				//anim.SetInteger("EstadoIA", 5);
			}
			else if (distancia <= distanciaDisparoQuieto)
			{
				// ESTADO 3: DISPARO ESTÁTICO
				DetenerAgente(true);
				MirarAlJugador();
				anim.SetInteger("EstadoIA", 3);
				miSistemaDeParticulas.IncreaseLifetime();
			}
			else if (distancia <= distanciaDisparoMov)
			{
				// ESTADO 2: CAMINAR DISPARANDO
				DetenerAgente(false); // Retoma el movimiento
				agent.SetDestination(player.position);
				anim.SetInteger("EstadoIA", 2);
				miSistemaDeParticulas.IncreaseLifetime();

			}
			else if (distancia <= distanciaPersecucion)
			{
				// ESTADO 1: SOLO CAMINAR (Persiguiendo)
				DetenerAgente(false); // Retoma el movimiento
				agent.SetDestination(player.position);
				anim.SetInteger("EstadoIA", 1);
				miSistemaDeParticulas.ReduceLifetime();

			}
			else
			{
				// ESTADO 0: IDLE (Fuera de rango)
				DetenerAgente(true);
				anim.SetInteger("EstadoIA", 0);
				miSistemaDeParticulas.ReduceLifetime();
			}

		
	}

	// Función para manejar el NavMeshAgent correctamente
	void DetenerAgente(bool detener)
	{
		if (detener)
		{
			agent.isStopped = true;
			agent.velocity = Vector3.zero; // Frena al instante, evita que resbale
		}
		else
		{
			agent.isStopped = false;
		}
	}

	// Función vital para que no dispare o golpee al aire cuando está quieto
	void MirarAlJugador()
	{
		Vector3 direccion = (player.position - transform.position).normalized;
		direccion.y = 0; // Bloqueamos el eje Y para que no se incline hacia el piso/cielo

		if (direccion != Vector3.zero)
		{
			Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 8f);
		}
	}

	


}
using UnityEngine;
using UnityEngine.AI;

public class SoldadoEnemy : MonoBehaviour
{
    [Header("Deteccion")]
    public float RadioMira = 30f;
    public float RadioPerder = 40f;

    [Header("Movimiento")]
    public float velocidadPersecucion = 8f;

    [Header("Disparo")]
    public GameObject bala;
    public Transform PointerBala;
    public float speedBala;
    public float Tiempo = 2.5f;
    public float RadioDisparo = 10f;

    [Header("Animacion")]
    public Animator AnimatorEnemy;

    private Transform PointerPlayer; // ya no es public
    private NavMeshAgent Agent;      // ya no es public
    private float _tiempoRestante = 0f;
    private bool persiguiendo = false;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        AnimatorEnemy = GetComponentInChildren<Animator>();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            PointerPlayer = obj.transform;
        else
            Debug.LogWarning("[SoldadoEnemy] No se encontró objeto con tag 'Player'");
    }

    void Update()
    {
        if (PointerPlayer == null) return;
        _tiempoRestante -= Time.deltaTime;
        MovimientoSoldado();
    }

    void MovimientoSoldado()
    {
        float distancia = Vector3.Distance(PointerPlayer.position, transform.position);

        // --- Rango de disparo ---
        if (distancia <= RadioDisparo)
        {
            Agent.speed = 0f;
            Agent.SetDestination(transform.position);
            AnimatorEnemy.SetBool("Disparar", true);
            InstanciarBala();
            persiguiendo = true;
            FaceTarget(PointerPlayer.position);
            return;
        }

        AnimatorEnemy.SetBool("Disparar", false);

        // --- Detectar jugador ---
        if (distancia <= RadioMira && !persiguiendo)
            persiguiendo = true;

        // --- Perder jugador ---
        if (persiguiendo && distancia >= RadioPerder)
        {
            persiguiendo = false;
            Agent.speed = 0f;
            Agent.SetDestination(transform.position); // Se queda quieto
            return;
        }

        // --- Perseguir ---
        if (persiguiendo)
        {
            Agent.speed = velocidadPersecucion;
            Agent.SetDestination(PointerPlayer.position);
            FaceTarget(PointerPlayer.position);
        }
    }

    void InstanciarBala()
    {
        if (_tiempoRestante <= 0f)
        {
            GameObject copiaBala = Instantiate(bala, PointerBala.position, PointerBala.rotation);
            BalaEnemigo scriptBala = copiaBala.GetComponent<BalaEnemigo>();
            scriptBala.IniciarDireccion(PointerPlayer.position);
            _tiempoRestante = Tiempo;
        }
    }

    void FaceTarget(Vector3 objetivo)
    {
        Vector3 direction = (objetivo - transform.position).normalized;
        if (direction == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 3f * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioMira);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RadioDisparo);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RadioPerder);
    }
}
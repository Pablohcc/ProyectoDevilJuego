// EnemyNavMesh.cs
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [Header("Deteccion")]
    public float RadioMira;
    public Transform PointerPlayer;
    public NavMeshAgent Agent;
    public float RadioDisparo;
    public Animator AnimatorEnemy;

    [Header("Patrullaje")]
    public float VistaA;
    public float VistaB;
    public Transform PuntoA;
    public Transform PuntoB;
    public float RadioMov;
    public bool ActivarA;

    [Header("Balas")]
    public GameObject bala;
    public Transform PointerBala;
    public float speedBala;
    public float TiempoRestante;
    public float Tiempo = 2.5f;
    

    [Header("Persecución")]
    public bool persiguiendo = false;
    public float RadioPerder;

    void Start()
    {
        AnimatorEnemy = GetComponentInChildren<Animator>();

        if (PuntoA == null || PuntoB == null)
        {
            Debug.LogError("Faltan asignar PuntoA o PuntoB en EnemyNavMesh");
            return;
        }

        Agent.SetDestination(PuntoA.position);
    }

    public void InstanciarBala()
    {
        if (TiempoRestante <= 0)
        {
            // Cuenta cuántas balas hay activas en escena ahora mismo
            int balasActivas = GameObject.FindGameObjectsWithTag("BalaEnemigo").Length;

           
            {
                GameObject copiaBala = Instantiate(bala, PointerBala.position, PointerBala.rotation);
                BalaEnemigo scriptBala = copiaBala.GetComponent<BalaEnemigo>();
                scriptBala.IniciarDireccion(PointerPlayer.position);
            }

            
            TiempoRestante = Tiempo;
        }
    }

    void Update()
    {
        if (PointerPlayer == null) return;
        TiempoRestante -= Time.deltaTime;
        MovimientoEnemigo();
    }

    

    void MovimientoEnemigo()
    {
        float distanciaJugador = Vector3.Distance(PointerPlayer.position, transform.position);

        if (distanciaJugador <= RadioDisparo)
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

        if (distanciaJugador <= RadioMira && !persiguiendo)
            persiguiendo = true;

        if (persiguiendo)
        {
            if (distanciaJugador >= RadioPerder)
            {
                persiguiendo = false;
                Agent.speed = 3.5f;
                Agent.SetDestination(PuntoA.position);
                ActivarA = true;
                return;
            }

            Agent.speed = 8f;
            Agent.SetDestination(PointerPlayer.position);
            FaceTarget(PointerPlayer.position);
            return;
        }

        Agent.speed = 3.5f;

        float DistanciaA = Vector3.Distance(PuntoA.position, transform.position);
        float DistanciaB = Vector3.Distance(PuntoB.position, transform.position);

        if (ActivarA && DistanciaA <= VistaA)
        {
            Agent.SetDestination(PuntoB.position);
            ActivarA = false;
        }
        else if (!ActivarA && DistanciaB <= VistaB)
        {
            Agent.SetDestination(PuntoA.position);
            ActivarA = true;
        }

        if (Agent.velocity.magnitude > 0.1f)
            FaceTarget(transform.position + Agent.velocity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioMira);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RadioDisparo);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RadioPerder);
    }

    void FaceTarget(Vector3 objetivo)
    {
        Vector3 direction = (objetivo - transform.position).normalized;
        if (direction == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 3f * Time.deltaTime);
    }
}
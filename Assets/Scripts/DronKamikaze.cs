using UnityEngine;

public class DronKamikaze : MonoBehaviour
{
    [Header("Deteccion")]
    public Transform jugador;
    public float radioDeteccion = 15f;
    public float radioPerdida = 25f;       // Si el jugador huye más lejos, el dron para

    [Header("Movimiento")]
    public float velocidadAtaque = 8f;
    public float velocidadMaxima = 8f;     // Velocidad tope, no se pasa de aquí
    public float aceleracion = 2f;         // Qué tan rápido llega a velocidadMaxima
    public float alturaFlotacion = 0.4f;
    public float velocidadFlotacion = 2f;

    [Header("Explosion")]
    public GameObject OndaExpansiva;
    public GameObject prefabExplosion;
    public float tiempoDestruccionExplosion = 5f;

    private float alturaInicial;
    private float velocidadActual = 0f;    // Velocidad actual interpolada
    public bool persiguiendo = false;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
		alturaInicial = transform.position.y;
        if (jugador == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null) jugador = obj.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);

        // Activar persecución
        if (distancia <= radioDeteccion)
            persiguiendo = true;

        // Desactivar si el jugador escapó
        if (distancia > radioPerdida)
        {
            persiguiendo = false;
            velocidadActual = 0f; // Resetear velocidad al perder al jugador
        }

        if (persiguiendo)
            Perseguir();
        else
            Flotar();
    }

    void Flotar()
    {
        float nuevaY = alturaInicial + Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }

    void Perseguir()
    {
        Vector3 destino = new Vector3(
            jugador.position.x,
            jugador.position.y + 1f,
            jugador.position.z
        );

        // Acelerar gradualmente hasta velocidadMaxima
        velocidadActual = Mathf.MoveTowards(velocidadActual, velocidadMaxima, aceleracion * Time.deltaTime);

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidadActual * Time.deltaTime
        );

        Vector3 direccion = (destino - transform.position).normalized;
        if (direccion != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 8f * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (prefabExplosion != null)
            {
                Instantiate(OndaExpansiva, transform.position, Quaternion.identity);
                GameObject explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
                Destroy(explosion, tiempoDestruccionExplosion);
            }
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioPerdida);
    }
}
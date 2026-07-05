
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad = 15f;
    private Vector3 direccion;

    public void IniciarDireccion(Vector3 objetivo)
    {
        direccion = (objetivo - transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BalaEnemigo") && !other.CompareTag("Enemigo"))
            Destroy(gameObject);
    }
}
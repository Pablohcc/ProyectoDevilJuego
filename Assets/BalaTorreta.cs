using UnityEngine;

public class BalaTorreta : MonoBehaviour
{
    public float velocidad = 30f;
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
        if (other.CompareTag("Player"))
            Destroy(gameObject);
        if (!other.CompareTag("BalaTorreta") && !other.CompareTag("Enemigo") && !other.CompareTag("PlayerHead"))
            Destroy(gameObject);
    }
}
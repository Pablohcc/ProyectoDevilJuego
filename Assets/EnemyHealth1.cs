using UnityEngine;

public class EnemyHealth1 : MonoBehaviour
{
    public int vida;
    public float VidaMaximaEnemigo = 100;
    void Update()
    {
        
    }
    public void TakeDamage(int daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "GolpePlayer")
        {
            TakeDamage(25);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject enemyPrefab;
    public int maxEnemies = 3;
    public float spawnInterval = 4f;
    public float spawnRadiusOffset = 1f;

    [Header("Radio de activación")]
    public Transform jugador;
    public float radioActivacion = 20f;

    private float _timer = 0f;
    private List<GameObject> _misEnemies = new List<GameObject>(); 

    void Update()
    {
        if (jugador == null) return;
        if (Vector3.Distance(transform.position, jugador.position) > radioActivacion) return;

        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            TrySpawnDron();
        }
    }

    void TrySpawnDron()
    {
        
        _misEnemies.RemoveAll(d => d == null);

        if (_misEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 offset = new Vector3(
            Random.Range(-spawnRadiusOffset, spawnRadiusOffset),
            0f,
            Random.Range(-spawnRadiusOffset, spawnRadiusOffset)
        );

        GameObject nuevoEnemy = Instantiate(enemyPrefab, transform.position + offset, transform.rotation);
        _misEnemies.Add(nuevoEnemy);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioActivacion);
    }
}
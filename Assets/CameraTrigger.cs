using UnityEngine;
using Unity.Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [Header("Al ENTRAR al trigger")]
    public CinemachineCamera[] camarasActivarAlEntrar;
    public CinemachineCamera[] camarasApagarAlEntrar;

    [Header("Al SALIR del trigger")]
    public CinemachineCamera[] camarasActivarAlSalir;
    public CinemachineCamera[] camarasApagarAlSalir;

    private void Start()
    {
        foreach (CinemachineCamera cam in camarasActivarAlEntrar)
            cam.Priority = 0;

        foreach (CinemachineCamera cam in camarasActivarAlSalir)
            cam.Priority = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (CinemachineCamera cam in camarasActivarAlEntrar)
            cam.Priority = 20;

        foreach (CinemachineCamera cam in camarasApagarAlEntrar)
            cam.Priority = 0;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (CinemachineCamera cam in camarasActivarAlSalir)
            cam.Priority = 20;

        foreach (CinemachineCamera cam in camarasApagarAlSalir)
            cam.Priority = 0;
    }
}
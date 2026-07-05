using UnityEngine;

public class EventosAnimacionKira : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Arma;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarArma()
    {
        Arma.SetActive(true);
    }

    public void DescativarArma()
    {
        Arma.SetActive (false);
    }
}

using UnityEngine;
using UnityEngine.Events;


public class EventosPlayer : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    public UnityEvent ColliderEnter;
    public UnityEvent ColliderExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            TriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            TriggerExit.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            ColliderEnter.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            ColliderExit.Invoke();
        }
    }
}


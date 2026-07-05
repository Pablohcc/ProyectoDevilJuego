using UnityEngine;

public class EvitarColisionRocas : MonoBehaviour
{
	public Rigidbody body;
	private CharacterController controller;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;

		if (!hit.gameObject.CompareTag("RocaVolcanica")) return;

		/*
		// Si chocamos con el objeto en movimiento...
		if (body != null && !body.isKinematic)
		{
			// Opción 1: Hacemos que ignore por completo el empuje del jugador
			// simplemente no aplicando fuerzas y saliendo del método.
			return;
		}
		*/
	}
}

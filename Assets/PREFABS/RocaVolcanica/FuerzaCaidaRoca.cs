using UnityEngine;

public class FuerzaCaidaRoca : MonoBehaviour
{
	Rigidbody rb;
	public float fuerzaCaida = 10f;
	public float Contador;
	public GameObject Humo;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		// Aplica fuerza constante hacia abajo
		rb.AddForce(Vector3.down * fuerzaCaida, ForceMode.Acceleration);
	}

	private void Update()
	{
		/*Contador -= Time.deltaTime;

		if(Contador <=0)
		{
			Destroy(gameObject);
			Contador = UnityEngine.Random.Range(2, 4);
		}

		*/
	}

	public void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.CompareTag("Suelo")){

			InstanciaHumo();
			
			Destroy(gameObject, 5f);
		}

		if (collision.transform.CompareTag("Player"))
		{
			rb.isKinematic = true;
			VidaJugador.Instance.TakeDamage(10);
		}
	}

	public void OnCollisionExit(Collision collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			rb.isKinematic = false;
		}
	}

	public void InstanciaHumo(){
		Instantiate(Humo, transform.position, transform.rotation);
		
	}

}
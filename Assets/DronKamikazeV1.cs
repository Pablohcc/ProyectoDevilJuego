using UnityEngine;

public class DronKamikazeV1 : MonoBehaviour
{
    [Header("Deteccion")]
    public Transform jugador;
    public float radioDeteccion = 15f;
	public float radioPerdida = 25f;

	[Header("Movimiento")]
	public float velocidadAtaque = 8f;
	public float velocidadMaxima = 8f;     // Velocidad tope, no se pasa de aquí
	public float aceleracion = 2f;         // Qué tan rápido llega a velocidadMaxima
	public float alturaFlotacion = 0.4f;
	public float velocidadFlotacion = 2f;

	[Header("Daño")]
    public int danioAlContacto = 15;

    [Header("Explosion")]
    public Transform TransformPlayer;
    public GameObject ExplosionPartiicula;

    private float _alturaInicial;
    [SerializeField] private bool _persiguiendo = false;

    void Start()
    {   
        TransformPlayer = GameObject.FindGameObjectWithTag("PlayerImpactoBala").transform;
		jugador = GameObject.FindGameObjectWithTag("Player").transform;
        
		_alturaInicial = transform.position.y;

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

        if (distancia <= radioDeteccion)
        {
            _persiguiendo = true;
        }

        // Flotar y Perseguir son mutuamente excluyentes
        if (_persiguiendo)
        {
            Perseguir();
        }
        else
        {
            Flotar();
        }
    }

 



    public void ExplosionDron()
    {
		Instantiate(ExplosionPartiicula, TransformPlayer.transform.position, TransformPlayer.transform.rotation);
		CineMachibeMovimientoCamara.Instance.MoverCamara(20, 20, 1);
	}

    public void ExplosionDronSuperficie()
    { 
        Instantiate(ExplosionPartiicula, transform.position, transform.rotation);
        
    }





	void Flotar()
    {
        float nuevaY = _alturaInicial + Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }

    void Perseguir()
    {
        Vector3 destino = new Vector3(
            jugador.position.x,
            jugador.position.y + 1f,
            jugador.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidadAtaque * Time.deltaTime
        );

        Vector3 direccion = (destino - transform.position).normalized;
        if (direccion != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 8f * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
           /* VidaJugador vida = otro.GetComponent<VidaJugador>();
            if (vida != null)
            {
                vida.RecibirDanio(danioAlContacto);
            } */
            Destroy(gameObject);
        }


		if (otro.transform.tag == "Player")
		{
			ExplosionDron();
		}

		if (otro.transform.tag == "Player")
		{
			Destroy(gameObject);
		}

		if (otro.transform.tag== "Suelo")
		{
            ExplosionDronSuperficie();
		}

		if (otro.transform.tag=="Suelo")
		{
            Destroy(gameObject);
		}

	}
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
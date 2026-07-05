using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour
{
    public static VidaJugador Instance;

    [Header("VIDA")]

    public static float vidaActual = 30f;
    public float vidaMaxima = 30;

    [Header("PUNTOS")]
    public static int puntos;


    [Header("TIEMPO")]
    public float cronometro = 3;

    [Header("ITEMS")]
    public bool Llave;

    [Header("UI")]
    public GameObject PanelGameOver;


    [Header("ANIMACIONES")]
    public Animator animator;

    public void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Cronometro();

        if (vidaActual >= 50 && vidaActual <= 99 && cronometro <= -0.09f)
        {
            RecuperacionVida();
        }
        else
        {
            VidaMaximaObtenida();
        }

    }


    public void VidaUp(float valor)
    {

        vidaActual += valor;
        Debug.Log("Corazón usado");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BalaTorreta")
        {
            vidaActual -= 5f;
            Destroy(other.transform.gameObject);
            if (vidaActual <= 0)
            {
                animator.SetBool("IsDed", true);
                PanelGameOver.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (other.transform.tag == "BalaEnemigo")
        {

            vidaActual -= 0.1f;
			Debug.Log(vidaActual);
			Destroy(other.transform.gameObject);
            if (vidaActual <= 0)
            {
                animator.SetBool("IsDed", true);
                PanelGameOver.SetActive(true);
                Time.timeScale = 0;

            }
        }



        if (other.transform.tag == "ExplosionEnemigo")
        {

            vidaActual -= 1f;
            Destroy(other.transform.gameObject);
            if (vidaActual <= 0)
            {
                animator.SetBool("IsDed", true);
                PanelGameOver.SetActive(true);
                Time.timeScale = 0;

            }
        }

        if (other.transform.tag == "MUERTE")
        {
            PanelGameOver.SetActive(true);
            animator.SetBool("IsDedFall", true);
            Time.timeScale = 0;

        }


		if (other.transform.tag == "TRAMPAS")
		{
			vidaActual = vidaActual - 15;
		}

		if (other.transform.tag == "Lava")
		{
			vidaActual = 0;
			//Destroy (gameObject);
		}

		if (other.transform.CompareTag("BalaEnemigo"))
		{
			//RecibirDanio(1);
			TakeDamage(1);
            Debug.Log(vidaActual);
			//Destroy(other.transform.gameObject);
		}

	}
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "COIN")
        {
            puntos = puntos + 1;
            Destroy(collision.transform.gameObject);
        }

		if (collision.transform.tag == "Lava")
		{
			vidaActual = 0;
			Destroy(gameObject);
		}

		if (collision.transform.tag == "Muerte")
		{
			vidaActual = vidaActual - 2000;
			if (vidaActual == 0)
			{
				Morir();
			}
		}


	}
    public void Cronometro()
    {
        cronometro = cronometro - Time.deltaTime;

        if (cronometro <= -0.1f)
        {
            cronometro = 3f;
        }
    }

    public void RecuperacionVida()
    {

        vidaActual = vidaActual + 1;

    }


    ///Esta Funcion es para que cuando la vida llega a 100, no se pase de alli
    public void VidaMaximaObtenida()
    {
        if (vidaActual >= vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }

    }

	public void RecibirDanio(float cantidad)
	{
		vidaActual -= cantidad;
		vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

		Debug.Log("Vida del jugador: " + vidaActual + "/" + vidaMaxima);

		if (vidaActual <= 0) Morir();
	}

	public void Curar(float cantidad)
	{
		vidaActual += cantidad;
		vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
	}

	void Morir()
	{
		Debug.Log("Game Over");
		// UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
	}

	public void TakeDamage(float daño)
	{
		vidaActual -= daño;

		if (vidaActual <= 0f)
		{
			Destroy(gameObject);
		}
	}

}

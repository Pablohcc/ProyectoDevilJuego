
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control_UI : MonoBehaviour
{
    public Image BarraVida;
    public float VidaMaxima = 30f;
    public GameObject PanelPausa;

    public Text textoPuntos;


    void Update()
    {
        
        if (BarraVida != null)
        {
            BarraVida.fillAmount = VidaJugador.vidaActual / VidaMaxima;
        }

        textoPuntos.text = "" + VidaJugador.puntos;
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void CargarEscena()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Reanudar()
    {
        PanelPausa.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pausa()
    {
        PanelPausa.SetActive(true);
        Time.timeScale = 0;
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ManagerUIInicio : MonoBehaviour
{


    public GameObject PanelAjustes;
    public GameObject PanelInventario;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CargarEscena()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void SalirAjustes()
    {
        PanelAjustes.SetActive(false);
    }


    public void Ajustes()
    {
        PanelAjustes.SetActive(true);
    }

    public void SalirInventario()
    {
        PanelInventario.SetActive(false);
    }
    public void Inventario()
    {
        PanelInventario.SetActive(true);
    }
}

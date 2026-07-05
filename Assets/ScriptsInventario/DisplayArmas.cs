using UnityEngine;
using UnityEngine.UI;

public class DisplayArmas : MonoBehaviour
{
    public Armas Espada1;
    public Armas Espada2;
    public Armas Espada3;
    public Armas Espada4;

    public Text TextoNombre;
    public Text TextoDescripcion;
    public Text TextoPoder;
    public Image IconoArma;

    public void ActivarEspada1()
    {
        TextoNombre.text = Espada1.NombreArma;
        TextoDescripcion.text = Espada1.DescripcionArma;
        TextoPoder.text = "Poder: " + Espada1.Poder;
        IconoArma.sprite = Espada1.IconoArma;
    }

    public void ActivarEspada2()
    {
        TextoNombre.text = Espada2.NombreArma;
        TextoDescripcion.text = Espada2.DescripcionArma;
        TextoPoder.text = "Poder: " + Espada2.Poder;
        IconoArma.sprite = Espada2.IconoArma;
    }

    public void ActivarEspada3()
    {
        TextoNombre.text = Espada3.NombreArma;
        TextoDescripcion.text = Espada3.DescripcionArma;
        TextoPoder.text = "Poder: " + Espada2.Poder;
        IconoArma.sprite = Espada3.IconoArma;
    }

    public void ActivarEspada4()
    {
        TextoNombre.text = Espada4.NombreArma;
        TextoDescripcion.text = Espada4.DescripcionArma;
        TextoPoder.text = "Poder: " + Espada4.Poder;
        IconoArma.sprite = Espada4.IconoArma;
    }
}

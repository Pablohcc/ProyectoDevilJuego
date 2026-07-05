using UnityEngine;

[CreateAssetMenu(fileName = "New Arma", menuName = "Arma")]
public class Armas : ScriptableObject
{
    public string NombreArma;
    public string DescripcionArma;
    public string Poder;
    public Sprite IconoArma;
    public GameObject ModeloArma;
}

using Unity.Burst.Intrinsics;
using UnityEngine;

public class EventosAnimation : MonoBehaviour
{
    public AudioClip ClipAttack;
    public AudioClip ClipWalk;
    public AudioClip ClipSaltar;
    public GameObject Arma;

    public AudioSource source;
    public void ActivarAtaque()
    {
        source.PlayOneShot(ClipAttack);
    }

    public void ActivarWalk()
    {
        source.PlayOneShot(ClipWalk);
    }

    public void ActivarSaltar()
    {
        source.PlayOneShot(ClipSaltar);
    }

	public void ActivarArma()
	{
		Arma.SetActive(true);
	}

	public void DescativarArma()
	{
		Arma.SetActive(false);
	}
}

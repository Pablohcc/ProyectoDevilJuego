using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {

        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("IsAttacking1", true);
        }
        else
        {
            animator.SetBool("IsAttacking1", false);
        }
    }
}

using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator não encontrado no dummy!");
    }

    // Função chamada quando recebe dano
    public void TakeHit()
    {
        // Apenas dispara a animação de hit
        animator.SetTrigger("HitTrigger");
        Debug.Log("Dummy recebeu hit!");
    }
}
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Classe básica para movimentação do player no jogo
/// </summary>
[RequireComponent(typeof(Rigidbody2D))] //Adiciona automaticamente o componente se não tiver
[RequireComponent(typeof(Animator))] //Adiciona automaticamente o componente se não tiver
public abstract class CharacterTopDown : MonoBehaviour {
    
    [Header("Atributos")]
    public int HP;
    public float speed;

    public bool destroyOnDead = true;

    protected bool canMove = true;
    protected bool canBeHitted = true;
    protected Rigidbody2D rb;
    protected Animator animator;
    
    
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        animator = GetComponent<Animator>();
    }

    public virtual void Hit(int damage, Vector3 enemyPosition) {
        if (!canBeHitted || HP == 0) return;
        //CAUSA DANO
        HP -= damage;
        if (HP < 0)
            HP = 0;
        //EMPURA
        var push = (transform.position - enemyPosition).normalized; //Empura na direção oposta do inimigo
        push.z = 0;
        
        rb.AddForce(push * 20f);


        //Animação
        animator.SetInteger("HP", HP);
        animator.SetTrigger("Hit");
        
        if (HP == 0) { //Se acabou a vida desabilita o script do player
            if (destroyOnDead)
                Destroy(gameObject, 3f);

            rb.bodyType = RigidbodyType2D.Static; //Não pode se mvoer
            enabled = false;
            return ;
        }
        StartCoroutine(CanBeHitted(1.5f));
        StartCoroutine(CanMove(1.5f));
    }

    //Limita o movimento do personagens por alguns segunso
    protected IEnumerator CanMove(float delay) {
        //Bloquea o movimento
        canMove = false; 
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("Walking", false);

        //Libera
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    //Torna o personagem invencível por alguns segundos
    protected IEnumerator CanBeHitted(float delay) {
        canBeHitted = false;   
        yield return new WaitForSeconds(delay);
        canBeHitted = true;

    }
    
}

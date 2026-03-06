using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// Classe básica para movimentação do player no jogo
/// </summary>
[RequireComponent(typeof(Rigidbody2D))] //Adiciona automaticamente o componente se não tiver
[RequireComponent(typeof(Animator))] //Adiciona automaticamente o componente se não tiver
public class PlayerTopDown : MonoBehaviour, IDamage {
    
    [Header("Atributos")]
    public int HP;
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInputs inputs;
    private Canvas canvas;

    private bool canMove = true;


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        animator = GetComponent<Animator>();
        inputs = new PlayerInputs();
        canvas = GetComponentInChildren<Canvas>();
    }

    void OnEnable() => inputs.Enable();
    void OnDisable() => inputs.Disable();

    /// <summary>
    /// Função usada para elementos que envolve fisica, mantendo sempre a constancia da execução
    /// </summary>
    void FixedUpdate() {
        Move();        
    }

    void Update() {
        Attack();
    }


    //Ataca
    private void Attack() {
        if (inputs.Player.Attack.WasPressedThisFrame()) {
            animator.SetTrigger("Attack");    
            StartCoroutine(CanMove(0.7f));
        }
    }

    //Sofre dano
    public void Hit( int damage, Vector3 enemyPosition) {

        //CAUSA DANO
        HP -= damage;
        if (HP < 0)
            HP = 0;
        //EMPURA
        var push = (transform.position - enemyPosition).normalized; //Empura na direção oposta do inimigo
        push.z = 0;
        rb.AddForce(push *30f);

        //Animação
        animator.SetInteger("HP", HP);
        animator.SetTrigger("Hit");
        
        if (HP == 0) //Se acabou a vida desabilita o script do player
            enabled = false;

        StartCoroutine(CanMove(1f));

    }

    //Movimenta o personagem
    private void Move() {
        if (!canMove) return;

        //Movimento
        var direction = inputs.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = direction * speed;
        //animação
        animator.SetBool("Walking", direction != Vector2.zero);
        //Giro
        if (direction.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
            if (canvas.transform.localScale.x < 0) //Evita espelha a barra
                canvas.transform.localScale = new Vector3(-1 * canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
        else if (direction.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
            if (canvas.transform.localScale.x > 0) //Evita espelha a barra
                canvas.transform.localScale = new Vector3(-1 * canvas.transform.localScale.x, canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
    }

    IEnumerator CanMove(float delay) {
        //Bloquea o movimento
        canMove = false; 
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("Walking", false);

        //Libera
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

     IEnumerator CanBeHitted(float delay) {
        
    }
}

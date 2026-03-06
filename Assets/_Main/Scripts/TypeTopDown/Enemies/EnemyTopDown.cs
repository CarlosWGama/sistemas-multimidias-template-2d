using System.Collections;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyTopDown : MonoBehaviour, IDamage {

    [Header("Atributos")]
    public float speed = 2f;
    public int HP = 2;
    
    [Header("Debug")]

    public GameObject target;
    private Rigidbody2D rb;
    private Animator animator;

    private bool canMove = true;


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        animator = GetComponent<Animator>();
    }

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("UpdateTarget", 0f, 3000f); //A cada 3 segundos repete a função.
    }


    void Update() {
        
    }

    void FixedUpdate() {
        Move();
    
    }

    //Busca o alvo mais próximo
    private void UpdateTarget() {
            var targets = GameObject.FindGameObjectsWithTag("Player");
            
            float distanceTarget = Vector3.Distance(transform.position, target.transform.position);
            
            //Verifica se tem algum alvo mais próximo
            foreach (GameObject newTarget in targets) {
                float distance = Vector3.Distance(transform.position, newTarget.transform.position);
                if (distance < distanceTarget) {
                    target = newTarget;
                    distanceTarget = distance;
                }
            }
    }

    //Movimenta o inimigo
    private void Move() {
        if (!canMove) return;   


        var direction = (target.transform.position - transform.position).normalized;
        direction.z = 0;

        rb.linearVelocity = direction * speed;
        animator.SetBool("Walking", direction != Vector3.zero);

        //Girar
        if (target.transform.position.x > transform.position.x) transform.localScale = Vector3.one;
        else if (target.transform.position.x < transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            animator.SetTrigger("Attack");
            StartCoroutine(CanMove(2f));
        }
    }

    //Sofre dano
    public void Hit(int damage, Vector3 playerPosition) {
        //CAUSA DANO
        HP -= damage;
        if (HP < 0)
            HP = 0;
        //EMPURA
        var push = (transform.position - playerPosition).normalized; //Empura na direção oposta do inimigo
        push.z = 0;
        
        rb.AddForce(push * 20f);


        //Animação
        animator.SetInteger("HP", HP);
        animator.SetTrigger("Hit");
        
        if (HP == 0) { //Se acabou a vida desabilita o script do player
            Destroy(gameObject, 3f);
            rb.bodyType = RigidbodyType2D.Static; //Não pode se mvoer
            enabled = false;
            return ;
        }
    
        StartCoroutine(CanMove(1.5f));
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
}

using System.Collections;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyTopDown : CharacterTopDown {
    
    [Header("Debug")]
    public GameObject target;
    // --------------------------------
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("UpdateTarget", 0f, 3f); //A cada 3 segundos repete a função.
    }
    // --------------------------------
    void FixedUpdate() {
        Move();    
    }
    // --------------------------------
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
    // --------------------------------
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
    // ----------------------------------
    public override bool Hit(int damage, Vector3 enemyPosition) {
        var hitted = base.Hit(damage, enemyPosition);
        if (hitted && HP == 0) {
            TopDownLevelController.Instance.WinLevel();
        }
        return hitted;
    }
    // --------------------------------
    void OnCollisionEnter2D(Collision2D collision) {
        if (HP <= 0) return;
        if (collision.gameObject.tag == "Player") {
            animator.SetTrigger("Attack");
            StartCoroutine(CanMove(2f));
        }
    }


    
}

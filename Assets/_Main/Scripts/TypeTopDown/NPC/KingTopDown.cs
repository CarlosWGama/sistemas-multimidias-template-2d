using UnityEngine;

public class KingTopDown : CharacterTopDown {
    
    private bool moving = false;

    private Vector2 direction = new Vector2(0, -1);

    private float timeWalking = 2f;
    private float timeIdle = 3f;


    void Update() {
        

        if (timeIdle > 0) { //Parado
            timeIdle -= Time.deltaTime;
            timeWalking = 2f;
        } else if (timeWalking > 0) { //Andando
            timeWalking -= Time.deltaTime;
        } else { //Parou de andar 
            timeIdle = 3f;
            timeWalking = 2f;
            direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)); //Define a próxima nova direção

            if (direction.x < 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else {
                transform.localScale = Vector3.one;
            }
        }

        moving = timeIdle <= 0;
        animator.SetBool("Walking", moving);
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        if (moving && canMove) rb.linearVelocity = direction * speed;
        else rb.linearVelocity = Vector2.zero;
    }

    public virtual void Hit(int damage, Vector3 enemyPosition)  {
        base.Hit(damage, enemyPosition);

        if (HP < 0) {
            //Game Over
        }
    }
}

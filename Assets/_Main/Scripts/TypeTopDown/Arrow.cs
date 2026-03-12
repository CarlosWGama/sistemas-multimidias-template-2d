using UnityEngine;

public class Arrow : AttackDamage {

    public float lifeTime = 2f;
    public float speed;
    private Vector2 direction = Vector2.right;
    // ------------------------------
    void Awake() {
        Destroy(gameObject, lifeTime);
    }
    // -------------------------------
    // Determina qual direção a flecha vai seguir
    public void DefineDirection(bool right) {
        if (typeLevel == TypeLevel.PLATFORM) { //Direção caso seja platform
            direction = right ? Vector2.right : Vector2.left;
        } else { //Direção caso o jogo seja TopDown
            //Busca o inimigo mais próximo
            GameObject nearestEnemy = null;
            var enemies = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (var enemy in enemies) {
                //Ignora os inimigos que estão na direção oposta do que o player está olhando
                if (right && enemy.transform.position.x < transform.position.x) continue;
                else if (!right && enemy.transform.position.x > transform.position.x) continue;

                if (nearestEnemy == null) {
                    nearestEnemy = enemy;
                } else if (Vector2.Distance(transform.position, enemy.transform.position) < Vector2.Distance(transform.position, nearestEnemy.transform.position)) {
                    nearestEnemy = enemy;
                }
            }
            //Define a direção
            if (nearestEnemy != null) {
                direction = (nearestEnemy.transform.position - transform.position).normalized;
            } else {
                direction = right ? Vector2.right : Vector2.left;
            }
        }        

        //Ajusta a direção da flecha
        if (!right) transform.localScale = new Vector3(-1, 1, 1);

    }

    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

}

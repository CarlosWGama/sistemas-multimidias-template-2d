using UnityEngine;

public class AttackDamage : MonoBehaviour {
    
    [Header("Info")]
    public int damage;
    public string targetTag = "Player";
    public bool enemyAttack = true;
    public bool destroyOnHit = false;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == targetTag) {
            collision.GetComponent<IDamage>().Hit(damage, transform.position);
        }
    }
}

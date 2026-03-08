using UnityEngine;

/// <summary>
/// Script responsável por infligir dano ou no inimigo ou no player
/// </summary>
public class AttackDamage : MonoBehaviour {
    
    [Header("Info")]
    public int damage;
    public string targetTag = "Player"; //Determina se o dano é para ser causado no Player ou no inimigo de acordo com a tag
    public bool enemyAttack = true;
    public bool destroyOnHit = false;
    // ---------------------------------------
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == targetTag) {
            collision.GetComponent<CharacterTopDown>().Hit(damage, transform.position);
        }
    }
}

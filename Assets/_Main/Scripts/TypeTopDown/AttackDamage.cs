using UnityEngine;

/// <summary>
/// Script responsável por infligir dano ou no inimigo ou no player
/// </summary>
public class AttackDamage : MonoBehaviour {
    
    [Header("Info")]
    public TypeLevel typeLevel = TypeLevel.TOP_DOWN;
    public int damage;
    public string targetTag = "Player"; //Determina se o dano é para ser causado no Player ou no inimigo de acordo com a tag
    public bool destroyOnHit = false;
    // ---------------------------------------
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == targetTag) {
            if (typeLevel == TypeLevel.TOP_DOWN)
                collision.GetComponent<CharacterTopDown>().Hit(damage, transform.position);

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}

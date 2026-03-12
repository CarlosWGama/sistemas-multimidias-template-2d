using UnityEngine;

/// <summary>
/// Classe responsável por criar novos inimigos no mapa
/// </summary>
public class RespawnTopDown : MonoBehaviour {
    
    [Header("Geração de Inimigos")]
    public GameObject[] enemies;
    
    public float delayMin = 2f;
    public float delayMax = 5f;

    private float delay;
    

    void Awake() {
        delay = Random.Range(delayMin, delayMax);        
    }

    void Update() {
        //Verica quando deve respawnar um inimigo
        delay -= Time.deltaTime;
        if (delay <= 0) {
            if (TopDownLevelController.Instance.CanSpawnEnemy()) {
                SpawnEnemy();
                delay = Random.Range(delayMin, delayMax);
            } else {
                enabled = false;
            }
        }
    }
    
    //Realiza o spawn de um inimigo aleatório        
    void SpawnEnemy() {    
        int index = Random.Range(0, enemies.Length);
        Instantiate(enemies[index], transform.position, Quaternion.identity);
    }
}

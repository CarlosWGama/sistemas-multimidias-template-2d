using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopDownLevelController : GenericLevelController {

    [Header("Informações do Player")]
    public GameObject[] prefabsCharacters;

    public int defaultCharacter = 0;

    public Vector3 initialPosition = Vector3.zero;
    // ---------------------------------------
    [Header("UI")]
    public TMP_Text textRemainingEnemies;
    public GameObject gameOverPanel;
    // ---------------------------------------
    public int maxEnemies = 100;
    private int deadEnemies = 0;
    private int spawnEnemies = 0;
    // ---------------------------------------

    void Awake() {
        SpawnPlayer();
    }
    // ---------------------------------------
    private void SpawnPlayer() {
        var player = Instantiate(prefabsCharacters[defaultCharacter], initialPosition, Quaternion.identity);
        GameObject.FindAnyObjectByType<CameraFollow>().target = player;
    }
    // ---------------------------------------
    protected override void Start() {
        base.Start();
        UpdateUI();
    }
    // ---------------------------------------
    /// <summary>
    /// Determina e contabiliza o total de inimigos gerados
    /// </summary>
    /// <returns></returns>
    public bool CanSpawnEnemy() {
        if (spawnEnemies < maxEnemies) {
            spawnEnemies++;
            UpdateUI();
            return true;
        }
        return false;
    }
    // ---------------------------------------
    /// <summary>
    /// Sempre que um inimigo for morta, aumenta a contagem. Você vence se todos forem mortos
    /// </summary>
    /// <returns></returns>
    public bool WinLevel() {
        deadEnemies++;
        UpdateUI();
        if (deadEnemies == maxEnemies) {
            return true;
        }
        return false;
    }
    // ---------------------------------------
    private void UpdateUI() {
        if (textRemainingEnemies != null) {
            int remaining = maxEnemies - deadEnemies;
            textRemainingEnemies.text = "Inimigos Restantes: " + remaining;
        }
    }
}

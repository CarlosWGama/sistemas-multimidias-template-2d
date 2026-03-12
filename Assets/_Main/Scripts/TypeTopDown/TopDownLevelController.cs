using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum LevelStatus {
    WIN, GAME_OVER, PLAYING
}

public class TopDownLevelController : GenericLevelController {

    [Header("Informações do Player")]
    public GameObject[] prefabsCharacters;
    private PlayerTopDown player;

    public int defaultCharacter = 0;

    public Vector3 initialPosition = Vector3.zero;
    // ---------------------------------------
    [Header("UI")]
    public TMP_Text textRemainingEnemies;
    public GameObject gameOverPanel;

    public string nextLevel = "_Main/Scenes/06_Ending";
    // ---------------------------------------
    public int maxEnemies = 100;
    private int deadEnemies = 0;
    private int spawnEnemies = 0;
    private LevelStatus levelStatus = LevelStatus.PLAYING;
    // ---------------------------------------
    //
    public static TopDownLevelController Instance { get; private set; }


    void Awake() {
        Instance = this;
        SpawnPlayer();
    }
    // ---------------------------------------
    private void SpawnPlayer() {
        player = Instantiate(prefabsCharacters[defaultCharacter], initialPosition, Quaternion.identity).GetComponent<PlayerTopDown>();
        FindAnyObjectByType<CameraFollow>().target = player.gameObject;
    }
    // ---------------------------------------
    protected override void Start() {
        base.Start();
        UpdateUI();
    }
    // ---------------------------------------
    /// <summary>Determina e contabiliza o total de inimigos gerados</summary>
    public bool CanSpawnEnemy() {
        if (levelStatus != LevelStatus.PLAYING) return false;

        if (spawnEnemies < maxEnemies) {
            spawnEnemies++;
            UpdateUI();
            return true;
        }
        return false;
    }
    // ---------------------------------------
    /// <summary> Sempre que um inimigo for morta, aumenta a contagem. Você vence se todos forem mortos</summary>
    public void WinLevel() {
        if (levelStatus != LevelStatus.PLAYING) return;
        deadEnemies++;
        UpdateUI();
        if (deadEnemies == maxEnemies) Win();
        
    }
    // ---------------------------------------
    /// <summary>Mantem atualizado a informação de quantos inimigos ainda restam no cenário a serem derrotados </summary>
    private void UpdateUI() {
        if (textRemainingEnemies != null) {
            int remaining = maxEnemies - deadEnemies;
            textRemainingEnemies.text = "Inimigos Restantes: " + remaining;
        }
    }
    // ---------------------------------------
    /// <summary> Recarrega a própria Scene</summary>
    public void BtnTryAgain() {
        SceneManager.LoadScene("_Main/Scenes/04.1_Level_Top_Down");
    }
    // ----------------------------------------
    /// <summary> Volta para tela principal </summary>
    public void BtnMainMenu() {
        SceneManager.LoadScene("_Main/Scenes/01_Menu");
    }
    // -----------------------------------------
    /// <summary>Jogador perdeu a fase, inimigos comemoram e panel de game over aparece </summary>
    public void GameOver() {
        gameOverPanel.SetActive(true);
        levelStatus = LevelStatus.GAME_OVER;
        //Faz animação dos inimigos comemorando!
        var enemies = FindObjectsByType<EnemyTopDown>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var enemy in enemies)
            enemy.Win();
    }
    // -----------------------------------------
    public void Win() {
        levelStatus = LevelStatus.WIN;
        //Faz animação do player comemorando e depois volta para a tela de seleção de fases ou créditos. 
        player.Win();
        //Carrega a próxima fase após 3 segundos
        StartCoroutine(NextLevel(3f)); //Carrega a 
        
    }
    // -----------------------------------------
    private IEnumerator NextLevel(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextLevel);

    } 

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class CutSceneController : GenericLevelController {


    [Header("Info")]
    public CutSceneScriptableObject[] cutscenes;
    public string nextScene;
    private int indexCurrentCutScene = 0;
    // -------------------------------------------------------
    [Header("UI")]
    public Image background;
    public TMP_Text text;
    // -------------------------------------------------------
    void Awake() {
        SetValues();
    }
    // ------------------------------------------------------
    public void BtnNextCutScene() {
        indexCurrentCutScene++;

        if (indexCurrentCutScene < cutscenes.Length) 
            SetValues();
        else 
            SceneManager.LoadScene(nextScene);
    }
    // ------------------------------------------------------
    private void SetValues() {
        background.sprite = cutscenes[indexCurrentCutScene].image;
        text.text = cutscenes[indexCurrentCutScene].Text; 
    }
}

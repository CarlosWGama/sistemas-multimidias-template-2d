using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectPlayerLevelController : MonoBehaviour {


    [Header("UI")]
    public Animator BtnAmazon;    
    public Animator BtnElf;    

    public TMP_Text textNameChar;
    public TMP_Text textBioChar;
    public Image imgConceptArt;

    [Header("Scriptable Objects")]
    public SelectPlayerScriptableObject amazonScriptable;
    public SelectPlayerScriptableObject elfScriptable;

    private TypeCharacter typeCharacter = TypeCharacter.AMAZON;

    /* FUNCIONALIDADES DOS BOTÕES DE SELEÇÃO DE PERSONAGENS  */ 
    /// <summary> Troca a animação do botão de acordo com a seleção </summary>
    public void BtnSelectAmazon() {
        BtnAmazon.SetBool("Selected", true);
        BtnElf.SetBool("Selected", false); 
        UpdateInfo(amazonScriptable);
        typeCharacter = TypeCharacter.AMAZON;
        AudioController.Instance.PlaySE(amazonScriptable.voice);
    }
    // -----------------------------------------------------
    public void BtnSelectElf() {
        BtnAmazon.SetBool("Selected", false);
        BtnElf.SetBool("Selected", true);
        UpdateInfo(elfScriptable);
        typeCharacter = TypeCharacter.ELF;
        AudioController.Instance.PlaySE(elfScriptable.voice);
    }
    // -----------------------------------------------------
    private void UpdateInfo(SelectPlayerScriptableObject selected) {
        textNameChar.text = selected.title;
        textBioChar.text = selected.bio;
        imgConceptArt.sprite = selected.ConceptArt;
    }
    // -----------------------------------------------------
    public void BtnConfirmChar() {
        PlayerPrefs.SetInt("TypeCharacter", (int)typeCharacter);
        SceneManager.LoadScene("_Main/Scenes/03_Select_Level");
    }

}

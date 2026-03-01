using System;
using TMPro;
using UnityEngine;
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

    /* FUNCIONALIDADES DOS BOTÕES DE SELEÇÃO DE PERSONAGENS  */ 
    /// <summary> Troca a animação do botão de acordo com a seleção </summary>
    public void BtnSelectAmazon() {
        BtnAmazon.SetBool("Selected", true);
        BtnElf.SetBool("Selected", false); 
        UpdateInfo(amazonScriptable);
        AudioController.Instance.PlaySE(amazonScriptable.voice);
    }

    public void BtnSelectElf() {
        BtnAmazon.SetBool("Selected", false);
        BtnElf.SetBool("Selected", true);
        UpdateInfo(elfScriptable);
        AudioController.Instance.PlaySE(elfScriptable.voice);
    }

    private void UpdateInfo(SelectPlayerScriptableObject selected) {
        textNameChar.text = selected.title;
        textBioChar.text = selected.bio;
        imgConceptArt.sprite = selected.ConceptArt;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script criado para garantir que um painel seja vísivel por vez.

public class PanelController : MonoBehaviour
{
    public GameObject painelMenu;
    //public GameObject painelOp;
    public GameObject painelNovoJogo;
    //public GameObject painelOpcoes;
    //public GameObject painelOpControll;
    //public GameObject painelMessageInfo;


    void Start()
    {
        ShowPainelMenu();
    }

    //public void ShowPainelOp()
    //{
    //    painelMenu.SetActive(false);
    //    painelNovoJogo.SetActive(false);
    //    painelOpcoes.SetActive(false);
    //    painelOpControll.SetActive(false);
    //    painelMessageInfo.SetActive(false);
    //    painelOp.SetActive(true);
    //}

    //public void ShowPainelOpControll()
    //{
    //    painelMenu.SetActive(false);
    //    painelNovoJogo.SetActive(false);
    //    painelOpcoes.SetActive(false);
    //    painelOp.SetActive(false);
    //    painelMessageInfo.SetActive(false);
    //    painelOpControll.SetActive(true);
    //}

    //public void ShowPainelMessageInfo()
    //{
    //    painelMenu.SetActive(false);
    //    painelNovoJogo.SetActive(false);
    //    painelOpcoes.SetActive(false);
    //    painelOp.SetActive(false);
    //    painelOpControll.SetActive(false);
    //    painelMessageInfo.SetActive(true);
    //}

    //public void ShowpainelOpcoes()
    //{
    //    painelMenu.SetActive(false);
    //    painelNovoJogo.SetActive(false);
    //    painelOp.SetActive(false);
    //    painelOpControll.SetActive(false);
    //    painelMessageInfo.SetActive(false);
    //    painelOpcoes.SetActive(true);
    //}

    public void ShowPainelNovoJogo()
    {
        painelMenu.SetActive(false);
        //painelOp.SetActive(false);
        //painelOpcoes.SetActive(false);
        //painelOpControll.SetActive(false);
        //painelMessageInfo.SetActive(false);
        painelNovoJogo.SetActive(true);

    }

    public void ShowPainelMenu()
    {
        //painelOp.SetActive(false);
        painelNovoJogo.SetActive(false);
        //painelOpcoes.SetActive(false);
        //painelOpControll.SetActive(false);
        //painelMessageInfo.SetActive(false);
        painelMenu.SetActive(true);
    }
}
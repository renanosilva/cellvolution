using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script criado para garantir que um painel seja vísivel por vez.

public class PanelController : MonoBehaviour
{
    public GameObject painelMenu;
    public GameObject painelOpVolume;
    public GameObject painelNovoJogo;
    public GameObject painelOpcoes;
    public GameObject painelOpControll;
    public GameObject painelMessageInfo;


    void Start()
    {
        ShowPainelMenu();
    }

    public void ShowPainelOpVolume()
    {
        painelMenu.SetActive(false);
        painelNovoJogo.SetActive(false);
        painelOpcoes.SetActive(false);
        painelOpControll.SetActive(false);
        painelMessageInfo.SetActive(false);
        painelOpVolume.SetActive(true);
    }

    public void ShowPainelOpControll()
    {
        painelMenu.SetActive(false);
        painelNovoJogo.SetActive(false);
        painelOpcoes.SetActive(false);
        painelOpVolume.SetActive(false);
        painelMessageInfo.SetActive(false);
        painelOpControll.SetActive(true);
    }

    public void ShowPainelMessageInfo()
    {
        painelMenu.SetActive(false);
        painelNovoJogo.SetActive(false);
        painelOpcoes.SetActive(false);
        painelOpVolume.SetActive(false);
        painelOpControll.SetActive(false);
        painelMessageInfo.SetActive(true);
    }

    public void ShowpainelOpcoes()
    {
        painelMenu.SetActive(false);
        painelNovoJogo.SetActive(false);
        painelOpVolume.SetActive(false);
        painelOpControll.SetActive(false);
        painelMessageInfo.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void ShowPainelNovoJogo()
    {
        painelMenu.SetActive(false);
        painelOpVolume.SetActive(false);
        painelOpcoes.SetActive(false);
        painelOpControll.SetActive(false);
        painelMessageInfo.SetActive(false);
        painelNovoJogo.SetActive(true);

    }

    public void ShowPainelMenu()
    {
        painelOpVolume.SetActive(false);
        painelNovoJogo.SetActive(false);
        painelOpcoes.SetActive(false);
        painelOpControll.SetActive(false);
        painelMessageInfo.SetActive(false);
        painelMenu.SetActive(true);
    }
}
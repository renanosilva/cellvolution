using UnityEngine;
using UnityEngine.UI;

public class TeclasMovimentação : MonoBehaviour
{
    [Header("Botões de Movimentação")]
    public Button wasdButton; // Botão para esquema WASD
    public Button arrowKeysButton; // Botão para esquema das setas

    [Header("Fechar Painel de Controle")]
    public GameObject painelMenu;
    public GameObject painelOpControll;

    void Start()
    {
        if (wasdButton != null)
        {
            wasdButton.onClick.AddListener(SetWASDControls);
        }
        if (arrowKeysButton != null)
        {
            arrowKeysButton.onClick.AddListener(SetArrowKeyControls);
        }
    }

    public void SetWASDControls()
    {
        PlayerPrefs.SetString("ControlScheme", "WASD");
        PlayerPrefs.Save();

        painelOpControll.SetActive(false);
        painelMenu.SetActive(true);
    }

    public void SetArrowKeyControls()
    {
        PlayerPrefs.SetString("ControlScheme", "ArrowKeys");
        PlayerPrefs.Save();
        
        painelOpControll.SetActive(false);
        painelMenu.SetActive(true);
    }
}

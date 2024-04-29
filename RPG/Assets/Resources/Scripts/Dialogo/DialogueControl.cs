using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speechText;
    public Text actorNameText;
    private float nameAtual = 0;
   
    public int[] qtdTurnos;
    public int turnoAtual = 0;

    public NPC NPC;
    public Char player;
    public Dialogue1 dialogue;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;
    

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void FixedUpdate(){
        if(dialogueObj.activeSelf){
            
            if(Input.GetKeyDown(KeyCode.Return)){
                NextSentence();
            }
        }
    }
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            //ainda há textos
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";

                if (nameAtual == 0)
                {
                    qtdTurnos[turnoAtual] = qtdTurnos[turnoAtual] - 1;

                    if (qtdTurnos[turnoAtual] == 0)
                    {
                        actorNameText.text = "Player";
                        nameAtual = 1;
                        turnoAtual++;
                        profile.sprite = Resources.Load<Sprite>("Sprites/celula");
                    }
                } else if (nameAtual == 1)
                {
                    qtdTurnos[turnoAtual] = qtdTurnos[turnoAtual] - 1;

                    if (qtdTurnos[turnoAtual] == 0)
                    {
                        actorNameText.text = NPC.nome;
                        nameAtual = 0;
                        turnoAtual++;
                        profile.sprite = dialogue.profile;
                    }
                }

                StartCoroutine(TypeSentence());
            }
            else //lido quando acaba os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                turnoAtual = 0;
                player.EnableControls();
            }
        }
    }

   
}

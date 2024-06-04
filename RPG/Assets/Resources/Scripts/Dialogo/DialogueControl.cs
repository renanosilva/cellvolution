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

    //public NPC NPC;
    public Char player;
    //public Dialogue1 dialogue;
    private Sprite spriteNpc;
    private string nomeNpc;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;
     private bool showFullText = false;
    

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        spriteNpc = p;
        sentences = txt;
        actorNameText.text = actorName;
        nomeNpc = actorName;
        StartCoroutine(TypeSentence());
    }

       IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            if (showFullText) // Verifica se deve mostrar todo o texto imediatamente
            {
                speechText.text = sentences[index];
                showFullText = false; // Reinicia a variável para o próximo texto
                yield break; // Sai da coroutine
            }

            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void FixedUpdate(){
        if(dialogueObj.activeSelf){
            
              if(Input.GetKeyDown(KeyCode.P)){
                GoToLastSentence();
              }

              if (Input.GetKeyDown(KeyCode.LeftShift    )){
                showFullText = true;
              }

             /* if (Input.GetKeyDown(KeyCode.Return)){
                NextSentence();
            }*/

        }
    }

   public void GoToLastSentence()
{
    index = sentences.Length - 1; // Define o índice para o último elemento do array
    StopAllCoroutines(); // Interrompe todas as coroutines em execução
    speechText.text = sentences[index]; // Atualiza o texto do diálogo para o último elemento

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
                        actorNameText.text = nomeNpc;
                        nameAtual = 0;
                        turnoAtual++;
                        profile.sprite = spriteNpc;
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
                nameAtual = 0;
               
            }
        } 
    }

   
}

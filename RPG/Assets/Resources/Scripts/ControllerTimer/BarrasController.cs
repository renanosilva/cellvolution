using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrasController : MonoBehaviour
{
   [SerializeField]
   private Slider slider; 

   public Text ValorAtual;

   public Text ValorMaximo;



   public float vidaMaxima{
        set{
            slider.maxValue = value;
            if(ValorMaximo != null){
                ValorMaximo.text = value.ToString("F0");

            }
        }
        get{
            return slider.maxValue;
        }
    }

    public float vidaAtual{
        set{
            slider.value = value;

            if(ValorAtual != null){

                ValorAtual.text = value.ToString("F0");
            }
        }
        get{
            return slider.value;
        }
   }

    public void SetActive(){
        gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    public RectTransform[] textElements; // Array de textos que vão subir
    public float delayTexts = 1.0f; // Tempo de espera entre os textos
    public float speed = 50.0f; // Velocidade de subida dos textos

    private void Start()
    {
        StartCoroutine(PlayCredits());
    }

    private IEnumerator PlayCredits()
    {
        foreach (RectTransform text in textElements)
        {
            StartCoroutine(MoveTextUpwards(text));
            yield return new WaitForSeconds(delayTexts);
        }
    }

    private IEnumerator MoveTextUpwards(RectTransform text)
    {
        float startPosition = text.anchoredPosition.y;
        float endPosition = startPosition + Screen.height; 

        while (text.anchoredPosition.y < endPosition)
        {
            text.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
            yield return null;
        }
    }
}

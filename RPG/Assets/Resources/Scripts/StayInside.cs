using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    // Referência para a câmera do minimapa
    public Transform MinimapCam;
    // Tamanho do minimapa
    public float MinimapSize;
    // Variável temporária para armazenar a posição
    Vector3 TempV3;

    // Método chamado a cada frame
    void Update()
    {
        // Atualiza a posição do objeto para coincidir com a posição do objeto pai, mas mantendo a mesma altura (y)
        TempV3 = transform.parent.transform.position;
        TempV3.y = transform.position.y;
        transform.position = TempV3;
    }

    // Método chamado após o Update em todos os objetos
    private void LateUpdate()
    {
        // Limita a posição do objeto para que ele permaneça dentro dos limites do minimapa
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MinimapCam.position.x - MinimapSize, MinimapSize + MinimapCam.position.x), // Limita a posição em X
            transform.position.y, // Mantém a posição em Y inalterada
            Mathf.Clamp(transform.position.z, MinimapCam.position.z - MinimapSize, MinimapSize + MinimapCam.position.z) // Limita a posição em Z
        );
    }
}

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable] // Permite que a classe seja serializável e exibida no Unity Inspector
    public class QuestObjetivo 
    {
        // Tipo de objetivo da quest (coletar itens ou pressionar botão)
        public TipoObjetivo tipoObjetivo;
        // Referência para o inventário
        public Inventory inventory;
        // Lista que armazena a quantidade atual de cada item necessário para completar o objetivo
        public List<int> QuantAtual;
        // Lista que armazena a quantidade requerida de cada item para completar o objetivo
        public List<int> QuantRequerida;
        // Lista que armazena os IDs dos itens necessários para completar o objetivo
        public List<int> idItem;
        private bool r; 
    // Código da tecla associada ao objetivo de pressionar botão

        public List<Enemy> inimigos;
        private int verificador;
        public KeyCode keyCode;

        // Método para verificar se o objetivo foi completado
        public bool completou()
        {
             r = false;
             verificador = inimigos.Count;
            if(tipoObjetivo == TipoObjetivo.eliminarInimigo){

                foreach(Enemy posicao in inimigos){
                    if(posicao == null){
                       verificador--;

                       if(verificador <=0){
                           r = true;                            
                       }
                    }
                }
            }
            if(tipoObjetivo == TipoObjetivo.pressioneBotão){
                if(Input.GetKey(keyCode)){
                    r = true;
                }
            }

            
            if(tipoObjetivo == TipoObjetivo.Colete){
                // Percorre a lista de quantidades requeridas e verifica se todas as quantidades atuais são maiores ou iguais às requeridas
                for(int i = 0; i < QuantRequerida.Count; i++)
                {
                    if (QuantAtual[i] >= QuantRequerida[i])
                    {
                        r = true;
                    }
                    else
                    {
                        r = false;
                        break; // Se uma quantidade atual for menor que a requerida, sai do loop
                    }
                }
            }
            return  r;
        }
        
        // Método para atualizar o progresso de coleta de itens
        public void ProgressoColeta(List<int> itensNecessarios,List<int> idN)
        {
            if(tipoObjetivo == TipoObjetivo.Colete){
                // Percorre o inventário

                for (int i = 0; i < inventory.itemInInv.Count; i++)
                {
                    int id = inventory.itemInInv[i].id;
                    // Compara os IDs dos itens no inventário com os IDs dos itens necessários
                    for (int l = 0; l< QuantAtual.Count; l++)
                    {
                        if(idN[l] == id)
                        {
                            // Atualiza a quantidade atual de cada item necessária para completar o objetivo
                            QuantAtual[l] = inventory.itemInInv[i].count;
                        }
                    }
                }
                // Atualiza a lista de quantidades requeridas com os novos valores
                QuantRequerida = itensNecessarios;

            }
        }
    }

    // Enumeração que define os tipos de objetivo possíveis
    public enum TipoObjetivo
    {
        pressioneBotão, // Objetivo de pressionar botão
        Colete, // Objetivo de coletar itens
        eliminarInimigo,
        none 
        
    }

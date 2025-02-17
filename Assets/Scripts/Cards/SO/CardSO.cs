using UnityEngine;

public abstract class CardSO : ScriptableObject
{
    public string nome;
    [Multiline]
    public string descricao;
    public int dano;
    [Range(2, 15)]
    public int falha;
    [Range(6, 19)]
    public int sucesso;
    public GameObject cardPFB;
}

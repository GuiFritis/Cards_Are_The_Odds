using UnityEngine;

[CreateAssetMenu(menuName = "SO/Card")]
public class CardSO : ScriptableObject
{
    public string nome;
    [TextArea]
    public string descricao;
    public CardType cardType;
    public int dano;
    public int combustivel;
    [Tooltip("The time the cards takes to finish it's action")]
    public float duration = 1f;
    [Range(2, 15)]
    public int falha;
    [Range(6, 19)]
    public int sucesso;
    public GameObject cardPFB;
}

public enum CardType
{
    BASIC_ATTACK = 1,
    TECNOLOGY = 2
}

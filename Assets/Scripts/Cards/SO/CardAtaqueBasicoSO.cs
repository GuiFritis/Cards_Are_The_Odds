using UnityEngine;

[CreateAssetMenu(menuName = "SO/Cards/Ataque Básico")]
public class CardAtaqueBasicoSO : CardSO
{
    [Range(1, 5)]
    public int combustivelGerado;
}

using UnityEngine;
using Utils.Singleton;

public class Dice : Singleton<Dice>
{
    public int ThrowDice(int advantage = 0)
    {
        int dieResult = Random.Range(1, 21);
        dieResult = Mathf.Clamp(dieResult, 1, 20);
        dieResult = dieResult switch
        {
            20 => 20,
            1 => 1,
            _ => dieResult + advantage
        };
        ShowNumber(dieResult);
        return dieResult;
    }

    private void ShowNumber(int result)
    {
        if(DamageText_Pooling.Instance != null)
        {
            DamageText_UI _damageText = DamageText_Pooling.Instance.GetPoolItem();
            _damageText.TextMesh.text = result.ToString();
            _damageText.transform.position = transform.position;
            _damageText.TextMesh.color = Color.cyan;
            _damageText.Play();
        }
    }
}

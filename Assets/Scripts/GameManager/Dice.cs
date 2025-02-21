using System.Collections;
using UnityEngine;
using Utils.Singleton;

public class Dice : Singleton<Dice>
{
    [SerializeField] private Animation _animation;

    public IEnumerator ThrowDice(System.Action<int> returnResult, int advantage = 0, int failure = -1, int success = -1)
    {
        if(_animation != null)
        {
            _animation.Play();
        }
        int result = Random.Range(1, 21);
        result = Mathf.Clamp(result, 1, 20);
        result = result switch
        {
            20 => 20,
            1 => 1,
            _ => result + advantage
        };
        yield return StartCoroutine(ShowNumber(result, GetRollColor(result, success, failure)));
        returnResult?.Invoke(result);
    }

    private IEnumerator ShowNumber(int result, Color color)
    {
        while(_animation != null && _animation.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        if(DamageText_Pooling.Instance != null)
        {
            DamageText_UI _damageText = DamageText_Pooling.Instance.GetPoolItem();
            _damageText.TextMesh.color = color;
            _damageText.TextMesh.text = (result==0||result==20?"<b>":"")+result+(result==0||result==20?"</b>":"");
            _damageText.transform.position = transform.position;
            _damageText.Play(1.5f);
        }
    }

    private Color GetRollColor(int result, int success, int failure)
    {
        if(success == failure)
        {
            return Color.white;
        }
        if(result >= success)
        {
            return Color.green;
        }
        return Color.red;
    }
}

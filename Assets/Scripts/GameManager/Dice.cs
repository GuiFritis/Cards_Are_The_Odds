using System.Collections;
using UnityEngine;
using Utils.Singleton;

public class Dice : Singleton<Dice>
{
    [SerializeField] private Animation _animation;
    [SerializeField] private SOAudio _diceThrowAudio;
    [SerializeField] private SOAudio _criticalSuccessAudio;
    [SerializeField] private SOAudio _successAudio;
    [SerializeField] private SOAudio _failureAudio;
    [SerializeField] private SOAudio _criticalFailureAudio;

    public IEnumerator ThrowDice(System.Action<int> returnResult, int advantage = 0, int failure = -1, int success = -1)
    {
        SFX_Pool.Instance.Play(_diceThrowAudio);
        if(_animation != null)
        {
            _animation.Play();
        }
        int result = Random.Range(1, 21);
        result = result switch
        {
            20 => 20,
            1 => 1,
            _ => result + advantage
        };
        result = Mathf.Clamp(result, 1, 20);
        yield return StartCoroutine(ShowNumber(result, failure, success));
        returnResult?.Invoke(result);
    }

    private IEnumerator ShowNumber(int result, int failure, int success)
    {
        while(_animation != null && _animation.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        if(DamageText_Pooling.Instance != null)
        {
            DamageText_UI _damageText = DamageText_Pooling.Instance.GetPoolItem();
            _damageText.TextMesh.color = GetRollColor(result, failure, success);
            _damageText.TextMesh.text = (result<failure||result==20?"<b>":"")+result+(result<failure||result==20?"</b>":"");
            _damageText.transform.position = transform.position;
            _damageText.Play(.8f, result<failure||result==20?40:24);
        }
        SFX_Pool.Instance.Play(GetAudio(result, failure, success));
        yield return new WaitForSeconds(.8f);
    }

    private Color GetRollColor(int result, int failure, int success)
    {
        if(success == failure)
        {
            return Color.white;
        }
        else if(result == 20)
        {
            return new Color(0.2f, 1f, 0);
        }
        else if(result >= success)
        {
            return Color.green;
        }
        return Color.red;
    }

    private SOAudio GetAudio(int result, int failure, int success)
    {
        if(success == failure)
        {
            return _successAudio;
        }
        else if(result == 20)
        {
            return _criticalSuccessAudio;
        }
        else if(result >= success)
        {
            return _successAudio;
        }
        else if(result > failure)
        {
            return _failureAudio;
        }
        return _criticalFailureAudio;
        
    }
}

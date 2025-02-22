using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private SOAudio _keyboardAudio;
    [SerializeField] private SOAudio _keyboardSpaceAudio;
    private bool _type = true;

    public void DisplayLore()
    {
        StartCoroutine(TypeLore());
    }

    private IEnumerator TypeLore()
    {
        string text = _textMesh.text;
        _textMesh.text = "";
        foreach (char letter in text)
        {
            _textMesh.text += letter;
            SFX_Pool.Instance.Play(letter == ' '?_keyboardSpaceAudio:_keyboardAudio);
            yield return new WaitForSeconds(0.05f);
            if(!_type)
            {
                _textMesh.text = text;
                break;
            }
        }
        yield return new WaitForSeconds(1f);
        float timer = 0;
        while(timer < 1f)
        {
            _textMesh.color = Color.Lerp(Color.white, Color.clear, timer);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(1);   
    }

    public void Skip()
    {
        _type = false;
    }
}

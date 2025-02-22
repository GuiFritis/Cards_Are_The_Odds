using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneByIndex(int index)
    {
        // Transition.SetTrigger("Start");
        SceneManager.LoadScene(index);        
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    //Called from an animation event in the Camera's "Slide" animation clip
    public void PlayGame() => SceneManager.LoadScene("Gameplay");
}

using UnityEngine;

public class MobileInputPanel : MonoBehaviour
{
    private void Awake()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public bool hasForceField;
    public GameObject forceField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) EnableForceField();
    }

    //Called when "Force Field" Button is pressed for Mobile Input, and from "Update" for Keyboard input
    public void EnableForceField()
    {
        if(!hasForceField && PlayerPrefs.GetInt("Force Field") > 0)
        {
            forceField.SetActive(true);
            hasForceField = true;
            UIManager.instance.HandleForceFieldCount();
            StartCoroutine(nameof(DisableForceField));
        }
    }

    IEnumerator DisableForceField()
    {
        yield return new WaitForSeconds(5f);
        forceField.SetActive(false);
        hasForceField = false;
    }
}

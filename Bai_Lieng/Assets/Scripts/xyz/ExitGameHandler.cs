using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameHandler : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePanel(GameObject exitPanel)
    {
        exitPanel.gameObject.SetActive(!exitPanel.gameObject.activeSelf);
    }
}

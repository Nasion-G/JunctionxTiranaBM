using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManagement : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject predictTheFuture;

    public void doExitGame()
    {
        Application.Quit();
    }

    public void changeScreen()
    {
        mainScreen.SetActive(false);
        predictTheFuture.SetActive(true);
    }

    public void goToMainScreen()
    {
        mainScreen.SetActive(true);
        predictTheFuture.SetActive(false);
    }
}

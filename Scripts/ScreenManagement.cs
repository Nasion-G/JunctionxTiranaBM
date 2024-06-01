using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManagement : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject predictTheFuture;
    public GameObject location;

    public void doExitGame()
    {
        Application.Quit();
    }

    public void changeScreen()
    {
        mainScreen.SetActive(false);
        predictTheFuture.SetActive(true);
        location.SetActive(false);
    }

    public void goToMainScreen()
    {
        mainScreen.SetActive(true);
        predictTheFuture.SetActive(false);
        location.SetActive(false);
    }

    public void locationScreen()
    {
        mainScreen.SetActive(false);
        predictTheFuture.SetActive(false);
        location.SetActive(true);
    }
}
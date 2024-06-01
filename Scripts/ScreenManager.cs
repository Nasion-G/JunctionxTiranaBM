using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManagement : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject iaeScreen;
    public GameObject predictTheFuture;
    public GameObject location;
    public GameObject tips;

    public void doExitGame()
    {
        Application.Quit();
    }

    public void goToWelcomeScreen()
    {
        welcomeScreen.SetActive(true);
        iaeScreen.SetActive(false);
        predictTheFuture.SetActive(false);
        location.SetActive(false);
        tips.SetActive(false);
    }

    public void goToIAEScreen()
    {
        welcomeScreen.SetActive(false);
        iaeScreen.SetActive(true);
        predictTheFuture.SetActive(false);
        location.SetActive(false);
        tips.SetActive(false);
    }

    public void goToInvesting()
    {
        welcomeScreen.SetActive(false);
        iaeScreen.SetActive(false);
        predictTheFuture.SetActive(true);
        location.SetActive(false);
        tips.SetActive(false);
    }

    public void locationScreen()
    {
        welcomeScreen.SetActive(false);
        iaeScreen.SetActive(false);
        predictTheFuture.SetActive(false);
        location.SetActive(true);
        tips.SetActive(false);
    }

    public void tipsScreen()
    {
        welcomeScreen.SetActive(false);
        iaeScreen.SetActive(false);
        predictTheFuture.SetActive(false);
        location.SetActive(false);
        tips.SetActive(true);
    }
}
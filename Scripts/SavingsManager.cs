using UnityEngine;
using TMPro;
using System;

public class SavingsManager : MonoBehaviour
{
    public TMP_Text savingsText;

    private const string FirstOpenDateKey = "FirstOpenDate";
    private const string SavingsKey = "Savings";

    // Function to initialize the savings and the first open date
    void InitializeSavings()
    {
        if (!PlayerPrefs.HasKey(FirstOpenDateKey))
        {
            // First time opening the app, set the first open date
            PlayerPrefs.SetString(FirstOpenDateKey, DateTime.Today.ToString("yyyy-MM-dd"));

            // Set initial savings value
            PlayerPrefs.SetInt(SavingsKey, 0);
            PlayerPrefs.Save();
        }
    }

    // Function to calculate savings based on the progression of months
    void CalculateMonthlySavings()
    {
        // Get the first open date
        string firstOpenDateString = PlayerPrefs.GetString(FirstOpenDateKey);
        DateTime firstOpenDate = DateTime.ParseExact(firstOpenDateString, "yyyy-MM-dd", null);

        // Calculate the number of months since the app was first opened
        int monthsSinceFirstOpen = ((DateTime.Today.Year - firstOpenDate.Year) * 12) + DateTime.Today.Month - firstOpenDate.Month;

        // Get the current savings
        int currentSavings = PlayerPrefs.GetInt(SavingsKey, 0);

        // Update savings based on the number of months
        int updatedSavings = currentSavings;

        for (int i = 0; i < monthsSinceFirstOpen; i++)
        {
            // Double the savings every month
            updatedSavings *= 2;
        }

        // Save the updated savings
        PlayerPrefs.SetInt(SavingsKey, updatedSavings);
        PlayerPrefs.Save();

        // Update the TMP text with the calculated savings value
        savingsText.text = updatedSavings.ToString();
    }

    void Start()
    {
        // Initialize savings if necessary
        InitializeSavings();

        // Calculate and update monthly savings
        CalculateMonthlySavings();
    }
}

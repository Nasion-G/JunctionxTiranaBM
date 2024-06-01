using System;
using TMPro;
using UnityEngine;

public class PredictTheFuture : MonoBehaviour
{
    public TMP_InputField savingsField;
    public TMP_InputField interestRateField;
    public TMP_InputField yearsField;
    public TMP_Text result;

    void Start()
    {
        savingsField.text = "0";
        interestRateField.text = "0";
        yearsField.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input values
        float monthlyDeposit = ParseFloat(savingsField.text);
        float annualRate = ParseFloat(interestRateField.text) / 100;
        int years = ParseInt(yearsField.text);

        // Calculate the monthly interest rate and the total number of months
        float monthlyRate = annualRate / 12;
        int totalMonths = years * 12;

        // Calculate the future value of monthly deposits
        float futureValue = monthlyDeposit * (Mathf.Pow(1 + monthlyRate, totalMonths) - 1) / monthlyRate;

        // Display the result
        if (futureValue > 0) result.text = futureValue.ToString("F0");
        else result.text = "";
    }

    // Helper method to parse float and handle empty or invalid input
    private float ParseFloat(string input)
    {
        if (float.TryParse(input, out float value))
        {
            return value;
        }
        return 0f;
    }

    // Helper method to parse int and handle empty or invalid input
    private int ParseInt(string input)
    {
        if (int.TryParse(input, out int value))
        {
            return value;
        }
        return 0;
    }
}

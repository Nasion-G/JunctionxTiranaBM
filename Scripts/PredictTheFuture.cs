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
        float monthlyDeposit = Convert.ToSingle(savingsField.text);
        float annualRate = Convert.ToSingle(interestRateField.text) / 100;
        int years = Convert.ToInt32(yearsField.text);

        // Calculate the monthly interest rate and the total number of months
        float monthlyRate = annualRate / 12;
        int totalMonths = years * 12;

        // Calculate the future value of monthly deposits
        float futureValue = monthlyDeposit * (Mathf.Pow(1 + monthlyRate, totalMonths) - 1) / monthlyRate;

        // Display the result
        result.text = futureValue.ToString("F0");  // Format to 2 decimal places
    }
}

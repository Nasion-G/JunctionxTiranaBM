using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagement : MonoBehaviour
{
    public TMP_InputField incomeField;
    public int income;
    public TMP_Text totalExpField;
    public int totalExpenses;
    public TMP_Text moneyLeftField;
    public int moneyLeft;
    public TMP_InputField rentField;
    public int rent;
    public TMP_InputField utilitiesField;
    public int utilities;
    public TMP_InputField entField;
    public int ent;
    public TMP_InputField medField;
    public int med;
    public TMP_InputField groceriesField;
    public int groceries;
    public TMP_InputField transportationField;
    public int transportation;
    public TMP_InputField persCareField;
    public int persCare;
    public TMP_InputField subsField;
    public int subs;
    public TMP_InputField insuranceField;
    public int insurance;
    public TMP_InputField debtField;
    public int debt;
    public TMP_InputField miscField;
    public int misc;
    public TMP_InputField holidaysField;
    public int holidays;
    public TMP_InputField savingsField;
    public int savings;
    public TMP_Text tips;
    public Image savingsBar;
    public Image moneyLeftBar;

    void Start()
    {
        income = PlayerPrefs.GetInt("Income", 0);
        incomeField.text = income.ToString();
        rent = PlayerPrefs.GetInt("Rent", 0);
        rentField.text = rent.ToString();
        utilities = PlayerPrefs.GetInt("Utilities", 0);
        utilitiesField.text = utilities.ToString();
        ent = PlayerPrefs.GetInt("Entertainment", 0);
        entField.text = ent.ToString();
        med = PlayerPrefs.GetInt("Medical", 0);
        medField.text = med.ToString();
        groceries = PlayerPrefs.GetInt("Groceries", 0);
        groceriesField.text = groceries.ToString();
        transportation = PlayerPrefs.GetInt("Transportation", 0);
        transportationField.text = transportation.ToString();
        persCare = PlayerPrefs.GetInt("PersonalCare", 0);
        persCareField.text = persCare.ToString();
        subs = PlayerPrefs.GetInt("Subscriptions", 0);
        subsField.text = subs.ToString();
        insurance = PlayerPrefs.GetInt("Insurance", 0);
        insuranceField.text = insurance.ToString();
        debt = PlayerPrefs.GetInt("Debt", 0);
        debtField.text = debt.ToString();
        misc = PlayerPrefs.GetInt("Miscellaneous", 0);
        miscField.text = misc.ToString();
        holidays = PlayerPrefs.GetInt("Holidays", 0);
        holidaysField.text = holidays.ToString();
        savings = PlayerPrefs.GetInt("Savings", 0);
        savingsField.text = savings.ToString();

        UpdateFields();
    }

    void Update()
    {
        income = GetValueFromField(incomeField, "Income");
        rent = GetValueFromField(rentField, "Rent");
        utilities = GetValueFromField(utilitiesField, "Utilities");
        ent = GetValueFromField(entField, "Entertainment");
        med = GetValueFromField(medField, "Medical");
        groceries = GetValueFromField(groceriesField, "Groceries");
        transportation = GetValueFromField(transportationField, "Transportation");
        persCare = GetValueFromField(persCareField, "PersonalCare");
        subs = GetValueFromField(subsField, "Subscriptions");
        insurance = GetValueFromField(insuranceField, "Insurance");
        debt = GetValueFromField(debtField, "Debt");
        misc = GetValueFromField(miscField, "Miscellaneous");
        holidays = GetValueFromField(holidaysField, "Holidays");
        savings = GetValueFromField(savingsField, "Savings");

        UpdateFields();
    }

    int GetValueFromField(TMP_InputField field, string key)
    {
        int value;
        if (string.IsNullOrEmpty(field.text))
        {
            value = 0;
        }
        else
        {
            value = Convert.ToInt32(field.text);
        }
        PlayerPrefs.SetInt(key, value);
        return value;
    }

    void UpdateFields()
    {
        totalExpenses = rent + utilities + ent + med + groceries + transportation + persCare + subs + insurance + debt + misc;
        totalExpField.text = totalExpenses.ToString();

        moneyLeft = income - totalExpenses - savings;
        moneyLeftField.text = moneyLeft.ToString();

        if (savings > income - totalExpenses)
        {
            savings = income - totalExpenses;
            savingsField.text = savings.ToString();
        }

        if (moneyLeft < totalExpenses)
        {
            tips.text = "Tip: Your total expenses seem to be higher than your income. See if you can change any of your spending habits.";
        }
        else if (moneyLeft > 0.5 * totalExpenses)
        {
            tips.text = "Tip: You are doing a great job at managing your income. You surely deserve an applause! See if you can start investing some of that.";
        }
        else
        {
            tips.text = string.Empty;
        }

        CheckAndDisplayTips();

        savingsBar.fillAmount = savings != 0 ? (float)savings / income : 0;
        moneyLeftBar.fillAmount = moneyLeft != 0 ? (float)moneyLeft / income : 0;
    }

    void CheckAndDisplayTips()
    {
        bool allFieldsNonZero = rent != 0 && utilities != 0 && ent != 0 && med != 0 && groceries != 0 && transportation != 0 && persCare != 0 && subs != 0 && insurance != 0 && debt != 0 && misc != 0;
        if (allFieldsNonZero)
        {
            if (moneyLeft < totalExpenses)
            {
                tips.text = "Tip: Your total expenses seem to be higher than your income. See if you can change any of your spending habits.";
            }
            else if (moneyLeft > 0.5 * totalExpenses)
            {
                tips.text = "Tip: You are doing a great job at managing your income. You surely deserve an applause! See if you can start investing some of that.";
            }
        }
        else
        {
            tips.text = "Tip: Please ensure all expense fields are filled out with non-zero values for accurate tips.";
        }
    }
}

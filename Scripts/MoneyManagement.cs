using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagement : MonoBehaviour
{
    //do array looping thorugh all children of scroll view panel
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

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 30;
        income = PlayerPrefs.GetInt("Income");
        incomeField.text = Convert.ToString(income);
        rent = PlayerPrefs.GetInt("Rent");
        rentField.text = Convert.ToString(rent);
        utilities = PlayerPrefs.GetInt("Utilities");
        utilitiesField.text = Convert.ToString(utilities);
        ent = PlayerPrefs.GetInt("Entertainment");
        entField.text = Convert.ToString(ent);
        med = PlayerPrefs.GetInt("Medical");
        medField.text = Convert.ToString(med);
        groceries = PlayerPrefs.GetInt("Groceries");
        groceriesField.text = Convert.ToString(groceries);
        transportation = PlayerPrefs.GetInt("Transportation");
        transportationField.text = Convert.ToString(transportation);
        persCare = PlayerPrefs.GetInt("PersonalCare");
        persCareField.text = Convert.ToString(persCare);
        subs = PlayerPrefs.GetInt("Subscriptions");
        subsField.text = Convert.ToString(subs);
        insurance = PlayerPrefs.GetInt("Insurance");
        insuranceField.text = Convert.ToString(insurance);
        debt = PlayerPrefs.GetInt("Debt");
        debtField.text = Convert.ToString(debt);
        misc = PlayerPrefs.GetInt("Miscellaneous");
        miscField.text = Convert.ToString(misc);
        holidays = PlayerPrefs.GetInt("Holidays");
        holidaysField.text = Convert.ToString(holidays);
        savings = PlayerPrefs.GetInt("Savings");
        savingsField.text = Convert.ToString(savings);

        savingsBar.fillAmount = (float)savings / (float)income;
        moneyLeftBar.fillAmount = (float)moneyLeft / (float)income;

        totalExpenses = rent + utilities + ent + med + groceries + transportation + persCare + subs + insurance + debt + misc;
        totalExpField.text = Convert.ToString(totalExpenses);

        moneyLeft = income - totalExpenses;
        moneyLeftField.text = Convert.ToString(moneyLeft);
    }

    // Update is called once per frame
    void Update()
    {
        income = Convert.ToInt32(incomeField.text);
        PlayerPrefs.SetInt("Income", income);

        rent = Convert.ToInt32(rentField.text);
        PlayerPrefs.SetInt("Rent", rent);
        utilities = Convert.ToInt32(utilitiesField.text);
        PlayerPrefs.SetInt("Utilities", utilities);
        ent = Convert.ToInt32(entField.text);
        PlayerPrefs.SetInt("Entertainment", ent);
        med = Convert.ToInt32(medField.text);
        PlayerPrefs.SetInt("Medical", med);
        groceries = Convert.ToInt32(groceriesField.text);
        PlayerPrefs.SetInt("Groceries", groceries);
        transportation = Convert.ToInt32(transportationField.text);
        PlayerPrefs.SetInt("Transportation", transportation);
        persCare = Convert.ToInt32(persCareField.text);
        PlayerPrefs.SetInt("PersonalCare", persCare);
        subs = Convert.ToInt32(subsField.text);
        PlayerPrefs.SetInt("Subscriptions", subs);
        insurance = Convert.ToInt32(insuranceField.text);
        PlayerPrefs.SetInt("Insurance", insurance);
        debt = Convert.ToInt32(debtField.text);
        PlayerPrefs.SetInt("Debt", debt);
        misc = Convert.ToInt32(miscField.text);
        PlayerPrefs.SetInt("Miscellaneous", misc);
        holidays = Convert.ToInt32(holidaysField.text);
        PlayerPrefs.SetInt("Holidays", holidays);
        savings = Convert.ToInt32(savingsField.text);
        PlayerPrefs.SetInt("Savings", savings);

        if (savings > income - totalExpenses)
        {
            savings = income - totalExpenses;
            savingsField.text = Convert.ToString(income - totalExpenses);
        }

        totalExpenses = rent + utilities + ent + med + groceries + transportation + persCare + subs + insurance + debt + misc;
        totalExpField.text = Convert.ToString(totalExpenses);

        moneyLeft = income - totalExpenses - savings;
        moneyLeftField.text = Convert.ToString(moneyLeft);

        if (moneyLeft < totalExpenses)
        {
            tips.text = "Tip: Your total expenses seem to be higher than your income. See if you can change any of your spending habits.";
        }
        else if (moneyLeft > 0.5 * totalExpenses)
        {
            tips.text = "Tip: You are doing a great job at managing your income. You surely deserve an applause! See if you can start investing some of that.";
        }

        CheckAndDisplayTips();

        if (savings != 0) savingsBar.fillAmount = (float)savings / (float)income;
        if (moneyLeft != 0) moneyLeftBar.fillAmount = (float)moneyLeft / (float)income;
        else moneyLeftBar.fillAmount = 0;
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

        if (!allFieldsNonZero)
        {
            tips.text = "Tip: Please ensure all expense fields are filled out with non-zero values for accurate tips.";
        }
    }
}

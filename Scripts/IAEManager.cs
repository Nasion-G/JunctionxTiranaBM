using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class IAEManager : MonoBehaviour
{
    public GameObject subcategoryPrefab; // Prefab for subcategory
    public Transform content; // Content area of the ScrollView
    public Button addButton; // Button to add subcategories
    public int maxSubcategories = 13; // Maximum number of subcategories allowed
    public TMP_Text totalExpensesText; // TMP text to display total expenses
    public TMP_Text moneyLeftText; // TMP text to display money left
    public TMP_InputField incomeInputField; // Input field for income
    public TMP_InputField savingsInputField; // Input field for savings
    public Image savingsBar;
    public Image moneyLeftBar;

    private List<Subcategory> subcategories = new List<Subcategory>();

    public readonly List<string> defaultCategoryNames = new List<string> { "Rent", "Utilities", "Groceries", "Entertainment" };

    void Start()
    {
        addButton.onClick.AddListener(AddSubcategory);
        RestoreSubcategories();
        RestoreIncomeAndSavings();
        InitializeDefaultCategories();
        incomeInputField.onEndEdit.AddListener(delegate { SaveIncome(); SaveSubcategories(); });
        savingsInputField.onEndEdit.AddListener(delegate { SaveSavings(); });
    }

    void Update()
    {
        UpdateTotalExpenses();
        UpdateMoneyLeft();
    }

    void AddSubcategory()
    {
        if (subcategories.Count >= maxSubcategories)
        {
            Debug.Log("Maximum number of subcategories reached.");
            return;
        }

        string id = System.Guid.NewGuid().ToString();
        GameObject newSubcategory = Instantiate(subcategoryPrefab, content);
        subcategories.Add(new Subcategory { Id = id, GameObject = newSubcategory });

        TMP_InputField[] inputFields = newSubcategory.GetComponentsInChildren<TMP_InputField>();
        inputFields[0].onEndEdit.AddListener(delegate { SaveSubcategories(); });
        inputFields[1].onEndEdit.AddListener(delegate { SaveSubcategories(); });
    }

    void SaveIncome()
    {
        if (int.TryParse(incomeInputField.text, out int income))
        {
            PlayerPrefs.SetInt("Income", income);
            PlayerPrefs.Save();
        }
    }

    void SaveSubcategories()
    {
        for (int i = 0; i < subcategories.Count; i++)
        {
            TMP_InputField[] inputFields = subcategories[i].GameObject.GetComponentsInChildren<TMP_InputField>();
            string subcategoryName = inputFields[0].text;
            int subcategoryValue = int.TryParse(inputFields[1].text, out subcategoryValue) ? subcategoryValue : 0;

            PlayerPrefs.SetString($"Subcategory_{i}_Id", subcategories[i].Id);
            PlayerPrefs.SetString($"Subcategory_{i}_Name", subcategoryName);
            PlayerPrefs.SetInt($"Subcategory_{i}_Value", subcategoryValue);
        }

        PlayerPrefs.SetInt("SubcategoryCount", subcategories.Count);
        PlayerPrefs.Save();
    }

    void SaveSavings()
    {
        if (int.TryParse(savingsInputField.text, out int savings))
        {
            savings = Mathf.Clamp(savings, 0, CalculateMaxSavings());
            savingsInputField.text = savings.ToString();
            PlayerPrefs.SetInt("Savings", savings);
            PlayerPrefs.Save();
        }
    }

    void RestoreSubcategories()
    {
        int subcategoryCount = PlayerPrefs.GetInt("SubcategoryCount", 0);

        for (int i = 0; i < subcategoryCount; i++)
        {
            string id = PlayerPrefs.GetString($"Subcategory_{i}_Id");
            string subcategoryName = PlayerPrefs.GetString($"Subcategory_{i}_Name");
            int subcategoryValue = PlayerPrefs.GetInt($"Subcategory_{i}_Value");

            GameObject newSubcategory = Instantiate(subcategoryPrefab, content);
            subcategories.Add(new Subcategory { Id = id, GameObject = newSubcategory });

            TMP_InputField[] inputFields = newSubcategory.GetComponentsInChildren<TMP_InputField>();
            inputFields[0].text = subcategoryName;
            inputFields[1].text = subcategoryValue.ToString();

            inputFields[0].onEndEdit.AddListener(delegate { SaveSubcategories(); });
            inputFields[1].onEndEdit.AddListener(delegate { SaveSubcategories(); });
        }
    }

    void RestoreIncomeAndSavings()
    {
        int income = PlayerPrefs.GetInt("Income", 0);
        incomeInputField.text = income.ToString();

        int savings = PlayerPrefs.GetInt("Savings", 0);
        savingsInputField.text = savings.ToString();
    }

    void InitializeDefaultCategories()
    {
        foreach (string categoryName in defaultCategoryNames)
        {
            if (!SubcategoryExists(categoryName))
            {
                string id = System.Guid.NewGuid().ToString();
                GameObject newSubcategory = Instantiate(subcategoryPrefab, content);
                subcategories.Add(new Subcategory { Id = id, GameObject = newSubcategory });

                TMP_InputField[] inputFields = newSubcategory.GetComponentsInChildren<TMP_InputField>();
                inputFields[0].text = categoryName;
                inputFields[1].text = "0";

                inputFields[0].onEndEdit.AddListener(delegate { SaveSubcategories(); });
                inputFields[1].onEndEdit.AddListener(delegate { SaveSubcategories(); });
            }
        }
    }

    bool SubcategoryExists(string name)
    {
        foreach (var subcategory in subcategories)
        {
            TMP_InputField[] inputFields = subcategory.GameObject.GetComponentsInChildren<TMP_InputField>();
            if (inputFields[0].text == name)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateTotalExpenses()
    {
        int totalExpenses = 0;

        foreach (var subcategory in subcategories)
        {
            TMP_InputField valueField = subcategory.GameObject.GetComponentsInChildren<TMP_InputField>()[1];
            if (int.TryParse(valueField.text, out int value))
            {
                totalExpenses += value;
            }
        }

        totalExpensesText.text = $"{totalExpenses}";
    }

    void UpdateMoneyLeft()
    {
        int income = int.TryParse(incomeInputField.text, out income) ? income : 0;
        int totalExpenses = int.TryParse(totalExpensesText.text, out totalExpenses) ? totalExpenses : 0;
        int savings = int.TryParse(savingsInputField.text, out savings) ? Mathf.Clamp(savings, 0, CalculateMaxSavings()) : 0;

        savingsInputField.text = savings.ToString();
        int moneyLeft = income - totalExpenses - savings;
        moneyLeftText.text = $"{moneyLeft}";

        savingsBar.fillAmount = income != 0 ? (float)savings / income : 0;
        moneyLeftBar.fillAmount = income != 0 ? (float)moneyLeft / income : 0;
    }

    int CalculateMaxSavings()
    {
        int income = int.TryParse(incomeInputField.text, out income) ? income : 0;
        int totalExpenses = int.TryParse(totalExpensesText.text, out totalExpenses) ? totalExpenses : 0;
        return Mathf.Max(0, income - totalExpenses);
    }

    private class Subcategory
    {
        public string Id { get; set; }
        public GameObject GameObject { get; set; }
    }
}

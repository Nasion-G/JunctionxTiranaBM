using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TipsManager : MonoBehaviour
{
    public TMP_Text tipsText; // Text component to display tips
    public IAEManager expenseManager; // Reference to the expense manager

    private Dictionary<string, string> tipsDictionary = new Dictionary<string, string>()
    {
        { "Transport", "Consider using public transportation to save on transport expenses." },
        { "Food", "Plan your meals ahead of time and cook at home to save on food expenses." },
        { "Entertainment", "Look for free or low-cost entertainment options in your area, such as parks or museums." },
        { "Shopping", "Avoid impulse purchases and only buy items that you need." },
        // Add more tips for other expense categories
    };

    void Start()
    {
        UpdateTips();
    }

    void UpdateTips()
    {
        string tips = GenerateTips();
        tipsText.text = tips;
    }

    string GenerateTips()
    {
        string generatedTips = "Here are some tips based on your expenses:\n\n";

        foreach (var categoryName in expenseManager.defaultCategoryNames)
        {
            if (tipsDictionary.ContainsKey(categoryName))
            {
                generatedTips += $"{tipsDictionary[categoryName]}\n\n";
            }
        }

        return generatedTips;
    }
}

using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TipsManager : MonoBehaviour
{
    public TMP_Text tipsText; // Text component to display tips
    public IAEManager expenseManager; // Reference to the expense manager
    public TranslationManager translationManager;

    private Dictionary<string, string> tipsDictionaryEn = new Dictionary<string, string>()
    {
        { "Transport", "Consider using public transportation to save on transport expenses." },
        { "Food", "Plan your meals ahead of time and cook at home to save on food expenses." },
        { "Entertainment", "Look for free or low-cost entertainment options in your area, such as parks or museums." },
        { "Shopping", "Avoid impulse purchases and only buy items that you need." },
        // Add more tips for other expense categories
    };

    private Dictionary<string, string> tipsDictionaryAl = new Dictionary<string, string>()
    {
        { "Transport", "Konsideroni përdorimin e transportit publik për të kursyer shpenzimet e transportit." },
        { "Ushqim", "Planifikoni ushqimet tuaja para kohe dhe gatuani në shtëpi për të kursyer në shpenzimet e ushqimit." },
        { "Argëtim", "Kërkoni opsione argëtimi falas ose me kosto të ulët në zonën tuaj, si parqe ose muze." },
        { "Blerjet", "Shmangni blerjet impulsive dhe blini vetëm artikujt që ju nevojiten." },
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
        string generatedTips;
        if (translationManager.currentLanguage == TranslationManager.Language.English) generatedTips = "Here are some tips based on your expenses:\n\n";
        else generatedTips = "Këtu janë disa këshilla bazuar në shpenzimet tuaja:\n\n";

        foreach (var categoryName in expenseManager.defaultCategoryNames)
        {
            if (translationManager.currentLanguage == TranslationManager.Language.English)
            {
                if (tipsDictionaryEn.ContainsKey(categoryName))
                {
                    generatedTips += $"{tipsDictionaryEn[categoryName]}\n\n";
                }
            }
            else
            {
                if (tipsDictionaryAl.ContainsKey(categoryName))
                {
                    generatedTips += $"{tipsDictionaryEn[categoryName]}\n\n";
                }
            }
        }

        return generatedTips;
    }
}

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class TranslationManager : MonoBehaviour
{
    public static TranslationManager instance;

    // Dictionary containing translations for English
    public Dictionary<string, string> englishTranslations = new Dictionary<string, string>();

    // Dictionary containing translations for Albanian
    public Dictionary<string, string> albanianTranslations = new Dictionary<string, string>();

    // Current language
    public Language currentLanguage = Language.English;

    // TMP Text fields in the scene
    private TMP_Text[] allTexts;

    // Buttons for switching between languages
    public Button englishButton;
    public Button albanianButton;

    // Enum for languages
    public enum Language
    {
        English,
        Albanian
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Populate dictionaries with translations
        PopulateEnglishTranslations();
        PopulateAlbanianTranslations();

        // Set up button click events
        englishButton.onClick.AddListener(() => SetLanguage(Language.English));
        albanianButton.onClick.AddListener(() => SetLanguage(Language.Albanian));
    }

    void Start()
    {
        // Get all TMP Text components in the scene
        allTexts = FindObjectsOfType<TMP_Text>();

        // Translate all texts initially
        TranslateAllUITexts();
    }

    void SetLanguage(Language language)
    {
        // Set current language
        currentLanguage = language;

        // Translate all texts to the selected language
        TranslateAllUITexts();
    }

    void PopulateEnglishTranslations()
    {
        // Add English translations to the dictionary
        englishTranslations.Add("Vendndodhjet", "Locations");
        englishTranslations.Add("Rezultati:", "Result:");
        englishTranslations.Add("Vitet:", "Years:");
        englishTranslations.Add("Interesi (vjetor):", "Interest (year):");
        englishTranslations.Add("Kursimet:", "Savings:");
        englishTranslations.Add("Mbrapa", "Back");
        englishTranslations.Add("% Para të mbetura", "% Money Left");
        englishTranslations.Add("% Para të kursyera", "% Money Saved");
        englishTranslations.Add("Para të mbetura:", "Money Left:");
        englishTranslations.Add("Të ardhurat(€):", "Income(€):");
        englishTranslations.Add("Shto Kategori", "Add Category");
        englishTranslations.Add("Shqip", "Albanian");
        englishTranslations.Add("Anglisht", "English");
        englishTranslations.Add("Këshilla për të kursyer", "Tips on saving");
        englishTranslations.Add("Të ardhurat dhe shpenzimet", "Income and Expenses");
        englishTranslations.Add("Sa shumë ke kursyer që kur ke filluar të përdoresh aplikacionin:", "How much you have saved since you have started using the app:");
        englishTranslations.Add("Të kursyerit me interes", "Saving with interest");
        englishTranslations.Add("Shpenzimet totale:", "Total Expenses:");
        englishTranslations.Add("Mirësevini në", "Welcome to");
        englishTranslations.Add("Konsideroni përdorimin e transportit publik për të kursyer shpenzime transporti.", "Consider using public transportation to save on transport expenses.");
        englishTranslations.Add("Planifikoni vaktet tuaja përpara dhe gatuani ne shtëpi për të kursyer shpenzime ushqimore.", "Plan your meals ahead of time and cook at home to save on food expenses.");
        englishTranslations.Add("Kërkoni opsione argëtimi falas ose me kosto të ulët në zonën tuaj, si parqet ose muzetë.", "Look for free or low-cost entertainment options in your area, such as parks or museums.");
        englishTranslations.Add("Shmangni blerjet impulsive dhe blini vetëm artikujt që ju nevojiten.", "Avoid impulse purchases and only buy items that you need.");
    }

    void PopulateAlbanianTranslations()
    {
        // Add Albanian translations to the dictionary
        albanianTranslations.Add("Locations", "Vendndodhjet");
        albanianTranslations.Add("Result:", "Rezultati:");
        albanianTranslations.Add("Years:", "Vitet:");
        albanianTranslations.Add("Interest (year):", "Interesi (vjetor):");
        albanianTranslations.Add("Savings:", "Kursimet:");
        albanianTranslations.Add("Back", "Mbrapa");
        albanianTranslations.Add("% Money Left", "% Para të mbetura");
        albanianTranslations.Add("% Money Saved", "% Para të kursyera");
        albanianTranslations.Add("Money Left:", "Para të mbetura:");
        albanianTranslations.Add("Income(€):", "Të ardhurat(€):");
        albanianTranslations.Add("Add Category", "Shto Kategori");
        albanianTranslations.Add("Albanian", "Shqip");
        albanianTranslations.Add("English", "Anglisht");
        albanianTranslations.Add("Tips on saving", "Këshilla për të kursyer");
        albanianTranslations.Add("Income and Expenses", "Të ardhurat dhe shpenzimet");
        albanianTranslations.Add("How much you have saved since you have started using the app:", "Sa shumë ke kursyer që kur ke filluar të përdoresh aplikacionin:");
        albanianTranslations.Add("Saving with interest", "Të kursyerit me interes");
        albanianTranslations.Add("Total Expenses:", "Shpenzimet totale:");
        albanianTranslations.Add("Welcome to", "Mirësevini në");
        albanianTranslations.Add("Consider using public transportation to save on transport expenses.", "Konsideroni përdorimin e transportit publik për të kursyer shpenzime transporti.");
        albanianTranslations.Add("Plan your meals ahead of time and cook at home to save on food expenses.", "Planifikoni vaktet tuaja përpara dhe gatuani ne shtëpi për të kursyer shpenzime ushqimore.");
        albanianTranslations.Add("Look for free or low-cost entertainment options in your area, such as parks or museums.", "Kërkoni opsione argëtimi falas ose me kosto të ulët në zonën tuaj, si parqet ose muzetë.");
        albanianTranslations.Add("Avoid impulse purchases and only buy items that you need.", "Shmangni blerjet impulsive dhe blini vetëm artikujt që ju nevojiten.");
    }

    void TranslateAllUITexts()
    {
        // Find all TMP Text components in the scene, including those on disabled game objects
        TMP_Text[] allTextsIncludingDisabled = Resources.FindObjectsOfTypeAll<TMP_Text>();

        foreach (TMP_Text textComponent in allTextsIncludingDisabled)
        {
            if (!string.IsNullOrEmpty(textComponent.text))
            {
                string translatedText = TranslateText(textComponent.text);
                textComponent.text = translatedText;
            }
        }
    }

    string TranslateText(string originalText)
    {
        string translatedText = originalText;

        // Translate text based on current language
        if (currentLanguage == Language.English && englishTranslations.ContainsKey(originalText))
        {
            translatedText = englishTranslations[originalText];
        }
        else if (currentLanguage == Language.Albanian && albanianTranslations.ContainsKey(originalText))
        {
            translatedText = albanianTranslations[originalText];
        }

        return translatedText;
    }
}

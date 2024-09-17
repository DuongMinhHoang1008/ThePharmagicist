using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Windows.Input;
using UnityEngine.EventSystems;
using UnityEditor.VersionControl;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog UI")]

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;

    public TextMeshProUGUI[] choicesText;

    private static DialogManager instance;

    public Story currentStory;

    public bool dialogIsPlaying;

    public int lettersPerSecond = 10;

    private void Awake()
    {
        instance = this;
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        dialogIsPlaying = false;
        dialogPanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            ContinueStory();
        }
        
    }
    public void EnterDialogMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;
        dialogPanel.SetActive(true);

        ContinueStory();
    } 
    public void ExitDialogMode()
    {
        dialogIsPlaying = false;
        if (dialogPanel != null) {
            dialogPanel.SetActive(false);
        }
        if (dialogText != null) {
            dialogText.text = "";
        }
    }
    public void ContinueStory()
    {
        //dialogText.text = currentStory.Continue();
        if (currentStory.canContinue)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentStory.Continue()));

            if (currentStory.currentChoices.Count > 0)
            {
                DisplayChoices();
            }
        }
        else
        {
            ExitDialogMode();
        }
        

    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        Debug.Log("currentChoice " + currentChoices.Count);

        int index = 0;

        foreach (Choice choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        Debug.Log("index in " + index);

        for (int i = index; i < choicesText.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
            Debug.Log("index in choices " + index);
        }

    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        choices[choiceIndex].gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Windows.Input;
using UnityEngine.EventSystems;

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
        if (!dialogIsPlaying) { 
            return; 
        }

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
        dialogPanel.SetActive(false);
        dialogText.text = "";
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogMode();
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        Debug.Log("currentChoice " + currentChoices.Count);

        int index = 0;

        foreach (Choice choice in currentChoices) {
            Debug.Log("currentChoice " + currentChoices.Count);
            Debug.Log(choice.text);
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choicesText.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(nameof(SelectFitstChoices));
    }
    private IEnumerable SelectFitstChoices()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}

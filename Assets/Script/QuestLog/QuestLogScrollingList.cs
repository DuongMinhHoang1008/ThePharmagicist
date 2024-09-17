using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class QuestLogScrollingList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject contentParent;
    [SerializeField] private GameObject questPrefabs; 

    [SerializeField] public SpawnPatient spawnPatients;
    
    public QuestPrefab[] questStored = new QuestPrefab[5];
    public QuestPrefab quest;

    public static QuestLogScrollingList instance { get; private set; }

    public bool isNeedRefresh = true;

    void Start()
    {
        for (int i = 0; i < 5; i++)
            {
                quest = createQuest(i);

                if (Random.Range(1, 10) > 5) collectHerbalQuest(quest, "Thảo dược");
                else savePeopleQuest(quest, "Cứu người");
                questStored[i] = quest;
                if (i == 0)
                {
                    quest.button.Select();
                }
            }
    }
    void Update()
    {
        if (isNeedRefresh)
        {

            for (int i = 0; i < 5; i++)
            {
                if (Random.Range(1, 10) > 5) collectHerbalQuest(questStored[i], "Thảo dược");
                else
                {
                    /*if (spawnPatients.countBed == spawnPatients.postionObject.Length)
                    {
                        collectHerbalQuest(questStored[i], "Thảo dược");
                    }
                    else
                    {*/
                        savePeopleQuest(questStored[i], "Cứu người");
                    //}
                }
                
                //questStored[0].button.Select();
            }
            isNeedRefresh = false;
        }
    }

    private QuestPrefab createQuest(int index)
    {
        QuestPrefab quest = Instantiate(questPrefabs, contentParent.transform).GetComponent<QuestPrefab>();

        quest.gameObject.name = "Mission " + index;
        quest.Initialize(quest.gameObject.name);

        return quest;
    }

    private void collectHerbalQuest(QuestPrefab quest, string herbalName)
    {
        quest.gameObject.name = "collectHerbal";
        quest.questCollected = 0;
        quest.questColletion = Random.Range(5, 10);

        quest.Initialize("Thu thập thảo dược", quest.questCollected, quest.questColletion);
    }

    private void savePeopleQuest(QuestPrefab quest, string currentPeople)
    {
        quest.gameObject.name = "savePeople";
        quest.questCollected = 0;
        quest.questColletion = 1;

        spawnPatients.SpawnPrefabs();
        spawnPatients.patient.gameObject.SetActive(true);

        quest.Initialize("Chữa bệnh", quest.questCollected, quest.questColletion);
    }
}

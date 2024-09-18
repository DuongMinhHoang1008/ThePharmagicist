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

    public bool isNeedRefresh;

    void Start()
    {
        for (int i = 0; i < 5; i++)
            {
                quest = createQuest(i);

                if (Random.Range(1, 10) > 5) collectHerbalQuest(quest, "Thảo dược");
                else
                {
                    if (spawnPatients.countBed == spawnPatients.postionObject.Length)
                    {
                        collectHerbalQuest(quest, "Thảo dược");
                    }
                    else
                    {
                        //Debug.Log("spawn patient");
                        spawnPatients.SpawnPrefabs();
                        savePeopleQuest(quest, "Cứu người");
                    }
                }

                questStored[i] = quest;
                if (i == 0)
                {
                    quest.button.Select();
                }
            }

        isNeedRefresh = false;
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
                    if (spawnPatients.countBed == spawnPatients.postionObject.Length)
                    {
                        collectHerbalQuest(questStored[i], "Thảo dược");
                    }
                    else
                    {
                        //Debug.Log("count bed" + spawnPatients.countBed);
                        //Debug.Log("spawn patient");
                        spawnPatients.SpawnPrefabs();
                        savePeopleQuest(questStored[i], "Cứu người");
                    }
                }
                
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

        quest.Initialize("Chữa bệnh", quest.questCollected, quest.questColletion);
    }
}

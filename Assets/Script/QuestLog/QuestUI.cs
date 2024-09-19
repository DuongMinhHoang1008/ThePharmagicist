using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.ComponentModel;

public class QuestUI : MonoBehaviour
{
    [SerializeField] GameObject contentParent;
    [SerializeField] QuestLogScrollingList questList;
    [SerializeField] TextMeshProUGUI QuestName;
    [SerializeField] TextMeshProUGUI QuestDescribe;
    [SerializeField] TextMeshProUGUI QuestStatus;
    [SerializeField] TextMeshProUGUI QuestReward;
    [SerializeField] TextMeshProUGUI TimeRefreshQuest;
    [SerializeField] float timeToRefresh = 600;
    private float remainTime = 120;
    private int indexActiveLastest;

    public static QuestUI instance { get; set; }
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void Start() {
        remainTime = timeToRefresh;
    }
    void Update()
    {
        HideAndShowUI();
        SwitchQuest(instance.questList);
        remainTime -= Time.deltaTime;
        transferTime(remainTime);

        if (remainTime <= 0)
        {
            questList.isNeedRefresh = true;
            remainTime = timeToRefresh;
        }
    }

    private void HideAndShowUI()
    {
        if (contentParent.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            contentParent.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            contentParent.SetActive(true);
        }
    }

    private void ShowQuestInfo(QuestPrefab quest)
    {
        QuestName.text = quest.buttonText.text;
        if (quest.gameObject.name == "collectHerbal")
        {
            QuestName.fontSize = 15;
            QuestDescribe.text = "Mô tả \nThu thâp đủ thảo dược để hoàn hành nhiệm vụ";
        }
        else if (quest.gameObject.name == "savePeople")
        {
            QuestName.fontSize = 20;
            QuestDescribe.text = "Mô tả \nCứu người bị bệnh";
        }

        QuestStatus.text = "Tiến độ          " + quest.questCollected + " / " + quest.questColletion;
    }

    private void SwitchQuest(QuestLogScrollingList questList)
    {
        bool isSelected = false;
        if (contentParent.activeInHierarchy)
        {
            for (int i = 0; i < 5; i++)
            {
                if (questList.questStored[i] != null && questList.questStored[i].gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    ShowQuestInfo(questList.questStored[i]);
                    indexActiveLastest = i;
                    isSelected = true;
                }
            }
            if (!isSelected && questList.questStored[indexActiveLastest] != null)
            {
                questList.questStored[indexActiveLastest].button.Select();
            }
        }      
    }

    private void transferTime(float remainTime)
    {
        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);
        TimeRefreshQuest.text = "Làm mới:       " + string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}

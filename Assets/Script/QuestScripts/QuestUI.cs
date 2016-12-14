using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {

    public GameManager manager;
    public PlayerData player;

    // On Off 
    public GameObject storyboard;
    public GameObject questListboard;
    public GameObject clearboard;

    // Storyboard UI element
    public Image completeBtn;

    public Text storyboard_name;
    public Text storyboard_content;

    public Text questName;
    public Text haveGotMaterialNumber;
    public Text demand;

    public Text questEnd;

    // Quest Data
    public int id = 1;

    // from Data repository
    public int num; // Xml의 몇번째 자리부터 그 퀘스트가 존재하는지 뽑아올 공간
    public QuestData quest;
    public QuestData prevque;

    // Storyboard Data
    public string[] talkerName;
    public string[] content;
    public int length;

    public int page = 0; // 한 퀘스트 각각 페이지 체크

    public bool cleared;
    public bool progress;

    void Start()
    {
        Debug.Log("test Quest UI");
        QuestLoad();
    }

    void Update()
    {
        StoryPolicy();
    }

    public void LinkComponentElement()
    {
        manager = GameObject.Find("GameLogic").GetComponent<GameManager>();
        player = manager.GamePlayer;

        storyboard = transform.Find("QuestStoryboard").gameObject;
        questListboard = transform.Find("QeustListView").gameObject;
        clearboard = transform.Find("Clearboard").gameObject;

        completeBtn = questListboard.transform.Find("complete").GetComponent<Image>();

        storyboard_name = storyboard.transform.Find("story_name").GetComponent<Text>();
        storyboard_content = storyboard.transform.Find("story_sentence").GetComponent<Text>();

        questName = questListboard.transform.Find("questName").GetComponent<Text>();
        haveGotMaterialNumber = questListboard.transform.Find("haveNumber").GetComponent<Text>();
        demand = questListboard.transform.Find("demand").GetComponent<Text>();
        questEnd = clearboard.transform.Find("questEnd").GetComponent<Text>();
    }

    //Storyboard riffle
    public void StoryPolicy()
    {
        if (storyboard.activeSelf)
        {
            storyboard_name.text = talkerName[page];
            storyboard_content.text = content[page];

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                page++;
            }
            if (page == length)
            {
                storyboard_name.text = "";
                storyboard_content.text = "";
                page = 0;
                storyboard.SetActive(false);
                ClearTermsCheck();
            }
        }
    }

    public void QuestLoad()
    {
        num = DataManager.FindDictionryNumber(id);
        quest = DataManager.FindQuestDataByID(num);

        length = quest.Length;
        talkerName = new string[length];
        content = new string[length];

        int j = num + 1;

        for (int i = 0; i < length; i++)
        {
            talkerName[i] = quest.Name;
            content[i] = quest.Content;

            quest = DataManager.FindQuestDataByID(j);
            if (j < num + length - 1)
            {
                j++;
            }
        }

        quest = DataManager.FindQuestDataByID(num);
    }


    public void QuestStoryOpen()
    {
        if (id == 1)
        {
            storyboard.SetActive(true);
        }
        else
        {
            if (progress)
            {
                QuestLoad();
                storyboard.SetActive(true);
                progress = false;
            }
            else
            {
                questName.text = quest.Wait;

                haveGotMaterialNumber.text = player.SearchItemCount(DataManager.FindItemDataByID(quest.CLEARTERMS[0])).ToString();
                demand.text = quest.CLEARTERMS[1].ToString();

                if (!questListboard.activeSelf)
                {
                    questListboard.SetActive(true);
                }
                else
                {
                    questListboard.SetActive(false);
                }
            }
        }
    }

    public void ClearTermsCheck()
    {
        questEnd.text = quest.Wait;

        switch (quest.CLEARTYPE)
        {
            case 0:
                quest.Clear = true;
                clearboard.SetActive(true);
                break;
            case 1:
                StartCoroutine(ClearRealTimeCheckingPolicy());
                if (cleared)
                {
                    quest.Clear = true;    
                }
                break;
            case 2:

                break;
            case 3:

                break;
            default:
                Debug.Log("default");
                break;
        }

        if (quest.Clear)
        {
            prevque = quest;
        }
    }

    public void OpenTermsCheck()
    {
        switch (quest.OPENTYPE)
        {
            case 0:
                Debug.Log(" normal open 0 // 여기들어오면 분명히 오류 일 것..ㅎ");
                quest.Clear = true;

                break;
            case 1:
                Debug.Log(" terms 1");



                break;
            case 2:

                break;
            case 3:

                break;
            default:
                Debug.Log("default");
                break;
        }
    }

    IEnumerator ClearRealTimeCheckingPolicy()
    {
        while (quest.CLEARTYPE == 1)
        {
            int temp = player.SearchItemCount(DataManager.FindItemDataByID(quest.CLEARTERMS[0]));

            if (quest.CLEARTERMS[1] <= temp)
            {
                completeBtn.sprite = Resources.Load<Sprite>("Image/Quest/complete") as Sprite;
                cleared = true;
            }
            else
            {
                completeBtn.sprite = Resources.Load<Sprite>("Image/Quest/no_complete") as Sprite;
                cleared = false;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void ClearConfirmButton()
    {
        if (cleared)
        {
            ClearTermsCheck();
            if (!clearboard.activeSelf)
            {
                player.ItemSet[player.SearchItem(DataManager.FindItemDataByID(quest.CLEARTERMS[0]), quest.CLEARTERMS[1])].Count -= quest.CLEARTERMS[1];

                Debug.Log("경험치 추가, 보상아이템 추가");
                //player.AddItemData(quest.Rewards[0], quest.Rewards[1]);
                player.StoreData.PresentExperience += quest.Rewards[2];

                clearboard.SetActive(true);
            }
            if (questListboard.activeSelf)
            {
                questListboard.SetActive(false);
            }

            progress = true;
        }
        else
        {
            progress = false;
        }
    }

    public void ClearboardClose()
    {
        if (clearboard.activeSelf)
        {
            clearboard.SetActive(false);
            id++;
            progress = true;
        }
    }

    public void ListClose()
    {
        questListboard.SetActive(false);
    }
}

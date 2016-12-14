using UnityEngine;

public class QuestData
{
    [SerializeField] int id;
    [SerializeField] int length;
    [SerializeField] string name;
    [SerializeField] string content;
    [SerializeField] OpenType openType;
    [SerializeField] int[] openTerms;
    [SerializeField] ClearType clearType;
    [SerializeField] int[] clearTerms;
    [SerializeField] bool clear;
    [SerializeField] string wait;
    [SerializeField] int[] rewards;

    public int ID { get { return id; } set { id = value; } }
    public int Length { get { return length; } set { length = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string Content { get { return content; } set { content = value; } }
    public int OPENTYPE { get { return (int)openType; } }
    public int[] OPENTERMS { get { return openTerms; } }
    public int CLEARTYPE { get { return (int)clearType; } }
    public int[] CLEARTERMS { get { return clearTerms; } }
    public bool Clear { get { return clear; } set { clear = value; } }
    public string Wait { get { return wait; } }
    public int[] Rewards { get { return rewards; } }

    public enum OpenType : int
    {
        Defalut = 0,
        Precedence, //선행
        Level,
        Rank,
        GetItem
    };
    public enum ClearType : int
    {
        Defalut = 0,
        Delivery,
        Level,
        Rank
    };

    public QuestData()
    {
        id = 0;
        length = 0;
        name = null;
        content = null;
        openType = OpenType.Defalut;
        clearType = ClearType.Defalut;
    }

    public QuestData(
        int _id,
        string _name,
        string _content)
    {
        id = _id;
        name = _name;
        content = _content;
    }

    public QuestData(
        int _id,
        int _lenght,
        string _name,
        string _content,
        int _openType,
        string _openTerms,
        int _clearType,
        string _clearTerms,
        string _wait,
        string _reward
        )
    {
        id = _id;
        length = _lenght;
        name = _name;
        content = _content;
        FromIntToOpenType(_openType);
        OpenTerms(openType, _openTerms);
        FromIntToClearType(_clearType);
        ClearTerms(clearType, _clearTerms);
        clear = false;
        wait = _wait;
        StringSplit(ref rewards, _reward);
    }

    void FromIntToOpenType(int _type)
    {
        switch (_type)
        {
            case 0:
                openType = OpenType.Defalut;
                break;
            case 1:
                openType = OpenType.Precedence;
                break;
            case 2:
                openType = OpenType.Level;
                break;
            case 3:
                openType = OpenType.Rank;
                break;
            case 4:
                openType = OpenType.GetItem;
                break;
            default:
                openType = OpenType.Defalut;
                break;
        }
    }

    void OpenTerms(OpenType _type, string _terms)
    {
        switch (_type)
        {
            case OpenType.Defalut:
                openTerms = null;
                break;

            case OpenType.Precedence:
                openTerms = new int[1];
                openTerms[0] = int.Parse(_terms);
                break;

            case OpenType.Level:
                openTerms = new int[1];
                openTerms[0] = int.Parse(_terms);
                break;

            case OpenType.Rank:
                StringSplit(ref openTerms, _terms);
                break;

            case OpenType.GetItem:
                StringSplit(ref openTerms, _terms);
                break;
        }

    }

    void FromIntToClearType(int _type)
    {
        switch (_type)
        {
            case 0:
                clearType = ClearType.Defalut;
                break;
            case 1:
                clearType = ClearType.Delivery;
                break;
            case 2:
                clearType = ClearType.Level;
                break;
            case 3:
                clearType = ClearType.Rank;
                break;
            default:
                clearType = ClearType.Defalut;
                break;
        }
    }

    void ClearTerms(ClearType _type, string _terms)
    {
        switch (_type)
        {
            case ClearType.Defalut:
                clearTerms = null;
                break;
            case ClearType.Delivery:
                StringSplit(ref clearTerms, _terms);
                break;
            case ClearType.Level:
                openTerms = new int[1];
                openTerms[0] = int.Parse(_terms);
                break;
            case ClearType.Rank:
                openTerms = new int[1];
                openTerms[0] = int.Parse(_terms);
                break;
        }
    }

    void StringSplit(ref int[] arrangement, string str)
    {
        string[] result = str.Split(new char[] { ',' });
        arrangement = new int[result.Length];

        for (int i = 0; i < result.Length; i++)
        {
            arrangement[i] = int.Parse(result[i]);
        }
    }

}

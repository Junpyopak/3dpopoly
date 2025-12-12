using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public int Order;
    public string Speaker;
    public string Text;
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueLine> Lines = new List<DialogueLine>();
}

public class DialogueDatabase : MonoBehaviour
{
    public static DialogueDatabase Instance;

    private Dictionary<string, DialogueData> dialogues = new Dictionary<string, DialogueData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCSV();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Dialogue/dialogues");
        if (csvFile == null)
        {
            Debug.LogError("dialogues.csv 파일을 찾을 수 없음!");
            return;
        }

        var csvData = CSVReader.ReadCSV(csvFile);

        foreach (var row in csvData)
        {
            string id = row["DialogueID"];
            int order = int.Parse(row["Order"]);
            string speaker = row["Speaker"];
            string text = row["Text"];

            if (!dialogues.ContainsKey(id))
                dialogues[id] = new DialogueData();

            dialogues[id].Lines.Add(new DialogueLine
            {
                Order = order,
                Speaker = speaker,
                Text = text
            });
        }

        // 순서대로 정렬
        foreach (var d in dialogues.Values)
        {
            d.Lines.Sort((a, b) => a.Order.CompareTo(b.Order));
        }

        Debug.Log("대화 CSV 로딩 성공!");
    }

    public DialogueData GetDialogue(string id)
    {
        if (dialogues.ContainsKey(id))
            return dialogues[id];

        Debug.LogWarning($"DialogueID [{id}] 존재하지 않음!");
        return null;
    }
}


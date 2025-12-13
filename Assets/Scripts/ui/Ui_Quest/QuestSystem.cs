using UnityEngine;
using System.Collections.Generic;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance;

    // 현재 수락한 퀘스트 목록
    public List<int> acceptedQuests = new List<int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 퀘스트 수락
    public void AcceptQuest(int questID)
    {
        // 이미 받은 퀘스트면 무시
        if (acceptedQuests.Contains(questID))
        {
            Debug.Log($"이미 수락한 퀘스트: {questID}");
            return;
        }

        acceptedQuests.Add(questID);
        Debug.Log($"퀘스트 수락 완료: {questID}");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string DialogueID = "npc01_intro";
    public int QuestID = 1;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            QuestManager.Instance.StartDialogue(DialogueID, OnDialogueEnd);
        }
    }

    void OnDialogueEnd()
    {
        Debug.Log($"퀘스트 시작: {QuestID}");
        // 여기서 실제 퀘스트 로직 실행하면 됨
    }
}

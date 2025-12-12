using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public Text speakerText;
    public Text dialogueText;
    public GameObject dialoguePanel;

    private DialogueData currentDialogue;
    private int currentIndex = 0;
    private System.Action onDialogueFinish;

    private void Awake()
    {
        Instance = this;
    }

    // 대화 시작
    public void StartDialogue(string dialogueID, System.Action finishCallback = null)
    {
        currentDialogue = DialogueDatabase.Instance.GetDialogue(dialogueID);
        currentIndex = 0;
        onDialogueFinish = finishCallback;

        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    // 다음 문장 출력
    public void ShowNextLine()
    {
        if (currentDialogue == null || currentIndex >= currentDialogue.Lines.Count)
        {
            dialoguePanel.SetActive(false);
            onDialogueFinish?.Invoke();   // 대화 끝 콜백
            return;
        }

        var line = currentDialogue.Lines[currentIndex];
        speakerText.text = line.Speaker;
        dialogueText.text = line.Text;

        currentIndex++;
    }
}

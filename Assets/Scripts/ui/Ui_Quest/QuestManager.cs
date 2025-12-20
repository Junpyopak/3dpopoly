using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Canvas")]
    public GameObject questCanvas;      // Canvas 전체
    public GameObject dialoguePanel;    // 하단 말풍선
    public GameObject characterPanel;   // 캐릭터 일러스트 패널 (선택)

    [Header("Text UI (Legacy)")]
    public Text speakerText;
    public Text dialogueText;

    [Header("Typing")]
    public float typingSpeed = 0.04f;

    private DialogueData currentDialogue;
    private int currentIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;

    private System.Action onDialogueFinish;

    private int pendingQuestID = -1;

    public GameObject acceptButton;
    public GameObject rejectButton;

    public GameObject questBody;
    public Transform questsGroup;
    private void Awake()
    {
        // 싱글톤
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // 시작 시 전부 OFF
        if (questCanvas != null)
            questCanvas.SetActive(false);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (characterPanel != null)
            characterPanel.SetActive(false);

        acceptButton.SetActive(false);
        rejectButton.SetActive(false);
    }

    void Start()
    {
        Transform body = GameObject.Find("Body").transform;
        body.gameObject.SetActive(false);
    }
    /// <summary>
    /// NPC가 호출하는 대화 시작 함수
    /// </summary>
    public void StartDialogue(string dialogueID, System.Action finishCallback = null)
    {
        currentDialogue = DialogueDatabase.Instance.GetDialogue(dialogueID);
        if (currentDialogue == null)
        {
            Debug.LogError($"DialogueID not found: {dialogueID}");
            return;
        }

        currentIndex = 0;
        onDialogueFinish = finishCallback;

        // 대화 시작 시에만 Canvas ON
        questCanvas.SetActive(true);
        dialoguePanel.SetActive(true);
        if (characterPanel != null)
            characterPanel.SetActive(true);

        ShowNextLine();
    }

    /// <summary>
    /// 다음 대사 출력
    /// </summary>
    public void ShowNextLine()
    {
        // 타이핑 중이면 즉시 전체 출력
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue.Lines[currentIndex - 1].Text;
            isTyping = false;
            return;
        }

        // 대화 종료
        if (currentIndex >= currentDialogue.Lines.Count)
        {
            EndDialogue();
            return;
        }

        var line = currentDialogue.Lines[currentIndex];

        speakerText.text = line.Speaker;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(line.Text));
        currentIndex++;
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    /// <summary>
    /// 대화 종료 처리
    /// </summary>
    void EndDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialoguePanel.SetActive(true);
        if (characterPanel != null)
            characterPanel.SetActive(false);


        //수락/거절 버튼 활성화
        acceptButton.SetActive(true);
        rejectButton.SetActive(true);

        onDialogueFinish?.Invoke();
    }

    private void Update()
    {
        if (!questCanvas.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    void CloseAllUI()
    {
        dialoguePanel.SetActive(false);
        questCanvas.SetActive(false);
    }

    public void OnAcceptQuest()
    {
        QuestSystem.Instance.AcceptQuest(pendingQuestID);

        questBody.SetActive(true);   // ← 켜기!

        Transform quest = questsGroup.GetChild(0);

        Text questTitle = quest.Find("Quest Name Text").GetComponent<Text>();
        questTitle.text = "상인을 도와주자";

        Text objectiveText = quest.Find("Objectives/Objective (1)").GetComponentInChildren<Text>();
        objectiveText.text = "몬스터 3마리 처치";

        CloseAllUI();
    }

    public void OnRejectQuest()
    {
        CloseAllUI();
    }
}

using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyMgr : MonoBehaviourPunCallbacks
{
    [Header("�� & UI")]
    public string gameSceneName = "PartyLobby";
    public GameObject RoomButtonPrefab;
    public Transform RoomButtonParent;
    public GameObject PartyUi;

    private List<GameObject> roomButtons = new List<GameObject>();

    // ��Ƽ ���� ��ư Ŭ��
    public void OnCreatePartyButtonClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            Debug.LogWarning("�κ� �����ؾ߸� �� ���� ����");
            return;
        }

        // ���� �̸� ����
        string newRoomName = "Room_" + Random.Range(1, 9999);

        // PhotonNetwork.CreateRoom ȣ�� X!
        // UI ��ư�� ���� �� Ŭ�� �� ����
        CreateRoomButton(newRoomName);
        Debug.Log("�� ��Ƽ ��ư ����: " + newRoomName);
    }

    // ���� ��ư ����
    void CreateRoomButton(string roomName)
    {
        if (RoomButtonPrefab == null || RoomButtonParent == null)
        {
            Debug.LogWarning("RoomButtonPrefab �Ǵ� RoomButtonParent�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        GameObject newButton = Instantiate(RoomButtonPrefab, RoomButtonParent);

        Transform roomNameTransform = newButton.transform.Find("RoomNameText"); // �ڽ� ������Ʈ �̸�
        if (roomNameTransform != null)
        {
            TMP_Text roomNameText = roomNameTransform.GetComponent<TMP_Text>();
            if (roomNameText != null)
            {
                roomNameText.text = roomName;
            }
            else
            {
                Debug.LogWarning("RoomNameText�� TMP_Text�� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogWarning("RoomNameText �ڽ� ������Ʈ�� ã�� �� �����ϴ�!");
        }

        // ��ư Ŭ�� �̺�Ʈ ����
        Button btn = newButton.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                Debug.Log("�� ��ư Ŭ��: " + roomName);
                // ��ư Ŭ�� �ø� JoinOrCreateRoom ȣ��
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
            });
        }

        roomButtons.Add(newButton);
    }

    // �� ���� ���� ��
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� �Ϸ�: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(gameSceneName); // �� �̵�
        PartyUi.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("�� ���� ����: " + message);
    }
}

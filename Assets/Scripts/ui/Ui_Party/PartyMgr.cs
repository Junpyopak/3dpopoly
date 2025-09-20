using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyMgr : MonoBehaviourPunCallbacks
{
    [Header("씬 & UI")]
    public string gameSceneName = "PartyLobby";
    public GameObject RoomButtonPrefab;
    public Transform RoomButtonParent;
    public GameObject PartyUi;

    private List<GameObject> roomButtons = new List<GameObject>();

    // 파티 생성 버튼 클릭
    public void OnCreatePartyButtonClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            Debug.LogWarning("로비에 입장해야만 룸 생성 가능");
            return;
        }

        // 랜덤 이름 생성
        string newRoomName = "Room_" + Random.Range(1, 9999);

        // PhotonNetwork.CreateRoom 호출 X!
        // UI 버튼만 생성 → 클릭 시 입장
        CreateRoomButton(newRoomName);
        Debug.Log("새 파티 버튼 생성: " + newRoomName);
    }

    // 동적 버튼 생성
    void CreateRoomButton(string roomName)
    {
        if (RoomButtonPrefab == null || RoomButtonParent == null)
        {
            Debug.LogWarning("RoomButtonPrefab 또는 RoomButtonParent가 할당되지 않았습니다!");
            return;
        }

        GameObject newButton = Instantiate(RoomButtonPrefab, RoomButtonParent);

        Transform roomNameTransform = newButton.transform.Find("RoomNameText"); // 자식 오브젝트 이름
        if (roomNameTransform != null)
        {
            TMP_Text roomNameText = roomNameTransform.GetComponent<TMP_Text>();
            if (roomNameText != null)
            {
                roomNameText.text = roomName;
            }
            else
            {
                Debug.LogWarning("RoomNameText에 TMP_Text가 없습니다!");
            }
        }
        else
        {
            Debug.LogWarning("RoomNameText 자식 오브젝트를 찾을 수 없습니다!");
        }

        // 버튼 클릭 이벤트 연결
        Button btn = newButton.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                Debug.Log("룸 버튼 클릭: " + roomName);
                // 버튼 클릭 시만 JoinOrCreateRoom 호출
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
            });
        }

        roomButtons.Add(newButton);
    }

    // 룸 입장 성공 시
    public override void OnJoinedRoom()
    {
        Debug.Log("룸 입장 완료: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(gameSceneName); // 씬 이동
        PartyUi.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("룸 입장 실패: " + message);
    }
}

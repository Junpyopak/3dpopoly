using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

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

        // 버튼 텍스트 설정
        Text btnText = newButton.GetComponentInChildren<Text>();
        if (btnText != null) btnText.text = roomName;

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

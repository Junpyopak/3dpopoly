using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public GameObject PartCan; // 로비 UI 패널

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Photon 서버 연결 시도 중...");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버 접속 완료");
        // 로비 입장은 버튼 클릭 시만
    }

    // 로비 입장 버튼 클릭
    public void OnClickJoinLobby()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Photon 서버 연결이 아직 안됨");
            return;
        }

        if (PhotonNetwork.InLobby)
        {
            Debug.Log("이미 로비에 입장되어 있음");
            return;
        }

        PhotonNetwork.JoinLobby();
        Debug.Log("로비 입장 시도 중...");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 입장 완료!");
        if (PartCan != null) PartCan.SetActive(true);
    }
}

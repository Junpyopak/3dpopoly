using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public GameObject PartCan; // �κ� UI �г�

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Photon ���� ���� �õ� ��...");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ���� �Ϸ�");
        // �κ� ������ ��ư Ŭ�� �ø�
    }

    // �κ� ���� ��ư Ŭ��
    public void OnClickJoinLobby()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Photon ���� ������ ���� �ȵ�");
            return;
        }

        if (PhotonNetwork.InLobby)
        {
            Debug.Log("�̹� �κ� ����Ǿ� ����");
            return;
        }

        PhotonNetwork.JoinLobby();
        Debug.Log("�κ� ���� �õ� ��...");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� �Ϸ�!");
        if (PartCan != null) PartCan.SetActive(true);
    }
}

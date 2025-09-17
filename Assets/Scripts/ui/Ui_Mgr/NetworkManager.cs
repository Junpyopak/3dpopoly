using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string playerPrefabName = "Player";
    public string roomName = "TestRoom";
    public Vector3 baseSpawnPos = new Vector3(-58f, 16.4500008f, -294f);

    void Start()
    {
       
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            JoinRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();
        JoinRoom();
    }

    //public override void OnJoinedLobby()
    //{
    //    PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    //}
    public void JoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        //Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
        Vector3 spawnPos = new Vector3(
           Random.Range(baseSpawnPos.x - 2f, baseSpawnPos.x + 2f),
           baseSpawnPos.y,
           Random.Range(baseSpawnPos.z - 2f, baseSpawnPos.z + 2f)
       );
        PhotonNetwork.Instantiate(playerPrefabName, spawnPos, Quaternion.identity);
    }
}

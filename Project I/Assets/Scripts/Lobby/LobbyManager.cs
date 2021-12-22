using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : ServerManager
{
    #region RoomList Variable
    [SerializeField] Transform roomlistItem_TR;
    #endregion

    #region private variable
    private LobbyUI lobbyUI;
    #endregion

    #region public variable
    public bool isConnected = false;
    public RoomInfo roomInfo;
    public GameObject roomListItem;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        lobbyUI = GameObject.FindObjectOfType<LobbyUI>();
        PhotonNetwork.AutomaticallySyncScene = true;

        if(!PhotonNetwork.IsConnected)
        {
            ConnecteToServer();
            isConnected = true;
            Debug.Log("Lobby이동완료");
        }
        else
        {
            ConnecteToServer();
            isConnected = true;
            Debug.Log("Lobby 재연결 성공 / " + "isConnected state : " + PhotonNetwork.NetworkClientState);
        }
    }
    #endregion

    #region public Method
    public void CreateInit(float slider_value)
    {
        return;
    }

    public void Create(string roomName, string nickName, float playerCount)
    {
        string room_Name = roomName;
        string nick_Name = nickName;
        if(string.IsNullOrEmpty(roomName) || string.IsNullOrEmpty(nickName))
        {
            return;
        }

        if(string.IsNullOrEmpty(room_Name) || string.IsNullOrEmpty(nick_Name))
        {
            roomName = "";
            nickName = "";
        }
        else
        {
            roomName = "";
            nickName = "";

            Hashtable roomHT = new Hashtable();
            roomHT.Add("RoomName", room_Name);
            string[] roomList = new string[1];
            roomList[0] = "RoomName";

            RoomOptions roomOp = new RoomOptions();
            roomOp.MaxPlayers = (byte)playerCount;
            roomOp.CustomRoomProperties = roomHT;
            roomOp.CustomRoomPropertiesForLobby = roomList;
            PhotonNetwork.CreateRoom(room_Name, roomOp, null);

            Debug.Log(room_Name + "방 생성");
            AppManager.Instance.ChangeScene(AppManager.eSceneState.Room);
        }
    }

    public void QuickRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Pun Method
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속 완료");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        lobbyUI.quickfailed_UI();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform tr in roomlistItem_TR)
        {
            Destroy(tr.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            roomInfo = roomList[i];
            if (roomInfo.PlayerCount == 0 || roomInfo.MaxPlayers == 0)
            {
                continue;
            }
            Instantiate(roomListItem, roomlistItem_TR).GetComponent<RoomListInfo>().SetRoomInfo(roomList[i]);
        }
    }
    #endregion
}

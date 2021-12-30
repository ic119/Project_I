using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : ServerManager
{
    [SerializeField] RoomUI roomUI;

    #region public variable
    #endregion

    #region LifeCycle
    private void Awake()
    {
        roomUI = GameObject.FindObjectOfType<RoomUI>();
    }
    #endregion

    #region Pun Method
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "방 입장 성공");

        Hashtable roomInfoHT = PhotonNetwork.CurrentRoom.CustomProperties;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Room", "room" } });
            Hashtable info = PhotonNetwork.LocalPlayer.CustomProperties;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                PhotonNetwork.PlayerList[i].SetCustomProperties(new Hashtable { { "Room", "room" } });
            }
        }

        SetRoomInfo();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateRoomInfo();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoomInfo();
    }
    #endregion

    #region public Method
    #endregion

    #region private Method
    /// <summary>
    /// 방 생성 시 세팅되는 방정보
    /// </summary>
    private void SetRoomInfo()
    {
        roomUI.roomName_TEXT.text = string.Format("Room Name : " + PhotonNetwork.CurrentRoom.Name);
        roomUI.playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                     " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    private void UpdateRoomInfo()
    {
        roomUI.playerCount_TEXT.text = string.Format("Player : " + PhotonNetwork.CurrentRoom.PlayerCount +
                                                     " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;

public class RoomListInfo : UIManager
{
    #region Singleton
    private static RoomListInfo instance;
    public new static RoomListInfo Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new RoomListInfo();
            }
            return instance;
        }
    }
    #endregion

    [Header("RoomInfo UI")]
    [SerializeField] Text roomName_TEXT;
    [SerializeField] Text roomCount_TEXT;
    [SerializeField] Button join_BTN;

    #region public variable
    public RoomInfo roomInfo;
    #endregion

    #region private variable
    private LobbyUI lobbyUI;
    #endregion

    #region LifeCycle
    private new void Awake()
    {
        lobbyUI = GameObject.FindObjectOfType<LobbyUI>();
        instance = this;
    }

    private void Start()
    {
        join_BTN.onClick.AddListener(OnClick_JoinBTN);
    }
    #endregion

    #region public Method
    public void OnClick_JoinBTN()
    {
        lobbyUI.nickName_Container.SetActive(true);
    }

    /// <summary>
    /// 방에 대한 정보 세팅처리
    /// </summary>
    /// <param name="_roomInfo"></param>
    public void SetRoomInfo(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        roomName_TEXT.text = (string)roomInfo.CustomProperties["RoomName"];
        roomCount_TEXT.text = string.Format(roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers);
    }
    #endregion
}

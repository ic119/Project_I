using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Realtime;

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
        join_BTN.onClick.AddListener(Join);
    }
    #endregion

    #region public Method
    public override void Join()
    {

    }
    #endregion
}

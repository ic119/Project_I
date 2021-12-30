using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;

public class RoomUI : UIManager
{
    [Header("RoomInfo UI")]
    public Text roomName_TEXT;
    public Text playerCount_TEXT;
    [SerializeField] GameObject start_Container;
    [SerializeField] GameObject bg;

    [Header("Button UI")]
    [SerializeField] Button roomExit_BTN;
    [SerializeField] Button start_BTN;

    #region public variable
    public bool isClickedToStart = false;
    #endregion

    #region private variable
    private RoomManager roomManager;
    #endregion

    #region LifeCycle
    private new void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    private void Start()
    {
        CountDownController.Instance.Counting_Init();
        roomExit_BTN.onClick.AddListener(Exit);
        start_BTN.onClick.AddListener(Click_Start);
    }

    private void Update()
    {
        if (isClickedToStart == true)
        {
            CountDownController.Instance.StartCountDown();
        }
    }
    #endregion

    #region public Method
    public override void Exit()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "방 => 로비 이동");
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Lobby);
    }

    public override void Click_Start()
    {
        bg.SetActive(false);
        start_Container.SetActive(false);
        isClickedToStart = true;
    }
    #endregion
}

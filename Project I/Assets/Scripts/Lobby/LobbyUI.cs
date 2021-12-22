using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class LobbyUI : UIManager
{
    [Header("Lobby Button UI")]
    [SerializeField] Button createRoom_BTN;
    [SerializeField] Button quickRoom_BTN;
    [SerializeField] Button exit_BTN;

    [Header("Active Container UI")]
    [SerializeField] GameObject createRoom_Container;
    public GameObject nickName_Container;

    [Header("CreateRoom UI")]
    [SerializeField] InputField roomName_INPUT;
    [SerializeField] InputField nickName_INPUT;
    [SerializeField] Slider playerCount_SLIDER;
    [SerializeField] Text playerValue_TEXT;
    [SerializeField] Button room_create_BTN;
    [SerializeField] Button room_cancel_BTN;

    [Header("NickName UI")]
    [SerializeField] InputField name_INPUT;
    [SerializeField] Button name_create_BTN;
    [SerializeField] Button name_cancel_BTN;

    [Header("Server Link State UI")]
    [SerializeField] Text serverState_TEXT;

    [Header("Info UI")]
    [SerializeField] GameObject quickFail_UI;

    #region private variable
    private string serverLinked_TEXT = "Connected To Server";
    private LobbyManager lobbyManager;
    private RoomInfo _roominfo;
    #endregion

    #region LifeCycle
    private new void Awake()
    {
        lobbyManager = GameObject.FindObjectOfType<LobbyManager>();
        Init();
        Create_Init();
    }
    private void Start()
    {
        if (lobbyManager.isConnected == true)
        {
            serverState_TEXT.text = serverLinked_TEXT;
            createRoom_BTN.enabled = true;
            quickRoom_BTN.enabled = true;
            exit_BTN.enabled = true;
        }

        playerCount_SLIDER.onValueChanged.AddListener(delegate { SetPlayerCount(); });

        exit_BTN.onClick.AddListener(base.Exit);
        
        // CreateRoom 관련 버튼 이벤트
        createRoom_BTN.onClick.AddListener(CreateContainer_On);
        room_cancel_BTN.onClick.AddListener(CreateContainer_Off);
        room_create_BTN.onClick.AddListener(CreateRoom);

        // QuickRoom 관련 버튼 이벤트
        quickRoom_BTN.onClick.AddListener(QuickJoinRoom);

        // JoinRoom 관련 버튼 이벤트
        name_create_BTN.onClick.AddListener(JoinRoom);
        name_cancel_BTN.onClick.AddListener(CancelJoinRoom);
    }
    #endregion

    #region private Method
    /// <summary>
    /// 로비UI 버튼 및 컨테이너를 비활성화처리로 초기화
    /// </summary>
    private void Init()
    {
        createRoom_BTN.enabled = false;
        quickRoom_BTN.enabled = false;
        exit_BTN.enabled = false;
        createRoom_Container.SetActive(false);
        nickName_Container.SetActive(false);
    }

    private void Create_Init()
    {
        playerCount_SLIDER.value = 1;
        playerValue_TEXT.text = playerCount_SLIDER.value.ToString();
        roomName_INPUT.text = "";
        nickName_INPUT.text = "";
    }

    private void SetPlayerCount()
    {
        playerValue_TEXT.text = playerCount_SLIDER.value.ToString();
    }

    /// <summary>
    /// 각 UI의 입력값을 매개변수로 하여 LobbyManager의 Create()에 전달하여 방 생성
    /// </summary>
    private void CreateRoom()
    {
        lobbyManager.Create(roomName_INPUT.text, nickName_INPUT.text, playerCount_SLIDER.value);
        PhotonNetwork.NickName = nickName_INPUT.text;
        PlayerPrefs.SetString("UserName", PhotonNetwork.NickName);
    }

    private void JoinRoom()
    {
        _roominfo = lobbyManager.roomInfo;
        string room_name = (string)_roominfo.CustomProperties["RoomName"];
        PhotonNetwork.NickName = name_INPUT.text;
        PlayerPrefs.SetString("UserName", PhotonNetwork.NickName);
        PhotonNetwork.JoinRoom(room_name);
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Room);
        Debug.Log(room_name + "방 입장시도");
    }

    private void CancelJoinRoom()
    {
        name_INPUT.text = "";
        nickName_Container.SetActive(false);
    }
    private void CreateContainer_On()
    {
        createRoom_Container.SetActive(true);
    }

    private void CreateContainer_Off()
    {
        createRoom_Container.SetActive(false);
    }

    private void NickNameContainer_On()
    {
        nickName_Container.SetActive(true);
    }

    private void NickNameContainer_Off()
    {
        nickName_Container.SetActive(false);
    }

    private void QuickJoinRoom()
    {
        lobbyManager.QuickRoom();
    }
    private void QuickState_PopUp()
    {
        quickFail_UI.SetActive(false);
    }
    #endregion

    #region public Method
    public void quickfailed_UI()
    {
        quickFail_UI.SetActive(true);
        Invoke("QuickState_PopUp", 2.0f);
        Invoke("CreateContainer_On", 2.0f);
    }
    #endregion
}

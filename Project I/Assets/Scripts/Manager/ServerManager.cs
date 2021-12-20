using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    private static ServerManager instance;
    public static ServerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServerManager();
            }
            return instance;
        }
    }
    #endregion

    #region LifeCycle
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    #region public Method
    /// <summary>
    /// ���� ������ �ȵǰų� �ȵǾ����� ��� �����翬�� �õ��ϴ� �޼���
    /// </summary>
    public void ConnecteToServer()
    {
        PhotonNetwork.GameVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion
}

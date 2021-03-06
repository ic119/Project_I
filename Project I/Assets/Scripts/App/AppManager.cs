using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using Photon.Pun;


public class AppManager : Singleton<AppManager>
{
   public enum eSceneState
    {
        App,
        Loading,
        Lobby,
        Room
    }

    public eSceneState sceneState;

    #region LifeCycle
    private void Start()
    {
        var app = GameObject.FindObjectOfType<AppManager>();
        if(app.GetInstanceID() != this.GetInstanceID())
        {
            DestroyImmediate(app.gameObject);
        }
        DontDestroyOnLoad(app.gameObject);
        ChangeScene(eSceneState.Loading);

    }
    #endregion

    #region public Method
    public void ChangeScene(eSceneState sceneState)
    {
        switch(sceneState)
        {
            case eSceneState.App :
                {
                    SceneManager.LoadScene(eSceneState.App.ToString());
                }
                break;
            case eSceneState.Loading:
                {
                    SceneManager.LoadScene(eSceneState.Loading.ToString());
                    
                    if (PhotonNetwork.IsConnected)
                    {
                        SceneManager.LoadSceneAsync(eSceneState.Loading.ToString()).completed += (oper) =>
                        {
                            ChangeScene(eSceneState.Lobby);
                        };
                    }
                }
                break;
            case eSceneState.Lobby:
                {
                    if(PhotonNetwork.IsConnected)
                    {
                        PhotonNetwork.LoadLevel(eSceneState.Lobby.ToString());
                    } 
                    else
                    {
                        SceneManager.LoadScene(eSceneState.Lobby.ToString());
                    }
                }
                break;
            case eSceneState.Room:
                {   
                    if(PhotonNetwork.IsConnected) 
                    {
                        PhotonNetwork.LoadLevel(eSceneState.Room.ToString());
                    }
                    else
                    {
                        SceneManager.LoadScene(eSceneState.Room.ToString());
                    }
                }
                break;
        }
    }
    #endregion
}

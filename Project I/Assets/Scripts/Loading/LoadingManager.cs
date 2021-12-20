using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoadingManager : ServerManager
{
    #region LifeCycle
    private void Awake()
    {
        StartCoroutine(JoinCharacter());
    }
    #endregion

    IEnumerator JoinCharacter()
    {
        yield return new WaitForSeconds(1.0f);
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Character);
        Debug.Log("캐릭터 생성으로 이동합니다.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region public Method
    public virtual void Exit()
    {
        Application.Quit();
    }
    #endregion
}

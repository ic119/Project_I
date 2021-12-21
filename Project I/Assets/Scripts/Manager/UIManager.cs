using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region public Method
    public virtual void Click()
    {

    }

    public virtual void Create()
    {

    }

    public virtual void Join()
    {

    }

    public virtual void Quick()
    {

    }

    public virtual void Exit()
    {
        Application.Quit();
    }
    #endregion
}

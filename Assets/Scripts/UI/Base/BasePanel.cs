using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel :MonoBehaviour
{

   public virtual void Init()
    {       
        
    }


    /// <summary>
    /// 该panel新建
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// 该panel暂停，即不能操控
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 该panel继续，即回复操控
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 该panel退出，即关闭
    /// </summary>
    public virtual void OnExit()
    {

    }
        
}

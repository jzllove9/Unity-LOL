using UnityEngine;
using System.Collections;
using Protocol;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        switch (model.command)
        {
            case LoginProtocol.LOGIN_SERS:
                login(model.GetMessage<int>());
                break;
            case LoginProtocol.REG_SERS:
                reg(model.GetMessage<int>());
                break;
        }
    }

    /// <summary>
    /// 登录返回处理
    /// </summary>
    /// <param name="value"></param>
    private void login(int value)
    {
        SendMessage("OpenLoginBtn");
        switch (value)
        {
            case 0:
                //加载游戏场景
              //  Application.LoadLevel(1); 过时
                SceneManager.LoadScene(1);
                break;
            case -1:
                WarningManager.errors.Add(new WarningModel("账号不存在！"));
                break;
            case -2:
                WarningManager.errors.Add(new WarningModel("该账号已经在线！"));
                break;
            case -3:
                WarningManager.errors.Add(new WarningModel("密码错误！"));
                break;
            case -4:
                WarningManager.errors.Add(new WarningModel("输入不合法！"));
                break;

        }
    }

    /// <summary>
    /// 注册返回处理
    /// </summary>
    /// <param name="value"></param>
    private void reg(int value)
    {
        switch (value)
        {
            case 0:
                WarningManager.errors.Add(new WarningModel("注册成功"));
                break;
            case 1:
                WarningManager.errors.Add(new WarningModel("该账号已经存在！"));
                break;
            case 2:
                WarningManager.errors.Add(new WarningModel("账号不合法！"));
                break;
            case 3:
                WarningManager.errors.Add(new WarningModel("密码不合法！"));
                break;
        }

    }
}

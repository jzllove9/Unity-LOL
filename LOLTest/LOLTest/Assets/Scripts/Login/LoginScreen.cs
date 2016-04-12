using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Protocol.dto;

public class LoginScreen : MonoBehaviour
{
    #region 登录部分
    [SerializeField]
    private InputField accountInput;
    [SerializeField]
    private InputField passwordInput;
    #endregion
    [SerializeField]
    private Button loginBtn;
    [SerializeField]
    private GameObject regPanel;
    #region 注册部分
    [SerializeField]
    private InputField regAccountInput;
    [SerializeField]
    private InputField regPassWordInput1;
    [SerializeField]
    private InputField regPassWordInput2;
    [SerializeField]
    private Button regSureBtn;
    #endregion

    void Start()
    {
        NetIO io = NetIO.Instance;
    }

    public void LoginOnClick()
    {
        if (accountInput.text.Length == 0 || accountInput.text.Length > 6)
        {
            WarningManager.errors.Add(new WarningModel("账号不合法", delegate { print("回调测试"); }));
            return;
        }

        if (passwordInput.text.Length == 0 || passwordInput.text.Length > 6)
        {
            WarningManager.errors.Add(new WarningModel("密码不合法"));
            return;
        }

        //验证通过 申请登录
        //loginBtn.enabled = false;
        AccountInfoDTO dto = new AccountInfoDTO();
        dto.account = accountInput.text;
        dto.password = passwordInput.text;
        this.Write(GameProtocol.Protocol.TYPE_LOGIN, 0, Protocol.LoginProtocol.LOGIN_CREQ, dto);
        loginBtn.interactable = false;
    }

    public void OpenLoginBtn()
    {
        passwordInput.text = string.Empty;
        loginBtn.interactable = true;
    }

    public void RegOnClick()
    {
        regPanel.SetActive(true);
    }

    public void RegCloseClick()
    {
        regAccountInput.text = string.Empty;
        regPassWordInput1.text = string.Empty;
        regPassWordInput2.text = string.Empty;
        regPanel.SetActive(false);
    }

    public void RegSureRegClick()
    {
        if (regAccountInput.text.Length == 0 || regAccountInput.text.Length > 6)
        {
            WarningManager.errors.Add(new WarningModel("账号格式错误"));
            return;
        }
        if (regPassWordInput1.text.Length == 0 || regPassWordInput1.text.Length > 6)
        {
            WarningManager.errors.Add(new WarningModel("密码格式错误"));
            return;
        }
        if (!regPassWordInput1.text.Equals(regPassWordInput2.text))
        {
            WarningManager.errors.Add(new WarningModel("两次密码输入不一致"));
            return;
        }
        //验证通过 申请注册
        //关闭注册面板
        //加载等待logo
        AccountInfoDTO dto = new AccountInfoDTO();
        dto.account = regAccountInput.text;
        dto.password = regPassWordInput1.text;
        this.Write(GameProtocol.Protocol.TYPE_LOGIN, 0, Protocol.LoginProtocol.REG_CREQ, dto);
        RegCloseClick();
    }
}

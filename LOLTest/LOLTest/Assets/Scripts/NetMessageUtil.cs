using UnityEngine;
using System.Collections;

public class NetMessageUtil : MonoBehaviour
{
    IHandler login;
    // Use this for initialization
    void Start()
    {
        login = GetComponent<LoginHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        while (NetIO.Instance.messages.Count > 0)
        {
            SocketModel model = NetIO.Instance.messages[0];
            StartCoroutine("MessageReceive", model);
            NetIO.Instance.messages.RemoveAt(0);
        }
    }

    void MessageReceive(SocketModel model)
    {
        switch (model.type)
        {
            case GameProtocol.Protocol.TYPE_LOGIN:
                login.MessageReceive(model);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TCPClientController : MonoBehaviour
{
    public Text text;
    public InputField inputField;


    TcpClient client;
    NetworkStream ns;
    //StreamWriter 


    public void StartConnect()
    {
        string ip = "127.0.0.1";
        int port = 11000;

        try
        {
            PrintMsg("서버에 접속을 시도중입니다...");
            client = new TcpClient(ip, port);
            ns = client.GetStream();
            PrintMsg("접속을 성공했습니다. 해피채팅 ^ㅡ^");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message + " __From StartConnect()");
        }
    }

    public void StopConnect()
    {
        ns.Close();
        client.Close();        
    }


    public void PrintMsg(string msg)
    {
        text.text += "\n >> "+msg;
    }

    public void SendMsgToServer(string msg)
    {
        //메세지 문자열을 바이트배열로 바꿔서 넣어주고
        char[] msgCharArr = msg.ToCharArray();        
        byte[] msgBytes = Encoding.Default.GetBytes(msgCharArr, 0, msgCharArr.Length);
                

        ns.Write(msgBytes, 0, msgBytes.Length);
        ns.Flush();

    }




    #region 버튼 관리
    public void OnButtonSendClicked()
    {
        string msg = inputField.text;
        PrintMsg(msg);
        SendMsgToServer(msg);                
    }
    public void OnButtonAccessClicked()
    {
        StartConnect();
    }
    public void OnButtonDisconnectClicked()
    {
        StopConnect();
    }        
    #endregion
}

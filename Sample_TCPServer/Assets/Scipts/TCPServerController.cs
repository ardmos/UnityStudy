using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TCPServerController : MonoBehaviour
{
    public Text text;
    public InputField inputField;

    TcpListener listener;
    TcpClient client;
    NetworkStream ns;

    bool done = false;
    bool startServer = false;

    void StartServer()
    {
        try
        {
            PrintMsg("서버가 시작됩니다...");
            listener = new TcpListener(IPAddress.Any, 11000);
            listener.Start();
            PrintMsg("서버가 시작되었습니다.");
            //StartListening();
            //StartCoroutine(StartListening());
            //startServer = true;
            PrintMsg("접속을 기다립니다...");
            listener.BeginAcceptTcpClient(new System.AsyncCallback(StartListening), listener);
            //print("아직..");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void Update()
    {
        if (client != null)
        {
            if (client.Connected)
            {
                PrintMsg("클라이언트가 연결되었습니다.");
            }
            else
            {
                ns.Close();
                client.Close();
                PrintMsg("클라이언트 연결이 끊어졌습니다.");
            }
        }
        else print("client is null");
        
    }

    void StartListening(System.IAsyncResult asyncResult)
    {
        PrintMsg("요스!");
        try
        {
            TcpListener listener = (TcpListener)asyncResult.AsyncState;


            client = listener.EndAcceptTcpClient(asyncResult);

            
            ns = client.GetStream();
            PrintMsg("접속됐습니다!");

        }
        catch (System.Exception ex)
        {
            PrintMsg(ex.Message);
        }
    }

    public void PrintMsg(string msg)
    {
        text.text += "\n >> " + msg;
    }

    #region 본격적 대화
    void DoChat()
    {
        byte[] msgbytes = new byte[1024];
        int bytesRead = ns.Read(msgbytes, 0, msgbytes.Length);
        string clientmsg = Encoding.Default.GetString(msgbytes, 0, bytesRead);
        PrintMsg(clientmsg);
    }
    #endregion


    #region 버튼 관리
    public void OnButtonSendClicked()
    {

    }
    public void OnButtonStartServerClicked()
    {
        StartServer();
    }
    public void OnButtonStopServerClicked()
    {

    }
    #endregion
    
}

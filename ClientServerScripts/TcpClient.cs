using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GameLibrary;
using UI_scripts;
using Unity.Collections;
using UnityEngine;
using System.Net.Sockets;
using System.IO;

namespace ClientServerScripts
{
    public class TcpClient : MonoBehaviour
    {
        static int dataSize = 1024 * 1024;
        public int port = 80; // порт сервера
        public string address = "52.183.129.25"; // адрес сервера

        public bool socketAvailible = false;

        IPEndPoint ipPoint;
        Socket socket;
        private System.Net.Sockets.TcpClient tcp;
        private NetworkStream stream;
        private Thread recieveThread;


        private void Start()
        {
            Links.TcpClient = this;
            //new Thread(OpenSocket).Start();
//            new Thread(TcpStart).Start();
            TcpStart();
            recieveThread = new Thread(RecieveData);
            recieveThread.Start();
        }

        private void OpenSocket()
        {
            int count = 0;
            while (count < 1)
            {
                try
                {
                    ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(ipPoint);
                    socketAvailible = true;
                    Debug.Log("Socket opened");
                    return;
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    count++;
                }
            }

            SyncContext.RunOnUnityThread(Links.RequestController.ConnectionFailed);
        }

        private void TcpStart()
        {
            socketAvailible = true;
            tcp = new System.Net.Sockets.TcpClient();
            tcp.Connect(address, port);
            stream = tcp.GetStream();
        }

        private void CloseSocket()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            Debug.Log("Socket closed");
        }


        public void SendData(byte[] data)
        {
            Thread.Sleep(50);
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        //receive
        public void RecieveData()
        {
            while (true)
            {
                try
                { 
                        //if(!socketAvailible) continue;
                        byte[] data = new byte[dataSize]; // буфер для ответа 
                        int bytes = 0; // количество полученных байт
                        do
                        { 
                            bytes = stream.Read(data, bytes, data.Length - bytes);
                            Thread.Sleep(10);
                        } while (stream.DataAvailable);
                        

                        DataInfo dataInfo = DataInfo.Deserialize(data);
                        switch (dataInfo.type)
                        {
                            case DataInfo.DataType.RequestInfo:
                                break;
                            case DataInfo.DataType.MapInfo:
                                SyncContext.RunOnUnityThread(() => Links.RequestController.MapInfoReceieved(dataInfo));
                                break;
                            case DataInfo.DataType.PlayerInfo:
                                SyncContext.RunOnUnityThread(() =>
                                    Links.RequestController.ResponseInfoReceieved(dataInfo));
                                break;
                            case DataInfo.DataType.ResponseInfo:
                                SyncContext.RunOnUnityThread(() =>
                                    Links.RequestController.ResponseInfoReceieved(dataInfo));
                                break;
                        }
                    } 
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    Debug.Log(ex.StackTrace);
                }

                Thread.Sleep(10);
            }
        }

        private void OnApplicationQuit()
        {
            tcp.Close();
            stream.Close();
            recieveThread.Abort();
            CloseSocket();
        }
    }
}
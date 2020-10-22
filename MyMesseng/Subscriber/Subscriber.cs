using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Subscriber
{
     class Subscriber
     {
          private Socket _socket;
          private string _topic;

          public Subscriber(string topic)
          {
               _topic = topic;
               _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          }

          public void Connect(string ipAddress, int port)
          {
               _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectedCall, null);
               Console.WriteLine("Waiting ... ");
          }

          private void ConnectedCall(IAsyncResult asyncResult)
          {
               if (_socket.Connected)
               {
                    Console.WriteLine("Subscriber connected .");
                    Subscribe();
                    Receive();
               }
               else
               {
                    Console.WriteLine("Error: Subscriber not connected.");
               }
          }

          private void Subscribe()
          {
               var data = Encoding.UTF8.GetBytes("subscribe#" + _topic);
               Send(data);
          }

          private void Receive()
          {
               ConnectInfo connection = new ConnectInfo();
               connection.Socket = _socket;

               _socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCall, connection);
          }

          private void ReceiveCall(IAsyncResult asyncResult)
          {
               ConnectInfo connection = asyncResult.AsyncState as ConnectInfo;

               try
               {
                    SocketError response;
                    int buffSize = _socket.EndReceive(asyncResult, out response);

                    if(response == SocketError.Success)
                    {
                         byte[] payloadBytes = new byte[buffSize];
                         Array.Copy(connection.Data, payloadBytes, payloadBytes.Length);

                         Handler.Handle(payloadBytes);
                    }
               
               }
               catch (Exception e)
               {
                    Console.WriteLine($"Can't receive data from broker. {e.Message}");
               }
               finally
               {
                    try
                    {
                         connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCall, connection);
                    }
                    catch(Exception e)
                    {
                         Console.WriteLine($"{e.Message}");
                         connection.Socket.Close();
                    }
               }
          }

          private void Send(byte[] data)
          {
               try
               {
                    _socket.Send(data);
               }
               catch(Exception e) {
                    Console.WriteLine($"Could not send data: {e.Message}");
               }
          }
     }
}

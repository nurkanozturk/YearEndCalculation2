using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace YearEndCalculation.Business.Tools
{
    public class OnlineTime
    {
        public static DateTime GetOnlineTime() {
            string ntpServer = "time.google.com";
            byte[] ntpData = new byte[48];
            ntpData[0] = 0x1B; //NTP versiyonu ve modu

            IPAddress[] addresses = Dns.GetHostEntry(ntpServer).AddressList;
            IPEndPoint endPoint = new IPEndPoint(addresses[0], 123);

            using (var client = new UdpClient())
            {
                client.Client.ReceiveTimeout = 200;
                client.Connect(endPoint);
                client.Send(ntpData, ntpData.Length);
                ntpData = client.Receive(ref endPoint);
            }

            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];
            ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            DateTime networkDT = new DateTime(1900, 1, 1, 3, 0, 0).AddMilliseconds((long)milliseconds);
            return networkDT;
            ////default Windows time server
            //const string ntpServer = "time.windows.com";

            //// NTP message size - 16 bytes of the digest (RFC 2030)
            //var ntpData = new byte[48];

            ////Setting the Leap Indicator, Version Number and Mode values
            //ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            //var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            ////The UDP port number assigned to NTP is 123
            //var ipEndPoint = new IPEndPoint(addresses[0], 123);
            ////NTP uses UDP

            //using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            //{
            //    socket.Connect(ipEndPoint);

            //    //Stops code hang if NTP is blocked
            //    socket.ReceiveTimeout = 3000;

            //    socket.Send(ntpData);
            //    socket.Receive(ntpData);
            //    socket.Close();
            //}

            ////Offset to get to the "Transmit Timestamp" field (time at which the reply 
            ////departed the server for the client, in 64-bit timestamp format."
            //const byte serverReplyTime = 40;

            ////Get the seconds part
            //ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            ////Get the seconds fraction
            //ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            ////Convert From big-endian to little-endian
            //intPart = SwapEndianness(intPart);
            //fractPart = SwapEndianness(fractPart);

            //var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            ////**UTC** time
            //var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            //return networkDateTime.ToLocalTime();
        }

        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Windows.Forms;

namespace IPFS_v2
{
    internal class SharedConnection
    {
        public static TcpClient tcpClient = new TcpClient();
        public static SslStream sslStream;
        public static byte[] buffer = new byte[4096];
        public static void establishConnection(string addr, int port)
        {
            try
            {
                tcpClient.Connect(addr, port);
                sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                sslStream.AuthenticateAsClient(addr);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public static void sendData(string data)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                sslStream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public static string receiveData()
        {
            try
            {
                int bytesRead = sslStream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            return "smth is wrong";
        }

        private static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
                {
                    // Customize the certificate validation logic here
                    // You can check the certificate's properties or compare it with a known certificate

                    if (sslPolicyErrors == SslPolicyErrors.None)
                        return true;

                    Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
                    return false;
                }
        }
    
}

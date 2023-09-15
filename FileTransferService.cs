using Renci.SshNet;

namespace DemoSFTP
{
    public class FileTransferService
    {
        public bool SendFileToSFTP()
        {
            try {

                // Ruta local del archivo a enviar
                string localFilePath = "fileTransfer/textfile.txt";
                // Ruta remota server SFTP
                string remoteDirectory = "/";

            
                string sftpServer = "192.168.1.21";
                string username = "tester";
                
                int port = 22; // Puerto SFTP

                //Conexión usando ssh private key file y passphrase(no required)
                var privateKeyFile = new PrivateKeyFile("privateKey/private-key");
                var connectionInfo = new ConnectionInfo(sftpServer, port, username, new PrivateKeyAuthenticationMethod(username, privateKeyFile));

                //Conexión con password Auth
                //string password = "password";
                //var connectionInfo = new ConnectionInfo(sftpServer, port, username, new PasswordAuthenticationMethod(username, password));

                using (var client = new SftpClient(connectionInfo))
                {
                    client.Connect();

                    // Verifica si el archivo local existe antes de intentar enviarlo
                    if (File.Exists(localFilePath))
                    {
                        // Abre el archivo local para lectura
                        using (var localStream = new FileStream(localFilePath, FileMode.Open))
                        {
                            // Sube el archivo al servidor SFTP
                            client.UploadFile(localStream, Path.Combine(remoteDirectory, Path.GetFileName(localFilePath)));
                        }

                        // Elimina el archivo local después de la transferencia exitosa
                        File.Delete(localFilePath);
                    }

                    client.Disconnect();
                }

                Console.WriteLine("Envío de archivo por SFTP ejecutado con éxito.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
        }
    }
}

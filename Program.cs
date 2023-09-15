using DemoSFTP;

Console.WriteLine("-------DEMO SEND FILE SFTP-------");
FileTransferService fileTransferService = new FileTransferService();

await Task.Delay(TimeSpan.FromSeconds(5));
fileTransferService.SendFileToSFTP();

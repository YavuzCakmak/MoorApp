using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NCrontab;

namespace Moor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackUpsController : ControllerBase
    {
        public BackUpsController()
        {
            // Crontab ifadesini ayarlayın (örneğin, her sabah saat 4'te çalışacak şekilde ayarlanmıştır)
            var schedule = CrontabSchedule.Parse("*/1 * * * *");
            //var schedule = NCrontab.CrontabSchedule.Parse("0 4 * * *");

            // Yeniden başlatma durumunda programın doğru çalışmasını sağlamak için sonraki yedeklemeleri planlı hale getirin
            var nextRun = schedule.GetNextOccurrence(DateTime.Now);
            var timer = new System.Threading.Timer(_ =>
            {
                BackupDatabase();
                nextRun = schedule.GetNextOccurrence(DateTime.Now);
            }, null, nextRun - DateTime.Now, TimeSpan.FromMinutes(1));
        }

        [HttpGet("BackUp")]
        public void BackUp()
        {
            BackupDatabase();
        }

        static void BackupDatabase()
        {

            //"ConnectionStrings": { "SqlConnection": "Server=127.0.0.1;Port=3306;Database=moor;Uid=Admin;Pwd=3X1Dkbp9#0UaKc4Ee9pY;" },
            string server = "127.0.0.1";
            string database = "moor";
            string user = "root";
            string password = "MpuH8QHcFniDNQv";

            string backupDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
            string backupFileName = "VeritabaniYedekleri-" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);

            // Eğer yedekleme dizini yoksa oluşturun
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            // mysqldump komutunu oluştur
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe";
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = true;
            psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", user, password, server, database);
            psi.UseShellExecute = false;

            // mysqldump komutunu çalıştır
            Process process = Process.Start(psi);

            // yedek dosyasını oluştur
            using (StreamWriter output = new StreamWriter(backupFilePath))
            {
                output.Write(process.StandardOutput.ReadToEnd());
            }

            process.WaitForExit();

            Console.WriteLine($"Database backup created at '{backupFilePath}'.");

            // Veritabanı yedeğini göndermek istediğiniz e-posta adresleri
            string fromEmail = "anka_yedek@hotmail.com";
            string toEmail = "musaozmen58@icloud.com";

            // E-posta konusu ve içeriği
            string emailSubject = $"Günlük Veritabanı Yedeği -- {DateTime.Now.Date}";
            string emailBody = "Merhaba,\n\nGünlük veritabanı yedeği ektedir.";

            // E-posta mesajını oluştur
            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("Backup Service", fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = emailSubject;

            var builder = new MimeKit.BodyBuilder();
            builder.TextBody = emailBody;
            builder.Attachments.Add(backupFilePath);

            message.Body = builder.ToMessageBody();

            // E-posta gönderme işlemini başlat
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(fromEmail, "morey1010");
                client.Send(message);
                client.Disconnect(true);
            }

            // Yedek dosyasını sil
            System.IO.File.Delete(backupFilePath);
        }
    }
}

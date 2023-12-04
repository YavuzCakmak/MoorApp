using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using NCrontab;

namespace Moor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackUpsController : ControllerBase
    {
        public BackUpsController()
        {
            var schedule = CrontabSchedule.Parse("0 4 * * *");

            var nextRun = schedule.GetNextOccurrence(DateTime.Now);
            var timer = new System.Threading.Timer(_ =>
            {
                BackupDatabase();
                nextRun = schedule.GetNextOccurrence(DateTime.Now);
            }, null, nextRun - DateTime.Now, TimeSpan.FromDays(1));
        }

        [HttpGet("BackUp")]
        public void BackUp()
        {
            BackupDatabase();
        }

        static async void BackupDatabase()
        {

            string server = "127.0.0.1";
            string database = "moordb";
            string user = "root";
            string password = "123456";

            string backupDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
            string backupFileName = "VeritabaniYedekleri-" + DateTime.Now.ToString("yyyy-MM-dd") + ".sql";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);

            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            string constring = $"server={server};user={user};pwd={password};database={database};";
            string file = backupFilePath;
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        conn.Close();
                    }
                }
            }

            Console.WriteLine($"Database backup created at '{backupFilePath}'.");

            string fromEmail = "anka_yedek@hotmail.com";
            string toEmail = "musaozmen58@icloud.com";

            string emailSubject = $"Günlük Veritabanı Yedeği -- {DateTime.Now.Date}";
            string emailBody = "Merhaba,\n\nGünlük veritabanı yedeği ektedir.";

            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("Backup Service", fromEmail));
            message.To.Add(MimeKit.MailboxAddress.Parse(toEmail));
            message.Subject = emailSubject;

            var builder = new MimeKit.BodyBuilder();
            builder.TextBody = emailBody;
            builder.Attachments.Add(backupFilePath);

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(fromEmail, "morey1010");
                client.Send(message);
                client.Disconnect(true);
            }

            System.IO.File.Delete(backupFilePath);
        }
    }
}

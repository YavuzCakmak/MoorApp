using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
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
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7019/api/Settings/TakeBackUp");
        }
    }
}

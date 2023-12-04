using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.MoorService;
using Moor.Model.Dtos.MoorDto.ReportDto;

namespace Moor.Service.Services.MoorService
{
    public class MailService : IMailService
    {
        private readonly ITransferService _transferService;
        private readonly IDistrictService _districtService;
        private readonly ITravellerService _travellerService;
        public MailService(IDistrictService districtService, ITravellerService travellerService, ITransferService transferService)
        {
            _districtService = districtService;
            _travellerService = travellerService;
            _transferService = transferService;
        }

        public Task SendTransferMail(long transferId)
        {
            TransferEntity transferEntity = _transferService.Where(x => x.Id == transferId).FirstOrDefault();
            if (transferEntity != null)
            {

                #region Mail
                string fromEmail = "anka_yedek@hotmail.com";
                string toEmail = "operation@ankavip.com.tr";

                string emailSubject = $"Yeni Transfer Kaydı -- {DateTime.Now.Date}";


                var message = new MimeKit.MimeMessage();
                message.From.Add(new MimeKit.MailboxAddress("Transfer", fromEmail));
                message.To.Add(MimeKit.MailboxAddress.Parse(toEmail));
                message.Subject = emailSubject;
                #endregion

                #region MailBody

                var Nereden = transferEntity.DirectionType == 1 ? transferEntity.Location : (transferEntity.District != null && !string.IsNullOrWhiteSpace(transferEntity.District.Name) ? transferEntity.District.Name : _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name);
                var Nereye = transferEntity.DirectionType == 2 ? transferEntity.Location : (transferEntity.District != null && !string.IsNullOrWhiteSpace(transferEntity.District.Name) ? transferEntity.District.Name : _districtService.Where(x => x.Id == transferEntity.DisctrictId).FirstOrDefault().Name);



                var arabaModel = transferEntity.CarParameter != null && transferEntity.CarParameter.CarModel != null && transferEntity.CarParameter.CarModel.Model != null ? transferEntity.CarParameter.CarModel.Model : "Araç Bilgisi Bulunamadı.";

                string yolcuListesi = string.Empty;
                var yolcular = _travellerService.Where(x => x.TransferId == transferEntity.Id).ToList();
                if (yolcular != null && yolcular.Count > 0)
                {

                    yolcuListesi = string.Join(", ", yolcular.Select(a => $"{a.FirstName} - {a.LastName}"));
                }

                string emailBody = $"Nereden {Nereden} " +
                    $"- Nereye {Nereye} " +
                    $"- Araba Model {arabaModel}" +
                    $"- Uçuş Kodu {transferEntity.FlightCode}" +
                    $"- Transfer Tarihi {transferEntity.CreatedDate}" +
                    $"- Açıklama {transferEntity.Explanation}" +
                    $"- Yolcular {yolcuListesi}";
                #endregion


                var builder = new MimeKit.BodyBuilder();
                builder.TextBody = emailBody;

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
            }
            return Task.CompletedTask;
        }
    }
}

using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Models.MoorModels.NotificationModel.NotificationPostModel;
using Moor.Model.Models.MoorModels.NotificationModel.NotificationReadModel;
using Moor.Model.Utilities;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class NotificationService : Service<NotificationEntity>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(IGenericRepository<NotificationEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, INotificationRepository notificationRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public override Task<IEnumerable<NotificationEntity>> GetAllAsync(DataFilterModel dataFilterModel)
        {
            return base.GetAllAsync(dataFilterModel);
        }

        public async Task<DataResult> CreateNotification(NotificationPostModel notificationPostModel)
        {
            #region Objects 
            DataResult dataResult = new DataResult();
            NotificationEntity notificationEntity = new NotificationEntity();
            #endregion

            if (notificationPostModel.IsFirst)
            {
                notificationEntity.Explanation = $"{notificationPostModel.AgencyName} - {notificationPostModel.TransferId}'Nolu transfer oluşturuldu.";
                notificationEntity.Status = 0;
                notificationEntity.CreatedDate = DateTime.Now;
                notificationEntity.IsRead = false;
                notificationEntity.IsFirst = true;
                notificationEntity.AgencyId = notificationPostModel.AgencyId;
                notificationEntity.TransferId = notificationPostModel.TransferId;
                await base.AddAsync(notificationEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            if (notificationPostModel.IsDriver)
            {
                notificationEntity.Explanation = $"{notificationPostModel.AgencyName} - {notificationPostModel.TransferId}'Nolu transfere şoför ataması gerçekleşti.";
                notificationEntity.Status = 0;
                notificationEntity.CreatedDate = DateTime.Now;
                notificationEntity.IsRead = false;
                notificationEntity.AgencyId = notificationPostModel.AgencyId;
                notificationEntity.TransferId = notificationPostModel.TransferId;
                await base.AddAsync(notificationEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            if (notificationPostModel.IsDriverChange)
            {
                notificationEntity.Explanation = $"{notificationPostModel.AgencyName} - {notificationPostModel.TransferId}'Nolu transferin şoförü değiştirildi.";
                notificationEntity.Status = 0;
                notificationEntity.CreatedDate = DateTime.Now;
                notificationEntity.IsRead = false;
                notificationEntity.AgencyId = notificationPostModel.AgencyId;
                notificationEntity.TransferId = notificationPostModel.TransferId;
                await base.AddAsync(notificationEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            if (notificationPostModel.IsAmount)
            {
                notificationPostModel.Explanation = $"{notificationPostModel.AgencyName} - {notificationPostModel.TransferId}'Nolu transferin ücreti değiştirildi.";
                notificationEntity.Status = 0;
                notificationEntity.CreatedDate = DateTime.Now;
                notificationEntity.IsRead = false;
                notificationEntity.AgencyId = notificationPostModel.AgencyId;
                notificationEntity.TransferId = notificationPostModel.TransferId;
                await base.AddAsync(notificationEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            if (notificationPostModel.IsStatus)
            {
                notificationPostModel.Explanation = $"{notificationPostModel.AgencyName} - {notificationPostModel.TransferId}'Nolu transferin statüsü güncellendi.";
                notificationEntity.Status = 0;
                notificationEntity.CreatedDate = DateTime.Now;
                notificationEntity.IsRead = false;
                notificationEntity.AgencyId = notificationPostModel.AgencyId;
                notificationEntity.TransferId = notificationPostModel.TransferId;
                await base.AddAsync(notificationEntity);
                dataResult.IsSuccess = true;
                return dataResult;
            }
            return dataResult;
        }

        public async Task<DataResult> Read(NotificationReadModel notificationReadModel)
        {
            DataResult dataResult = new DataResult();
            dataResult.IsSuccess = true;
            var notificationEnties = base.Where(x => x.AgencyId == notificationReadModel.AgencyId && x.IsRead == false && x.IsFirst == false && notificationReadModel.NotificationsIds.Contains(x.Id)).ToList();
            if (notificationEnties.IsNotNullOrEmpty())
            {
                foreach (var notificationEntity in notificationEnties)
                {
                    notificationEntity.IsRead = true;
                    await base.UpdateAsync(notificationEntity);
                }
            }
            return dataResult;
        }
    }
}

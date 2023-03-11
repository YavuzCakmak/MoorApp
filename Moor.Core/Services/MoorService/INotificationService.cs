using Moor.Core.Entities.MoorEntities;
using Moor.Core.Services.BaseService;
using Moor.Model.Models.MoorModels.NotificationModel.NotificationPostModel;
using Moor.Model.Utilities;

namespace Moor.Core.Services.MoorService
{
    public interface INotificationService : IService<NotificationEntity>
    {
        public Task<DataResult> CreateNotification(NotificationPostModel notificationPostModel);
    }
}

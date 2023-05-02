using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moor.API.Controllers.BaseController;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Extension.String;
using Moor.Core.Services.MoorService;
using Moor.Core.Utilities.DataFilter;
using Moor.Model.Models.MoorModels.DoPaymentModel;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;

namespace Moor.API.Controllers
{
    public class WalletsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IWalletService _walletService;
        private readonly IPaymentDescriptionService _paymentDescriptionService;

        public WalletsController(IMapper mapper, IWalletService walletService, IPaymentDescriptionService paymentDescriptionService)
        {
            _mapper = mapper;
            _walletService = walletService;
            _paymentDescriptionService = paymentDescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] DataFilterModel dataFilterModel)
        {
            var walletEntities = await _walletService.GetAllAsync(dataFilterModel);
            return CreateActionResult(CustomResponseDto<List<WalletEntity>>.Succces((int)HttpStatusCode.OK, walletEntities.ToList()));
        }

        [HttpPost("DoPayment")]
        public async Task<IActionResult> DoPayment(DoPaymentModel doPaymentModel)
        {
            if (doPaymentModel.AgencyId.IsNotNull())
            {
                var walletEntity = _walletService.Where(x => x.AgencyId == doPaymentModel.AgencyId).FirstOrDefault();
                if (walletEntity.IsNotNull())
                {
                    walletEntity.TotalAmount = walletEntity.TotalAmount + doPaymentModel.Amount;
                    await _walletService.UpdateAsync(walletEntity);
                }
                else
                {
                    WalletEntity wallet = new WalletEntity();
                    wallet.AgencyId = doPaymentModel.AgencyId;
                    wallet.TotalAmount = doPaymentModel.Amount;
                    wallet.IsDeleted = false;
                    wallet.CreatedDate = DateTime.Now;
                    await _walletService.AddAsync(wallet);
                }
                PaymentDescriptionEntity paymentDescriptionEntity = new PaymentDescriptionEntity();
                paymentDescriptionEntity.AgencyId = doPaymentModel.AgencyId;
                paymentDescriptionEntity.PaymentDate = doPaymentModel.PaymentDate;
                paymentDescriptionEntity.Description = doPaymentModel.Description;
                paymentDescriptionEntity.IsDeleted = false;
                paymentDescriptionEntity.CreatedDate = DateTime.Now;
                await _paymentDescriptionService.AddAsync(paymentDescriptionEntity);
            }
            else
            {

                var walletEntity = _walletService.Where(x => x.DriverId == doPaymentModel.DriverId).FirstOrDefault();
                if (walletEntity.IsNotNull())
                {
                    walletEntity.TotalAmount = walletEntity.TotalAmount + doPaymentModel.Amount;
                    await _walletService.UpdateAsync(walletEntity);
                }
                else
                {
                    WalletEntity wallet = new WalletEntity();
                    wallet.DriverId = doPaymentModel.DriverId;
                    wallet.TotalAmount = doPaymentModel.Amount;
                    wallet.IsDeleted = false;
                    wallet.CreatedDate = DateTime.Now;
                    await _walletService.AddAsync(wallet);
                }
                PaymentDescriptionEntity paymentDescriptionEntity = new PaymentDescriptionEntity();
                paymentDescriptionEntity.DriverId = doPaymentModel.DriverId;
                paymentDescriptionEntity.PaymentDate = doPaymentModel.PaymentDate;
                paymentDescriptionEntity.Description = doPaymentModel.Description;
                paymentDescriptionEntity.IsDeleted = false;
                paymentDescriptionEntity.CreatedDate = DateTime.Now;
                await _paymentDescriptionService.AddAsync(paymentDescriptionEntity);
            }

            return CreateActionResult(CustomResponseDto<NoContentDto>.Succces(200));
        }
    }
}

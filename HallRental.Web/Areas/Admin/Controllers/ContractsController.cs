
namespace HallRental.Web.Areas.Admin.Controllers
{
    using HallRental.Data;
    using HallRental.Services.Admin;
    using HallRental.Web.Infrastructure;
    using HallRental.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class ContractsController : Controller
    {
        private readonly IContractsService contractsService;

        public ContractsController(IContractsService contractsService)
        {
            this.contractsService = contractsService;
        }

        public IActionResult Index()
        {

            var allUploadedContracts = this.contractsService.GetAllContracts();

            return View(allUploadedContracts);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContract(IFormFile contract)
        {
            if (!contract.FileName.EndsWith(".pdf")
                || contract.Length > DataConstants.ContractSubmissionFileLength)
            {
                TempData.AddErrorMessage("Your file should be a '.pdf' file with no more than 3 MB size");
                return RedirectToAction(nameof(Index));

            }

            //byte array
            var contractSubmission = await contract.ToByteArrayAsync();

            DateTime currentDate = DateTime.UtcNow;

            string fileName = contract.FileName;

            bool success = await this.contractsService.SaveContractSubmission(contractSubmission, currentDate, fileName);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Contract submission saved successfully");
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DownloadContract(int id)
        {
            bool contractExists =  this.contractsService.ContractExists(id);

            if (!contractExists)
            {
                TempData.AddErrorMessage("Contract does not exists");
                return RedirectToAction(nameof(Index));
            }

            var contractSubmission = await this.contractsService.GetContractSubmission(id);

            if (contractSubmission == null)
            {
                return BadRequest();
            }

          //  Response.Headers.Add("content-disposition", "attachment;, filename="  + $"Contract{id}");
            Response.Headers.Add("content-disposition", "attachment;");

            return File(contractSubmission, "application/pdf");
        }

        public IActionResult DeleteContract(int id)
        {
            bool contractExists = this.contractsService.ContractExists(id);

            if (!contractExists)
            {
                TempData.AddErrorMessage("Contract does not exists");
                return RedirectToAction(nameof(Index));
            }

            bool isContactDeleted = this.contractsService.DeleteContract(id);

            if (!isContactDeleted)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Contract deleted successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
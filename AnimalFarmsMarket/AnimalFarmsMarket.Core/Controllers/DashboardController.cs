using AnimalFarmsMarket.Commons;
using AnimalFarmsMarket.Data.DTOs;
using AnimalFarmsMarket.Data.Models;
using AnimalFarmsMarket.Data.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using AnimalFarmsMarket.Data.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalFarmsMarket.Core.Controllers
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController : Controller
    {
        private UserManager<AppUser> _userManager;

        private readonly IMapper _mapper;


        public DashboardController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User);

            var userIsActive = user.Result.IsActive;
            if (!userIsActive)
            {
                return RedirectToAction("Profile", new { q = "update-profile" });
            }

            return View();
        }

        [HttpGet]
        public IActionResult TrackOrders()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string q)
        {
            ViewBag.Q = q;
            var id = HttpContext.Session.GetString("UserId");
            var token = HttpContext.Session.GetString("Token");
            var path = $"/api/v1/User/get-user/{id}";
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var response = await HttpHelper.GetContentWithTokenAsync<string>(baseUrl, "", token, path);
            var content = response.Item1;
            var httpContent = response.Item2;
            if (httpContent.IsSuccessStatusCode)
            {
                var successResponse = JsonConvert.DeserializeObject<ResponseDto<UserToReturnDto>>(content);
                var profileFound = successResponse.Data;

                var res = _mapper.Map<UpdateProfileViewModel>(profileFound);
                res.Phone = profileFound.PhoneNumber;
                res.DeliveryAddress = new AddressViewModel();
                res.DeliveryAddress.Street = profileFound.Street;
                res.DeliveryAddress.City = profileFound.City;
                res.DeliveryAddress.State = (States)Enum.Parse(typeof(States), profileFound.State);

                var resp = new ProfileViewModel();
                resp.UpdateProfileViewModel = res;

                return View(resp);
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Profile([FromQuery] string q, UpdateProfileViewModel model)
        {
            ViewBag.Q = "update-profile";
            var res = new ProfileViewModel();
            res.UpdateProfileViewModel = model;

            if (!ModelState.IsValid)
            {
                ViewBag.res = "notsaved";
                return View(res);
            }
            else
            {
                var id = HttpContext.Session.GetString("UserId");
                var userToUpdate = new UpdateUserDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone,
                    ZipCode = model.Zipcode,
                    Address = new Address()
                    {
                        UserId = id,
                        City = model.DeliveryAddress.City,
                        Street = model.DeliveryAddress.Street,
                        State = Enum.GetName(typeof(States), model.DeliveryAddress.State),
                    },
                };

                var token = HttpContext.Session.GetString("Token");
                var baseUrl = UrlHelper.BaseAddress(HttpContext);
                var response = await HttpHelper.PutContentAsync<UpdateUserDto>(baseUrl, userToUpdate, token, $"/api/v1/User/update-user/{id}");


                if (!response.Item2.IsSuccessStatusCode)
                {
                    var errResponse = JsonConvert.DeserializeObject<ResponseDto<string>>(response.Item1);
                    foreach (var err in errResponse.Errs)
                    {
                        ModelState.AddModelError(err.Key, err.Value);
                    }
                    ViewBag.res = "notsaved";
                    return View(res);
                }
                ViewBag.res = "saved";
                return View("profile", res);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PaymentHistory(int page, bool check)
        {
            var id = HttpContext.Session.GetString("UserId");
            var token = HttpContext.Session.GetString("Token");
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var (responseBody, response) = await HttpHelper.GetContentWithTokenAsync(baseUrl, "", token, $"api/v1/Order/payments-history-by-user/{id}?page={page}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            response.EnsureSuccessStatusCode();
            var deserializedContent = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<OrderDetailsViewModel>>>(responseBody);
            if (check == true)
            {
                return PartialView("PaymentHistoryPartialView", deserializedContent.Data);
            }

            return View(deserializedContent.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ViewBag.Q = "change-password";
            var resp = new ProfileViewModel();
            var update = _mapper.Map<ChangePasswordDto>(model);

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var userId = HttpContext.Session.GetString("UserId");
            var token = HttpContext.Session.GetString("Token");
            var (content, httpResponse) = await HttpHelper.PatchContentAsync(baseUrl, update, token, $"/api/v1/auth/change-password/{userId}");

            if (httpResponse.IsSuccessStatusCode)
            {
                ViewBag.resp = "saved";
                return View("profile", resp);
            }
            else
            {
                var errResponse = JsonConvert.DeserializeObject<ResponseDto<string>>(content);
                foreach (var error in errResponse.Errs)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                ViewBag.resp = "notsaved";
                return View("profile", resp);
            }
        }

        [HttpGet]
        [Authorize(Policy = "CustomerRolePolicy")]
        public async Task<IActionResult> Invoice([FromQuery] int page, [FromQuery] bool paginated)
        {
            string id = HttpContext.Session.GetString("UserId");
            string token = HttpContext.Session.GetString("Token");

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var (responseBody, response) = await HttpHelper.GetContentWithTokenAsync(baseUrl, "", token, "api/v1/Order/invoice/?" + $"page={page}&userid={id}");

            if (!response.IsSuccessStatusCode)
            {
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AccessDeniedHandler", "Error", new { StatusCode = 401 });
                }
            }

            var data = JsonConvert.DeserializeObject<ResponseDto<PagedInvoiceViewModel>>(responseBody);


            if (paginated)
            {
                return PartialView("InvoicePartial", data.Data);
            }
            return View(data.Data);
        }


        [HttpGet]
        [Authorize(Policy = "AdminAndDeliveryRolePolicy")]
        public async Task<IActionResult> DeliveryAssignment(bool check, int page)
        {

            string UserId = HttpContext.Session.GetString("UserId");
            string token = HttpContext.Session.GetString("Token");

            var deseriliazed = await GetAssignmentsByStatus(token, page, UserId, "Pending");
            if (check == true)
                return PartialView("AssignmentPartialView", deseriliazed);
            return View(deseriliazed);
        }


        public async Task<IActionResult> AcceptedPartialView(int page)
        {
            string UserId = HttpContext.Session.GetString("UserId");
            string token = HttpContext.Session.GetString("Token");
            var deserialized = await GetAssignmentsByStatus(token, page, UserId, "Accepted");
            return PartialView(deserialized);
        }

        public async Task<IActionResult> Accept(string assignmentId)
        {
            string UserId = HttpContext.Session.GetString("UserId");
            string token = HttpContext.Session.GetString("Token");

            var baseUrl = UrlHelper.BaseAddress(HttpContext);

            var Response = await HttpHelper.PutContentAsync<AcceptDeliveryAssignmentViewModel>(
                                            baseUrl, null, token, $"/api/v1/Assignment/" +
                                            $"accept-assignment/{assignmentId}");

            var deserialized = await GetAssignmentsByStatus(token, 0, UserId, "Accepted");
            return PartialView("AcceptedPartialView", deserialized);
        }


        private async Task<ResponseDto<PaginatedResultDto
                                <AssignmentDeliveryViewModel>>> GetAssignmentsByStatus(string token, int page, string UserId, string status)
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var Response = await HttpHelper.GetContentAsync<string>(baseUrl, "", token,
                                            $"/api/v1/Assignment/get-assignments-by-user-and-status?" +
                                            $"page={page}&AppUserId={UserId}&status={status}");

            var content = Response.Item1;
            var httpResponse = Response.Item2;
            httpResponse.EnsureSuccessStatusCode();

            var DeserializedContent = JsonConvert.DeserializeObject
                                    <ResponseDto<PaginatedResultDto<AssignmentDeliveryViewModel>>>(content);
            return DeserializedContent;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TrackOrders(int Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");
            var result = await HttpHelper.GetContentWithTokenAsync(baseUrl, "", token, $"/api/v1/Tracking/get-tracking-by-num?number={Id}");
            var responsebody = result.Item1;
            var response = result.Item2;

            if (!response.IsSuccessStatusCode)
            {
                var errResponse = JsonConvert.DeserializeObject<ResponseDto<TrackingHistoryViewModel>>(responsebody);
                foreach (var err in errResponse.Errs)
                {
                    ModelState.AddModelError(err.Key, err.Value);
                }
                return View();
            }
            else
            {
                var successResponse = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<TrackingHistoryForUsersDto>>>(responsebody);
                var model = new TrackingHistoryViewModel();
                List<TrackingHistoryViewModel> toReturn = new List<TrackingHistoryViewModel>();
                foreach (var item in successResponse.Data.Reverse())
                {
                    var stuff = new TrackingHistoryViewModel { Location = item.Location, OrderId = item.OrderId, Status = (TrackingHistoryStatus)item.Status, CustomerName = item.CustomerName, DeliveryPersonName = item.DeliveryPersonName, Description = item.Description };

                    toReturn.Add(stuff);
                }
                ViewData["Histories"] = toReturn;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Orders(int page, bool check)
        {
            var token = HttpContext.Session.GetString("Token");
            // var userId = HttpContext.Session.GetString("UserId");
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var (responseKey, response) = await HttpHelper.GetContentWithTokenAsync<string>(baseUrl, "", token,
                                            $"/api/v1/Order/get-orders?page={page}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            // response.EnsureSuccessStatusCode();
            var deserializedContent = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<OrdersViewModel>>>(responseKey);

            if (check == true)
                return PartialView("OrderPartialView", deserializedContent.Data);
            return View(deserializedContent.Data);
        }



        [HttpGet]
        public async Task<IActionResult> OrderDetails(string Id)
        {

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");
            var result = await HttpHelper.GetContentWithTokenAsync(baseUrl, "", token, $"/api/v1/Order/get-order/{Id}");
            var responsebody = result.Item1;
            var response = result.Item2;

            if (!response.IsSuccessStatusCode)
            {
                var errResponse = JsonConvert.DeserializeObject<ResponseDto<OrderToreturnByOrderIdDto>>(responsebody);
                return View(response);
            }
            else
            {
                var successResponse = JsonConvert.DeserializeObject<ResponseDto<OrderToreturnByOrderIdDto>>(responsebody);

                var res = new OrdersDetailsViewModel
                {
                    TrackingNumber = successResponse.Data.TrackingNumber,
                    ShippedTo = successResponse.Data.ShippedTo,
                    DeliveryDate = successResponse.Data.DeliveryDate,
                    OrderItems = successResponse.Data.OrderItems,
                    Status = successResponse.Data.Status
                };

                foreach (var item in successResponse.Data.OrderItems)
                {
                    res.PaymentAmount += item.Total;
                }
                ViewBag.Id = Id;
                return View(res);

            }

        }


        [HttpPost]
        public async Task<IActionResult> ConfirmOrder([FromForm]string Id)
        {

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");
            var result = await HttpHelper.PatchContentAsync(baseUrl, "", token, $"/api/v1/order/confirm-order/{Id}");

            var response = result.Item2;

            if (!response.IsSuccessStatusCode)
                return View(response);

            return RedirectToAction("Orders", "Dashboard");

        }

    }

}

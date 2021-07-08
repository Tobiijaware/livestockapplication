using AnimalFarmsMarket.Commons;
using AnimalFarmsMarket.Data.DTOs;
using AnimalFarmsMarket.Data.Models;
using AnimalFarmsMarket.Data.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalFarmsMarket.Core.Controllers
{
    public class MarketController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public MarketController(IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
        }


      

        

       
        [HttpGet]
        public async Task<IActionResult> Index(int page, string location, bool check)
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var localMarketDataToReturn = new PagedLivestockInfoVm();

            if (!string.IsNullOrEmpty(location))
            {

                var (LocalMarketResponse, LocalMarketData1) = await HttpHelper.GetContentAsync(baseUrl, "","", "/api/v1/LiveStock/get-livestocks-by-location/?page=" + page + "&location=" + location);
                
                if (!LocalMarketData1.IsSuccessStatusCode)
                {
                    var ErrorData = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<string>>>(LocalMarketResponse);
                    ViewData["errorLocation"] = location;
                    ViewData["error"] = ErrorData.Message.ToString();
                }
                
                var LocalMarketData = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<ShapedListOfLivestockVm>>>(LocalMarketResponse);
                if (LocalMarketData.Data != null)
                {
                    localMarketDataToReturn = new PagedLivestockInfoVm
                    {
                        PageMetaData = LocalMarketData.Data.PageMetaData,
                        ResponseData = LocalMarketData.Data.ResponseData
                    };
                }
            }


            /// section to get location and market
            var res = await SideNavMethod(baseUrl, localMarketDataToReturn, location);

            //-------------------------------------------------------------------------------------------


            if (string.IsNullOrEmpty(location))
            {

                ViewData["Location"] = location;
                return View(res);

            }

            ViewData["Location"] = location;
            return View(res);



            //-----pagin---------------------------------

            var (respo, response) = await HttpHelper.GetContentAsync(baseUrl, "", "", "/api/v1/LiveStock/get-livestocks/?page=" + page);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("PageNotFoundHandler", "Error", new { StatusCode = response.StatusCode });
            }

            var content = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<ShapedListOfLivestockVm>>>(respo);

            if (check == true)
            {
                return PartialView("MarketPlacePartial", content.Data);
            }

            return View(content.Data);
        }



        [HttpGet]
        [ActionName("fetchLivestockLocation-market")]
        public async Task<IActionResult> Index(int page,string location, string market)
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var MarketDataToReturn = new PagedLivestockInfoVm();
            if (string.IsNullOrEmpty(location) && (string.IsNullOrEmpty(market)))
            {
                ViewData["Location"] = location;
                ViewData["errorLocation"] = location;
                return View();

            }

            if(!string.IsNullOrEmpty(location) && (!string.IsNullOrEmpty(market)))
            {
                var (MarketResponse, MarketData) = await HttpHelper.GetContentAsync(baseUrl, "", "",
                    "/api/v1/LiveStock/get-livestocks-by-location-and-market?page=" + page + "&location=" + location + "&market=" + market);

                if (!MarketData.IsSuccessStatusCode)
                {
                    var ErrorData = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<string>>>(MarketResponse);
                    ViewData["errorLocation"] = location;
                    ViewData["error"] = ErrorData.Message.ToString();
                }

                var myMarketData = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<ShapedListOfLivestockVm>>>(MarketResponse);
                if (myMarketData.Data != null)
                {
                    MarketDataToReturn = new PagedLivestockInfoVm
                    {
                        PageMetaData = myMarketData.Data.PageMetaData,
                        ResponseData = myMarketData.Data.ResponseData
                    };
                }
            }


            /// section to get location and market
            var res = await SideNavMethod(baseUrl, MarketDataToReturn, location);

            ViewData["Location"] = location;
            return View("Index",res);
        }



        [HttpGet]
        [ActionName("Index-fetchAll")]
        public async Task<IActionResult> Index(int page,string location)
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var localMarketDataToReturn = new PagedLivestockInfoVm();

            var (respo, response) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl, "", "/api/v1/LiveStock/get-livestocks/?page=" + page);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("PageNotFoundHandler", "Error", new { StatusCode = response.StatusCode });
            }

            var content = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<ShapedListOfLivestockVm>>>(respo);
            if (content.Data != null)
            {
                localMarketDataToReturn = new PagedLivestockInfoVm
                {
                    PageMetaData = content.Data.PageMetaData,
                    ResponseData = content.Data.ResponseData
                };
            }
            /// section to get location and market
            var res = await SideNavMethod(baseUrl, localMarketDataToReturn, location);
            ViewData["Location"] = location;
            return View("Index", res);
        }




        [HttpGet]
        [ActionName("IndexByQuery")]
        public async Task<IActionResult> Index(string type, byte sex, decimal weight, string breed, int page, string livestockLocation)
        {

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var MarketDataToReturn = new PagedLivestockInfoVm();

            /// if user clicks both search buttons with selecting a parameter
            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(breed) && weight == 0 && livestockLocation == null)
            {

                /// section to get location and market
                var res = await SideNavMethod(baseUrl, MarketDataToReturn, livestockLocation);

                ViewData["Location"] = livestockLocation;
                ViewData["errorLocation"] = livestockLocation;
                return View("Index", res);

            }
            if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(breed) && weight == 0 && livestockLocation == null)
            {

                /// section to get location and market
                var res = await SideNavMethod(baseUrl, MarketDataToReturn, livestockLocation);

                ViewData["Location"] = livestockLocation;
                ViewData["errorLocation"] = livestockLocation;
                return View("Index", res);

            }



            var (modelResponse1, modelResponse2) = await HttpHelper.GetContentWithoutTokenAsync(baseUrl, "", 
                "/api/v1/LiveStock/get-livestocks-by-search-term?" + $"Type={type}&Sex={sex}&Weight={weight}&Breed={breed}&PageNumber={page}");

            
            if (!modelResponse2.IsSuccessStatusCode)
            {
                var ErrorData = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<string>>>(modelResponse1);
                var res = await SideNavMethod(baseUrl, MarketDataToReturn, livestockLocation);
                ViewData["errorLocation"] = livestockLocation;
                ViewData["error"] = ErrorData.Message.ToString();
                return View("Index", res);
            }


            var modelData = JsonConvert.DeserializeObject<ResponseDto<PaginatedResultDto<ShapedListOfLivestockVm>>>(modelResponse1);
            if (modelData.Data != null)
            {
                MarketDataToReturn = new PagedLivestockInfoVm
                {
                    PageMetaData = modelData.Data.PageMetaData,
                    ResponseData = modelData.Data.ResponseData
                };
            }


            var myRes = await SideNavMethod(baseUrl, MarketDataToReturn, livestockLocation);
            ViewData["Location"] = livestockLocation;
            return View("Index", myRes);
        }


        [HttpGet]
        public async Task<IActionResult> GetLivestockByMarket(string market)
        {
            if (string.IsNullOrEmpty(market))
            {

                return View();

            }

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var (MarketResponse, MarketData1) = await HttpHelper.GetContentAsync(baseUrl, "", "", "/api/v1/LiveStock/get-livestocks-by-location/?page=" + market);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id){
            var details = new DetailsViewModel();
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var (responseBody, response) = await HttpHelper.GetContentAsync(baseUrl, "", "", "api/v1/LiveStock/get-livestock/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("PageNotFoundHandler", "Error", new { StatusCode = response.StatusCode });
            }
             var livestockdata = JsonConvert.DeserializeObject<ResponseDto<LivestockViewModel>>(responseBody);
            var (LocationResponse, LocationData1) = await HttpHelper.GetContentAsync(baseUrl, "", "", "/api/v1/LiveStock/get-livestocksLocation");
            if (!LocationData1.IsSuccessStatusCode)
            {
                var ErrorData = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<string>>>(LocationResponse);
                ViewData["ErrorLocation"] = "";
                ViewData["MarketError"] = ErrorData.Message.ToString();
                return View();
            }
            var LocationMarketData = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<LocationMarketVm>>>(LocationResponse);
            details.LivestockModel = livestockdata.Data;
            details.LivestockModel.LocationMarkets = LocationMarketData.Data;
            return View(details);


        }


        //Get distinct fields data for a category in JSON format
        [HttpGet]
        public async Task<IActionResult> GetDataForLivestockCategory(string livestockCategory)
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);

            var (respo, response) = await HttpHelper.GetContentAsync(baseUrl, "","", "/api/v1/LiveStock/get-livestock-category-data/?category=" + livestockCategory);
            var livestockByType = JsonConvert.DeserializeObject<LivestockResponseDto>(respo);
            if(!response.IsSuccessStatusCode){
                return RedirectToAction("PageNotFoundHandler", "Error", new {StatusCode = response.StatusCode });
            }
            return Json(respo);
        }


          
          

        //Shopping cart page
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShippingDetails()
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");
            var Response = await HttpHelper.GetContentAsync<string>(baseUrl, "", token, "/api/v1/Markets/shippingdetails");
            var content = Response.Item1;
            var httpResponse = Response.Item2;
            if (httpResponse.IsSuccessStatusCode)
            {
                var DeserializedContent = JsonConvert.DeserializeObject<ResponseDto<ShippingDetailsPageViewModel>>(content);

                var data = DeserializedContent.Data;

                
                var ship = new ShippingDetailsPageViewModel()
                {
                    DeliveryModes = data.DeliveryModes,
                    ShippingPlans = data.ShippingPlans,
                    User = data.User,
                    PaymentMethods = data.PaymentMethods
                };
                ViewBag.Ship = ship;
                return View();
            }
            return Redirect("/account/login");
        }


        [HttpPost]
        public async Task<IActionResult> Payment(ShippingDetailsToReturnViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");

            var temOrder = await HttpHelper.PostContentAsync<ShippingDetailsToReturnViewModel>(baseUrl, model, token, "/api/v1/order/get-temp-order");

            var orderResponse = JsonConvert.DeserializeObject<ResponseDto<Order>>(temOrder.Item1);

            HttpContext.Session.SetString("tempOrder", JsonConvert.SerializeObject(orderResponse.Data));

           

            var response = await HttpHelper.GetContentAsync<string>(baseUrl, "", token, "/api/v1/paymentmethod/"+model.PaymentMethodId);

            var content = response.Item1;
            var httpResponse = response.Item2;
            if (httpResponse.IsSuccessStatusCode)
            {
                var DeserializedContent = JsonConvert.DeserializeObject<ResponseDto<PaymentMethodDto>>(content);

                var data = DeserializedContent.Data;
                if (data.Name.ToLower().Contains("flutter"))
                    return RedirectToAction("Initiate", "Checkout");

                if (data.Name.ToLower().Contains("paystack"))
                {
                    var result = await PaystackPayment.InitiatePayment(new PaystackRequestDto { amount = Math.Ceiling(orderResponse.Data.PaymentAmount).ToString(), email = orderResponse.Data.User.Email, callback_url = baseUrl + "/market/shippingdetailssave" },
                                                            _config["Paystack:BaseUrl"], "initialize", _config["Paystack:Token"]);

                    if (result.Status == "true")
                    {
                        HttpContext.Session.SetString("reference", result.Data.Reference);
                        return Redirect(result.Data.AuthorizationUrl);
                    }
                    else
                    {
                        return RedirectToAction("ShippingDetails", "Market");
                    }
                        
                }
                    

                return RedirectToAction("ShippingDetailsSave", "Market");
            }

            return View();//Return a failure view
        }


        
        public async Task<IActionResult> ShippingDetailsSave()
        {
            bool verify = false;
            bool payOnDelivery = true;

            var model = JsonConvert.DeserializeObject<Order>(HttpContext.Session.GetString("tempOrder"));
            if(HttpContext.Session.GetString("verifyDetails") != null)
            {
                payOnDelivery = false;
                var verifyResponse = JsonConvert.DeserializeObject<FlutterwaveResponseDto>(HttpContext.Session.GetString("verifyDetails"));
                verify = await FlutterwavePayment.VerifyPayment(verifyResponse.data.id, verifyResponse.data.amount, _config["Flutterwave:BaseUrl"], _config["Flutterwave:SecretKey"]);
                model.PaymentStatus = 1;
            }
            
            if(HttpContext.Session.GetString("reference")!= null)
            {
                payOnDelivery = false;
                verify = await PaystackPayment.VerifyPayment(HttpContext.Session.GetString("reference"), _config["Paystack:BaseUrl"], _config["Paystack:Token"]);
                model.PaymentStatus = 1;
            }

            HttpContext.Session.Remove("tempOrder");
            HttpContext.Session.Remove("verifyDetails");
            HttpContext.Session.Remove("reference");


            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");


            if (!verify && !payOnDelivery)
                return RedirectToAction("Index","Home");


            var response = await HttpHelper.PostContentAsync(baseUrl, model, token, "api/v1/order/add-order");


            if (!response.Item2.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ResponseDto<ShippingDetailsToReturnViewModel>>(response.Item1);

            }
             var firebaseDbUrl = _config["Firebase:DbAddress"];
            var userId = HttpContext.Session.GetString("UserId");
            await HttpHelper.DeleteFirebaseCartAsync(firebaseDbUrl, userId);

            if (payOnDelivery)
                return RedirectToAction("PayOnDelivery", "Market");

            return RedirectToAction("PaymentConfirmation", "Market");
        }

        [HttpPost]
        public async Task<IActionResult> Details(AddReviewViewModel model, string id)
        {
            if(!ModelState.IsValid )
            {
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                return View(model);
            }

            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var userId = HttpContext.Session.GetString("UserId");
            var token = HttpContext.Session.GetString("Token");
            var ratingModel = new AddRatingDto
            {
                UserId = userId,
                LivestockId = id,
                RatingFigure = model.RatingFigure
            };
            var reviewModel = new ReviewToAddDto
            {
                UserId = userId,
                LivestockId = id,
                ReviewText = model.ReviewText
            };

            var (ratingResponseBody, ratingResponse) = await HttpHelper.PostContentAsync(baseUrl, ratingModel, token, "api/v1/Rating/add-rating");
            if (!ratingResponse.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ResponseDto<string>>(ratingResponseBody);
                foreach (var error in errorResponse.Errs)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return View(model);
            }

            var (reviewResponseBody, reviewResponse) = await HttpHelper.PostContentAsync(baseUrl, reviewModel, token, "api/v1/Review/add-review");
            if (!reviewResponse.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ResponseDto<string>>(reviewResponseBody);

                foreach (var error in errorResponse.Errs)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return View(model);
            }


           

            return RedirectToRoute("details", new { id = id });
        }




        private async Task<LivestockInfoListViewModel> SideNavMethod(string baseUrl, PagedLivestockInfoVm model, string location)
        {
            /// section to get location and market
            var (LocationResponse, LocationData1) = await HttpHelper.GetContentAsync(baseUrl, "", "", "/api/v1/LiveStock/get-livestocksLocation");

            if (!LocationData1.IsSuccessStatusCode)
            {
                var ErrorData = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<string>>>(LocationResponse);
                ViewData["ErrorLocation"] = location;
                ViewData["MarketError"] = ErrorData.Message.ToString();
            }
            var LocationMarketData = JsonConvert.DeserializeObject<ResponseDto<IEnumerable<LocationMarketVm>>>(LocationResponse);

            var myResponseData = new LivestockInfoListViewModel
            {
                Data = (model is null) ? null : model,
               Info = (LocationMarketData.Data is null) ? null : LocationMarketData.Data
            };
            return myResponseData;

        }



        //Ajax function call
        public async Task<IActionResult> ShippingJsonDetails()
        {
            var baseUrl = UrlHelper.BaseAddress(HttpContext);
            var token = HttpContext.Session.GetString("Token");

            var Response = await HttpHelper.GetContentAsync<string>(baseUrl, "", token, "/api/v1/Markets/shippingdetails");

            var content = Response.Item1;
            var httpResponse = Response.Item2;
            if (httpResponse.IsSuccessStatusCode)
            {
                var DeserializedContent = JsonConvert.DeserializeObject<ResponseDto<ShippingDetailsPageViewModel>>(content);

                var data = DeserializedContent.Data.ShippingPlans;
                return Json(data);
            }
            return Json(null);
        }



        [HttpGet]
        public IActionResult PaymentConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PayOnDelivery()
        {
            return View();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanvasMvcHelloWorld.Models;

namespace CanvasMvcHelloWorld.Controllers
{
    public class HelloWorldController : Controller
    {
        public SalesForceOAuth.RootObject oAuth { get; set; }
        // GET: /HelloWorld/
		//this is coming from staging
		//this is another coming from staging
        public ActionResult Index()
        {
            var postedSignedRequest = Request.Form["signed_request"];
            var model = new HelloWorldModel(postedSignedRequest);
            if (model.myRootObject == null)
            {
                if (model.SignedRequestStatus.Contains("Did not find"))
                {
                    return View(model);
                }
            }
            else             
            {
                var query = model.myRootObject.context.links.queryUrl + "?q=select id, name from account";
                oAuth = model.myRootObject;
                return View("sfdcIndex", model);
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( )
        {
            return View();

        }
	}
}
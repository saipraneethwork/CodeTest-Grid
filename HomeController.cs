using Praneeth.DataRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Praneeth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int page = 1, string sort = "Name", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetProducts(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public List<Product> GetProducts(string search, string sort, string sortdir, int skip, int pageSize, out int total)
        {
            using (AdventureWorks2012Entities myDB = new AdventureWorks2012Entities())
            {

                var data = (from item in myDB.Products
                            where item.Name.Contains(search) ||
                                  item.ProductNumber.Contains(search) ||
                                 item.ProductLine.Contains(search)
                            select item

                                );
                total = data.Count();

                data = data.OrderBy(sort);

                if (pageSize > 0)
                {
                    data = data.Skip(skip).Take(pageSize);
                }
                return data.ToList();

            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
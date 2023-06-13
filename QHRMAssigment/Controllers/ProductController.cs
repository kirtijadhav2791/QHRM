using Dapper;

using Microsoft.AspNetCore.Mvc;
using QHRMAssigment.Models;
using System.Data;
using System.Linq;

namespace QHRMAssigment.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperCon.ReturnList<AddNewProd>("GetAllProducts"));
        }

        //for insert operation
        [HttpGet]
        public ActionResult AddorUpdate(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@SN", id);

                return View(DapperCon.ReturnList<AddNewProd>("ProductViewById", p).FirstOrDefault<AddNewProd>());

            }
        }
        [HttpPost]
        public ActionResult AddorUpdate(AddNewProd prod)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("@SN", prod.SN);
            d.Add("@Product", prod.Product);
            d.Add("@Description", prod.Description);
            d.Add("@Price", prod.Price);
            d.Add("@Created", DateTime.Now); //set date and time
            DapperCon.ExecuteWithoutReturn("AddOrEditProduct", d);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("@SN", id);
            DapperCon.ExecuteWithoutReturn("ProductDeleteById", d);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            DynamicParameters d = new DynamicParameters();
            d.Add("@SN", id);
            DapperCon.ExecuteWithoutReturn("ShowProduct", d);
            return View(DapperCon.ReturnSingle<AddNewProd>("ShowProduct", d));
        }
    }
}

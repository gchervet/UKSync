using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ninject;
using ConsumosCapataz.Domain;
using ConsumosCapataz.Service;
using System.Configuration;

namespace ConsumosCapataz
{
    /// <summary>
    /// Summary description for StockMovementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StockMovementService : Ninject.Web.WebServiceBase
    {
        private int EnsolFood_IdCompany = Convert.ToInt32(ConfigurationManager.AppSettings["EnsolFood_IdCompany"]);

        [Inject]
        public StockService stockService { get; set; }

        [WebMethod]
        public bool StockSetStockConsumptionForOrder(EnsolStockMovement FinishedProduct, List<EnsolStockMovement> RawMaterials)
        {
            SetStockConsumptionForOrder(EnsolFood_IdCompany, FinishedProduct, RawMaterials);
            return true;
        }

        [WebMethod]
        public Order SetStockConsumptionForOrder(int IdCompany, EnsolStockMovement FinishedProduct, List<EnsolStockMovement> RawMaterials)
        {
            return stockService.ConsumeOrder(IdCompany, FinishedProduct, RawMaterials);

            //return true;
        }
    }
}

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
    /// Summary description for ArticleService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ArticleService : Ninject.Web.WebServiceBase
    {
        private int EnsolFood_IdCompany = Convert.ToInt32(ConfigurationManager.AppSettings["EnsolFood_IdCompany"]);

        [Inject]
        public StockService stockService { get; set; }

        [WebMethod]
        public List<Article> Article_GetAll()
        {
            return Article_GetByIdCompany_All(EnsolFood_IdCompany);
        }

        [WebMethod]
        public Article Article_GetByCode(string Code)
        {
            return Article_GetBy_ByIdCompany_Code(EnsolFood_IdCompany, Code);
        }

        [WebMethod]
        public Article Article_GetByID(int ID)
        {
            return Article_GetByIdCompany_ID(EnsolFood_IdCompany, ID);
        }


        [WebMethod]
        public List<Article> Article_GetByIdCompany_All(int IdCompany)
        {
            return new List<Article>();
        }

        [WebMethod]
        public Article Article_GetBy_ByIdCompany_Code(int IdCompany, string Code)
        {
            return new Article();
        }

        [WebMethod]
        public Article Article_GetByIdCompany_ID(int IdCompany, int ID)
        {
            return new Article();
        }
    }
}

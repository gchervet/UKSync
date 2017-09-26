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
    /// Summary description for RecipeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RecipeService : Ninject.Web.WebServiceBase
    {
        private int EnsolFood_IdCompany = Convert.ToInt32(ConfigurationManager.AppSettings["EnsolFood_IdCompany"]);

        [Inject]
        public StockService stockService { get; set; }

        [WebMethod]
        public Recipe Recipe_GetByCode_Version(string Code, string Version)
        {
            return Recipe_GetByIdCompany_Code_Version(EnsolFood_IdCompany, Code, Version);
        }

        [WebMethod]
        public List<Recipe> Recipe_GetCurrentRecipesCodes()
        {
            return Recipe_GetCurrentRecipesCodes_ByIdCompany(EnsolFood_IdCompany);
        }

        [WebMethod]
        public int Recipe_GetCurrentVersion_ByCode(string Code)
        {
            return Recipe_GetCurrentVersion_ByIdCompany_Code(EnsolFood_IdCompany, Code);
        }

        [WebMethod]
        public Recipe Recipe_GetByIdCompany_Code_Version(int IdCompany, string Code, string Version)
        {
            return new Recipe();
        }

        [WebMethod]
        public List<Recipe> Recipe_GetCurrentRecipesCodes_ByIdCompany(int IdCompany)
        {
            return new List<Recipe>();
        }

        [WebMethod]
        public int Recipe_GetCurrentVersion_ByIdCompany_Code(int IdCompany, string Code)
        {
            return 0;
        }
    }
}

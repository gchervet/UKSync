using AutoMapper;
using ConsumosCapataz.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace ConsumosCapataz.DAL
{
    public class OrderDal : DalBase
    {

        public OrderDal(ModelEntities context)
            : base(context)
        {
        }

        public List<Order> SPECreaOT(CompanyEnum company, string codigoArticulo, Nullable<decimal> cantidad, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            List<Order> rtn = new List<Order>();
            using (var ctx = new ModelEntities())
            {
                List<SPE_CREA_OT_Result> ot = new List<SPE_CREA_OT_Result>();
                switch (company)
                {
                    case CompanyEnum.Ensol:
                        ot = ctx.ENSOL_CREA_OT(codigoArticulo, cantidad, codigoDeposito, usuario, codigoUsuario, terminal).ToList();
                        break;
                    case CompanyEnum.Ensolfood:
                        ot = ctx.ENSOLFOOD_CREA_OT(codigoArticulo, cantidad, codigoDeposito, usuario, codigoUsuario, terminal).ToList();
                        break;
                    case CompanyEnum.Ensolpigs:
                        ot = ctx.ENSOLPIGS_CREA_OT(codigoArticulo, cantidad, codigoDeposito, usuario, codigoUsuario, terminal).ToList();
                        break;
                    default:
                        throw new Exception("Invalid company");
                }

                foreach (SPE_CREA_OT_Result item in ot)
                {
                    Order order = new Order();
                    order.Number = item.NumeroOt;
                    order.Type = item.TipoOt;
                    rtn.Add(order);
                }
            }
            return rtn;
        }

        public List<EnsolStockMovement> SPECreaECA(CompanyEnum company, string tipoOt, string numeroOt, List<EnsolStockMovement> RawMaterials, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            List<EnsolStockMovement> rtn = new List<EnsolStockMovement>();

            using (var ctx = new ModelEntities())
            {
                DataSet retVal = new DataSet();
                SqlConnection sqlConn = (SqlConnection)ctx.Database.Connection;

                string spEca = string.Empty;
                switch (company)
                {
                    case CompanyEnum.Ensol:
                        spEca = "ENSOL_CREA_ECA";
                        break;
                    case CompanyEnum.Ensolfood:
                        spEca = "ENSOLFOOD_CREA_ECA";
                        break;
                    case CompanyEnum.Ensolpigs:
                        spEca = "ENSOLPIGS_CREA_ECA";
                        break;
                    default:
                        throw new Exception("Invalid company");
                }

                SqlCommand cmdEca = new SqlCommand(spEca, sqlConn);
                SqlDataAdapter daEca = new SqlDataAdapter(cmdEca);
                using (cmdEca)
                {
                    DataTable dt = Common.ToDataTable(RawMaterials);

                    ////Use DbType.Structured for TVP
                    var articulosParameter = new SqlParameter("articulos", SqlDbType.Structured);
                    articulosParameter.Value = dt;
                    articulosParameter.TypeName = "UdtEnsolStockMovement";

                    cmdEca.Parameters.Add(articulosParameter);

                    var tipoOtParameter = tipoOt != null ?
                        new SqlParameter("tipoOt", tipoOt) :
                        new SqlParameter("tipoOt", typeof(string));

                    cmdEca.Parameters.Add(tipoOtParameter);

                    var numeroOtParameter = numeroOt != null ?
                        new SqlParameter("numeroOt", numeroOt) :
                        new SqlParameter("numeroOt", typeof(string));

                    cmdEca.Parameters.Add(numeroOtParameter);

                    var usuarioParameter = usuario != null ?
                        new SqlParameter("usuario", usuario) :
                        new SqlParameter("usuario", typeof(string));

                    cmdEca.Parameters.Add(usuarioParameter);

                    var codigoUsuarioParameter = codigoUsuario.HasValue ?
                        new SqlParameter("codigoUsuario", codigoUsuario) :
                        new SqlParameter("codigoUsuario", typeof(int));

                    cmdEca.Parameters.Add(codigoUsuarioParameter);

                    var terminalParameter = terminal != null ?
                        new SqlParameter("terminal", terminal) :
                        new SqlParameter("terminal", typeof(string));

                    cmdEca.Parameters.Add(terminalParameter);

                    cmdEca.CommandType = CommandType.StoredProcedure;
                    daEca.Fill(retVal);
                }
                rtn = (List<EnsolStockMovement>)Common.ToList<EnsolStockMovement>(retVal.Tables[0]);
            }
            return rtn;
        }

        public List<EnsolStockMovement> SPECreaICA(CompanyEnum company, string tipoOt, string numeroOt, string lote, int idOrden, string codigoArticulo, string descripcionArticulo, Nullable<decimal> cantidad, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            List<EnsolStockMovement> rtn = new List<EnsolStockMovement>();
            using (var ctx = new ModelEntities())
            {
                List<SPE_CREA_ICA_Result> results = new List<SPE_CREA_ICA_Result>();

                switch (company)
                {
                    case CompanyEnum.Ensol:
                        results = ctx.ENSOL_CREA_ICA(tipoOt, numeroOt, codigoArticulo, descripcionArticulo, cantidad, lote, idOrden, codigoDeposito, usuario, codigoUsuario, terminal).ToList();
                        break;
                    case CompanyEnum.Ensolfood:
                        results = ctx.ENSOLFOOD_CREA_ICA(tipoOt, numeroOt, codigoArticulo, descripcionArticulo, cantidad, lote, idOrden, codigoDeposito, usuario, codigoUsuario, terminal).ToList();
                        break;
                    case CompanyEnum.Ensolpigs:
                        results = ctx.ENSOLPIGS_CREA_ICA(tipoOt, numeroOt, codigoArticulo, descripcionArticulo, cantidad, lote, idOrden, codigoDeposito, usuario, codigoUsuario, terminal).ToList();
                        break;
                    default:
                        throw new Exception("Invalid company");
                }

                rtn = Mapper.DynamicMap<List<EnsolStockMovement>>(results);
            }
            return rtn;
        }

        public void SPECierreOT(CompanyEnum company, string tipoOt, string numeroOt, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            using (var ctx = new ModelEntities())
            {
                switch (company)
                {
                    case CompanyEnum.Ensol:
                        ctx.ENSOL_CIERRE_OT(tipoOt, numeroOt, usuario, codigoUsuario, terminal);
                        break;
                    case CompanyEnum.Ensolfood:
                        ctx.ENSOLFOOD_CIERRE_OT(tipoOt, numeroOt, usuario, codigoUsuario, terminal);
                        break;
                    case CompanyEnum.Ensolpigs:
                        ctx.ENSOLPIGS_CIERRE_OT(tipoOt, numeroOt, usuario, codigoUsuario, terminal);
                        break;
                    default:
                        throw new Exception("Invalid company");
                }
            }
        }
    }
}

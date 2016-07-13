using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;
using Healkeep.Models;

namespace Healkeep
{
    /// <summary>
    /// Summary description for HealkeepService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HealkeepService : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetData()
        {
            List<Diseases> listDiseases = new List<Diseases>();
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Diseases;Select * from NaturalTreatments", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataView dataView = new DataView(ds.Tables[1]);

                foreach (DataRow diseaseDataRow in ds.Tables[0].Rows)
                {
                    Diseases disease = new Diseases();
                    disease.DiseaseID = Convert.ToInt32(diseaseDataRow["DiseaseID"]);
                    disease.Name = diseaseDataRow["Name"].ToString();

                    dataView.RowFilter = "DiseaseID = '" + disease.DiseaseID + "'";

                    List<NaturalTreatments> listNaturalTreatments = new List<NaturalTreatments>();

                    foreach (DataRowView naturalTreatmentDataRowView in dataView)
                    {
                        DataRow naturalTreatmentDataRow = naturalTreatmentDataRowView.Row;
                        NaturalTreatments naturalTreatment = new NaturalTreatments();
                        naturalTreatment.NaturalTreatmentID = Convert.ToInt32(naturalTreatmentDataRow["NaturalTreatmentID"]);
                        naturalTreatment.Name = naturalTreatmentDataRow["Name"].ToString();
                        naturalTreatment.DiseaseID = Convert.ToInt32(naturalTreatmentDataRow["DiseaseID"]);
                        listNaturalTreatments.Add(naturalTreatment);
                    }

                    disease.NaturalTreatments = listNaturalTreatments;
                    listDiseases.Add(disease);

                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listDiseases));
        }

        [WebMethod]
        public void GetComments()
        {
            List<Comments> listComments = new List<Comments>();
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Comments", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow commentDataRow in ds.Tables[0].Rows)
                {
                    Comments comment = new Comments();
                    comment.CommentID = Convert.ToInt32(commentDataRow["CommentID"]);
                    comment.VoteUp = Convert.ToInt32(commentDataRow["VoteUp"]);
                    listComments.Add(comment);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listComments));
        }
    }
}

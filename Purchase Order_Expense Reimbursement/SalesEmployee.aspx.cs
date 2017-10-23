using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Purchase_Order_Expense_Reimbursement
{
    public partial class SalesEmployee : System.Web.UI.Page
    {
        string EmployeeID;
        string LoggedInSalesperson;
        protected void Page_Init(object sender, EventArgs e)
        {
            EmployeeID = (string)(Session["UserSalesLogin"]);
            LoggedInSalesperson = (string)Session["LoggedInSalesUser"];
            lblSalesperson.Text = LoggedInSalesperson;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string)Session["UserSalesLogin"] == null)
            {
                Response.Redirect("LoginSales.aspx");
            }

            try
            {
                lblSalesperson.Text = " Hello " + LoggedInSalesperson + "!";
                viewdata();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }
        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public void viewdata()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            DataTable dataTable = new DataTable("Sales");
            string query = "SELECT ContactLogID, CompanyName, ContactName," +
            " ContactNumber, ContactAddress" +
            " FROM ContactDetailsLog WHERE EmployeeID = @param1";
            SqlDataAdapter dataAdap = new SqlDataAdapter(query, conn);
            dataAdap.SelectCommand.Parameters.AddWithValue("@param1", EmployeeID);
            dataAdap.Fill(dataTable);
            GridSalesLog.DataSource = dataTable;
            GridSalesLog.DataBind();

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Response.Redirect("LoginSales.aspx");
        }
    }
}
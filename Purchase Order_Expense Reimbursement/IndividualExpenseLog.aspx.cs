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
    public partial class IndividualExpenseLog : System.Web.UI.Page
    {
        string EmployeeID;
        string LoggedInEmployee;
        protected void Page_Init(object sender, EventArgs e)
        {
            EmployeeID = (string)(Session["UserLogin"]);
            LoggedInEmployee = (string)Session["LoggedInUser"];
            lblManagerName.Text = LoggedInEmployee;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string)Session["UserLogin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
           
            try
            {
                lblManagerName.Text = " Hello " + LoggedInEmployee + "!";
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
            DataTable dataTable = new DataTable("Expenses");
            string query = "SELECT ExpenseID, ExpenseName, ExpenseAmount, ExpenseType,"+
            " ExpenseAddress, ExpenseDate, ExpenseNote, ExpenseApproval" +
            " FROM Expenses WHERE EmployeeID = @param1";
            SqlDataAdapter dataAdap = new SqlDataAdapter(query, conn);
            dataAdap.SelectCommand.Parameters.AddWithValue("@param1", EmployeeID);
            dataAdap.Fill(dataTable);
            GridExpenseLog.DataSource = dataTable;
            GridExpenseLog.DataBind();

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Response.Redirect("Login.aspx");
        }
    }
}
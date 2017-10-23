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
    public partial class ManagerExpenseLog : System.Web.UI.Page
    {
        string ManagerID;
        string LoggedInManager;
        protected void Page_Init(object sender, EventArgs e)
        {
            ManagerID = (string)(Session["UserLogin"]);
            LoggedInManager = (string)Session["LoggedInUser"];
            lblManagerName.Text = LoggedInManager;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string)Session["UserLogin"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            try
            {
                lblManagerName.Text = " Hello " + LoggedInManager + "!";
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
            string query = "SELECT Expenses.ExpenseID, Expenses.EmployeeID, Employees.FirstName, Expenses.ExpenseName," +
            " Expenses.ExpenseAmount, Expenses.ExpenseType, Expenses.ExpenseAddress," +
            " Expenses.ExpenseDate, Expenses.ExpenseNote, Expenses.ExpenseApproval" +
            " FROM Employees " +
            " JOIN Expenses ON Expenses.EmployeeID = Employees.EmployeeID" +
            " WHERE ManagerID = @param1";
            SqlDataAdapter dataAdap = new SqlDataAdapter(query, conn);
            dataAdap.SelectCommand.Parameters.AddWithValue("@param1", ManagerID);
            dataAdap.Fill(dataTable);
            GridManagerLog.DataSource = dataTable;
            GridManagerLog.DataBind();

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
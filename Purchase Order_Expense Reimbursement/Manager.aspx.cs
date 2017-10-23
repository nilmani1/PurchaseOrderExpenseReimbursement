using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Purchase_Order_Expense_Reimbursement
{
    public partial class Manager : System.Web.UI.Page
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
            
            if ((string)Session["UserLogin"] != Request.QueryString["IdEmployee"])
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                //lblManagerName.Text = LoggedInManager;
                ClearControls();
            }
            try
            {
                lblManagerName.Text = " Hello "+LoggedInManager+"!";
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
            " Expenses.ExpenseDate, Expenses.ExpenseNote" +
            " FROM Employees " +
            " JOIN Expenses ON Expenses.EmployeeID = Employees.EmployeeID" +
            " WHERE ManagerID = @param1 and ExpenseApproval = 'Pending'";
            SqlDataAdapter dataAdap = new SqlDataAdapter(query, conn);
            dataAdap.SelectCommand.Parameters.AddWithValue("@param1", ManagerID);
            dataAdap.Fill(dataTable);
            Gridview1.DataSource = dataTable;
            Gridview1.DataBind();

        }

        protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Gridview1.Rows[index];  // row which u have created..
                //row.BackColor = Color.Blue;
                string ExpenseId = row.Cells[2].Text;
                SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();
                string query = "UPDATE Expenses SET ExpenseApproval = 'Approved'" +
                                " WHERE ExpenseID = @param1 ; ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = ExpenseId;
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@param1", SqlDbType.Int).Value = EmployeeId;
                cmd.ExecuteNonQuery();
                conn.Close();
                
                viewdata();

                // similarly u can get all the values of the row..
            }

            if (e.CommandName == "Reject") // other command names etc etc...
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = Gridview1.Rows[index];  // row which u have created..

                //int RowIndexx = Convert.ToInt32(row.RowIndex);
                //Gridview1.Rows[index].BackColor = System.Drawing.Color.Red;

                string ExpenseId = row.Cells[2].Text;
                SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();
                string query = "UPDATE Expenses SET ExpenseApproval = 'Declined'" +
                                " WHERE ExpenseID = @param1 ; ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = ExpenseId;
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@param1", SqlDbType.Int).Value = EmployeeId;
                cmd.ExecuteNonQuery();
                conn.Close();
                viewdata();
            }
        }

        
        public void ClearControls()
        {
            lblManagerName.Text = String.Empty;
            Gridview1.DataSource = null;
            Gridview1.DataBind();
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
        protected void btnExpenseLog_Click(object sender, EventArgs e)
        {
            string pageurl = "ManagerExpenseLog.aspx";
            Response.Write("<script> window.open('" + pageurl + "','_blank'); </script>");
        }
    }
}
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
    public partial class ExpenseEditor : System.Web.UI.Page
    {
        public string expenseType;
        string EmployeeID;
        string loggedInUserName;

        protected void Page_Init(object sender, EventArgs e)
        {
            EmployeeID = (string)(Session["UserLogin"]);
            loggedInUserName = (string)Session["LoggedInUser"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //string EmployeeID = Request.QueryString["UserLogin"];
            if ((string)Session["UserLogin"] != Request.QueryString["IdEmployee"])
            {
                Response.Redirect("Login.aspx", false);
            }
            
            if (!Page.IsPostBack)
            {
                ClearControls();
            }
            try
            {
                lblEmployeeName.Text = loggedInUserName;
            }
            catch
            {

            }
        }    

        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            expenseType = radioExpenseType.SelectedValue;
            LogExpense(EmployeeID, txtExpenseName.Text, txtExpenseAmount.Text, expenseType, txtAddress.Text, txtComment.Text, lblDate.Text);
        }

        public void LogExpense(string EmployeeID, string strExpenseName, string strExpenseAmount, string strExpenseType,
            string strAddress, string strComment, string strlblDate)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            try
            {
                string query = "INSERT INTO EXPENSES"+
                    "(EmployeeID, ExpenseName, ExpenseAmount, ExpenseType, ExpenseAddress, ExpenseDate, ExpenseNote)"+
                    "VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7)";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = EmployeeID;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = strExpenseName;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 50).Value = strExpenseAmount;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = strExpenseType;
                cmd.Parameters.Add("@param5", SqlDbType.VarChar, 50).Value = strAddress;
                cmd.Parameters.Add("@param6", SqlDbType.VarChar, 50).Value = strlblDate;
                cmd.Parameters.Add("@param7", SqlDbType.VarChar, 50).Value = strComment;
                
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                
                ClearControls();
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void ClearControls()
        {
            txtExpenseName.Text = String.Empty;
            txtExpenseAmount.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtComment.Text = String.Empty;
            DatePicker.SelectedDates.Clear();
            lblDate.Text = String.Empty;
            lblEmployeeName.Text = String.Empty;
        }

        protected void Date_SelectionChanged(object sender, EventArgs e)
        {
            lblDate.Text = DatePicker.SelectedDate.ToShortDateString();
        }

        protected void BtnClearRegister_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        public void radioExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //radioExpense = radioExpenseType.SelectedValue;
            if(radioExpenseType.SelectedValue == "Purchase order")
            {
                expenseType = "Purchase order";
            }
            else if(radioExpenseType.SelectedValue == "Expense Reimbursement")
            {
                expenseType = "Expense Reimbursement";
            }
            else
            {
                expenseType = "";
            }
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
            string pageurl = "IndividualExpenseLog.aspx";
            Response.Write("<script> window.open('" + pageurl + "','_blank'); </script>");
        }
    }
}
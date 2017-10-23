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
    public partial class LoginSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClearControls();
            }
        }
        public string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        protected void BtnLogIn_Click(object sender, EventArgs e)
        {
            ValidateSales(TxtUserName.Text, TxtPassWord.Text);
        }
        string EmployeeFirstName;
        public string IdEmployee;
        public bool Ismngr;
        private void ValidateSales(string UserName, string PassWord)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            string query = "SELECT EmployeeID, IsManager, FirstName FROM Employees Where UserName = @param1 and PassWord = @param2";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@param1", UserName);
                cmd.Parameters.AddWithValue("@param2", PassWord);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    EmployeeFirstName = dr["FirstName"].ToString();
                    IdEmployee = dr["EmployeeID"].ToString();
                    Ismngr = (bool)dr["IsManager"];
                    Session["UserSalesLogin"] = IdEmployee;
                    Session["LoggedInSalesUser"] = EmployeeFirstName;
                    if (Ismngr)
                    {
                        //Response.Redirect("Manager.aspx", false);
                        Response.Redirect("SalesManager.aspx?IdEmployee=" + IdEmployee);
                    }
                    else
                    {
                        //Response.Redirect("ExpenseEditor.aspx", false);
                        Response.Redirect("SalesEmployee.aspx?IdEmployee=" + IdEmployee);
                    }

                }
                else
                {
                    lblErrorMessage.Text = "Incorrect Username or Password. Please try again.";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                ClearControls();
            }
        }
        protected void BtnClearLogIn_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        public void ClearControls()
        {
            TxtUserName.Text = String.Empty;
            TxtPassWord.Text = String.Empty;
            lblErrorMessage.Text = String.Empty;
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Purchase_Order_Expense_Reimbursement
{
    public partial class SalesManager : System.Web.UI.Page
    {
        string ddlSelectedID;
        string ManagerID;
        string LoggedInManager;
        protected void Page_Init(object sender, EventArgs e)
        {
            ManagerID = (string)(Session["UserSalesLogin"]);
            LoggedInManager = (string)Session["LoggedInSalesUser"];
            lblManagerName.Text = LoggedInManager;
            ddlSelectedID = (string)Session["myDdlID"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserSalesLogin"] != Request.QueryString["IdEmployee"])
            {
                Response.Redirect("LoginSales.aspx");
            }
            if (!Page.IsPostBack)
            {
                //populate dropdown list
                SqlConnection conn = new SqlConnection(GetConnectionString());
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, FirstName, LastName FROM Employees WHERE ManagerID = @param1 AND IsManager = 'false'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@param1", ManagerID);
                SqlDataReader dr = cmd.ExecuteReader();
                ddlEmployee.DataSource = dr;
                ddlEmployee.DataTextField = "FirstName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
                conn.Close();
                ddlEmployee.Items.Insert(0, new ListItem("Select Salesperson", "0"));
                ClearControls();

                viewlog();
            }
            try
            {

                //Display name of loggedin user
                lblManagerName.Text = " Hello " + LoggedInManager + "!";
                
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

        protected void ImportCSV(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                string csvData = "";
                //Upload and save the file
                string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload.PostedFile.FileName);
                FileUpload.SaveAs(csvPath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ContactLogID", typeof(int)),
            new DataColumn("CompanyName", typeof(string)),
            new DataColumn("ContactName", typeof(string)),
            new DataColumn("ContactNumber", typeof(string)),
            new DataColumn("ContactAddress",typeof(string)) });

                //Read the contents of CSV file.
                csvData = File.ReadAllText(csvPath);

                //Execute a loop over the rows.
                Boolean headerRowHasBeenSkipped = false; //To skip 1st line
                foreach (string row in csvData.Split('\n'))
                {
                    if (headerRowHasBeenSkipped)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }
                    headerRowHasBeenSkipped = true;
                }

                GridSalesManager.DataSource = dt;
                GridSalesManager.DataBind();
                // add to session
                Session["myTable"] = dt;
            }
            else
            {
                lblNotification.CssClass = "label label-warning";
                lblNotification.Text = "No file has selected!";
            }
        }

        public void viewlog()
        {
            GridSalesManager.DataSource = null;
            GridSalesManager.DataBind();
            SqlConnection conn = new SqlConnection(GetConnectionString());
            DataTable dataTable = new DataTable("ContactDetails");
            string query = "SELECT ContactLogID, CompanyName, ContactName,ContactNumber, ContactAddress FROM ContactDetailsLog WHERE EmployeeID is null";
            SqlDataAdapter dataAdap = new SqlDataAdapter(query, conn);
            //dataAdap.SelectCommand.Parameters.AddWithValue("@param1", ManagerID);
            dataAdap.Fill(dataTable);
            GridSalesManager.DataSource = dataTable;
            GridSalesManager.DataBind();

        }
        public void ClearControls()
        {
            lblManagerName.Text = String.Empty;
            GridSalesManager.DataSource = null;
            GridSalesManager.DataBind();
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

        protected void SaveCSV(object sender, EventArgs e)
        {
            DataTable dataTableTemp = (DataTable)Session["myTable"];
            if (dataTableTemp == null)
            {
                lblNotification.CssClass = "label label-warning";
                lblNotification.Text = "Current data has already been stored!";
            }
            else
            {
                SqlConnection conn = new SqlConnection(GetConnectionString());
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn);
                //Set the database table name
                sqlBulkCopy.DestinationTableName = "dbo.ContactDetailsLog";
                conn.Open();
                sqlBulkCopy.WriteToServer(dataTableTemp);
                conn.Close();
                Session["myTable"] = null;
                lblNotification.CssClass = "label label-success";
                lblNotification.Text = "Data stored!";
                viewlog();
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            if (ddlSelectedID != null)
            {
                foreach (GridViewRow row in GridSalesManager.Rows)
                {
                    //if (row.RowType == DataControlRowType.DataRow)
                    //{
                        //CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        CheckBox chkRow = (CheckBox)row.FindControl("chkRow");
                        if (chkRow != null && chkRow.Checked)
                        //if (chkRow.Checked)
                        {

                        //int ContactID = Convert.ToInt32(GridSalesManager.DataKeys[row.RowIndex].Value);
                            string ContactID = row.Cells[1].Text;
                            SqlConnection conn = new SqlConnection(GetConnectionString());
                            conn.Open();
                            string query = "UPDATE ContactDetailsLog SET EmployeeID = @param1 WHERE ContactLogId = @param2 ; ";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = ddlSelectedID;
                            cmd.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ContactID;
                            cmd.CommandType = CommandType.Text;
                            //cmd.Parameters.AddWithValue("@param1", SqlDbType.Int).Value = EmployeeId;
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            viewlog();
                    }
                        
                    //}

                }
            }
            else
            {
                lblNotification.Text = "Please select a Salesperson.";
            }
        }

        protected void ddlEmployee_Change(object sender, EventArgs e)
        {

            Session["myDdlID"] = ddlEmployee.SelectedItem.Value;
        }
        protected void cb_Challenged_CheckedChanged(object sender, EventArgs e)
        {
            //this oncheckedchanged-event in the aspx is essential, otherwise checkbox.checked will always be false!
        }
    }
}
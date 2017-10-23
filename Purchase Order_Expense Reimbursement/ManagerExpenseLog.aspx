<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerExpenseLog.aspx.cs" Inherits="Purchase_Order_Expense_Reimbursement.ManagerExpenseLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">

        <div class="jumbotron" style="overflow-x: auto">
            <asp:Label ID="lblManagerName" runat="server" Text="Label"></asp:Label>
            <asp:Button type="submit" CausesValidation="false" Text="Logout" runat="server" 
                class="btn btn-default" style="float:right" ID="btnLogout" OnClick="btnLogout_Click" />
            <asp:GridView ID="GridManagerLog" Class="table table-striped table-bordered table-hover"
                runat="server">

            </asp:GridView>
        </div>

</asp:Content>

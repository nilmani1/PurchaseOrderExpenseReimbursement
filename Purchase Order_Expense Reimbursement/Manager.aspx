<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="Purchase_Order_Expense_Reimbursement.Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">

    <div class="jumbotron" style="overflow-x: auto">
        <asp:Label ID="lblManagerName" runat="server" Text="Label"></asp:Label>
        <asp:Button type="submit" CausesValidation="false" Text="Logout" runat="server"
            class="btn btn-default" Style="float: right" ID="btnLogout" OnClick="btnLogout_Click" />
        <asp:Button type="submit" CausesValidation="false" Text="Expense List" runat="server"
            class="btn btn-default" Style="float: right" ID="btnExpenseLog" OnClick="btnExpenseLog_Click" />
        <asp:GridView ID="Gridview1" Class="table table-striped table-bordered table-hover"
            runat="server" OnRowCommand="Gridview1_RowCommand">
            <Columns>
                <asp:ButtonField CommandName="Approve" runat="server" Text="Approve" />
                <asp:ButtonField CommandName="Reject" runat="server" Text="Reject" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>

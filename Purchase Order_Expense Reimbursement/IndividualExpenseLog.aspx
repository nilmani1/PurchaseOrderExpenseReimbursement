<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndividualExpenseLog.aspx.cs" Inherits="Purchase_Order_Expense_Reimbursement.IndividualExpenseLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
        <div class="jumbotron" style="overflow-x: auto">
            <asp:Label ID="lblManagerName" runat="server" Text="Label"></asp:Label>
            <asp:Button type="submit" CausesValidation="false" Text="Logout" runat="server" 
                class="btn btn-default" style="float:right" ID="btnLogout" OnClick="btnLogout_Click" />
            <asp:GridView ID="GridExpenseLog" Class="table table-striped table-bordered table-hover"
                runat="server">
                <%--<Columns>
                    <asp:ButtonField CommandName="Approve" runat="server" Text="Approve" />
                    <asp:ButtonField CommandName="Reject" runat="server" Text="Reject" />
                </Columns>--%>

            </asp:GridView>
        </div>
</asp:Content>

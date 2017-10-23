<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginSales.aspx.cs" Inherits="Purchase_Order_Expense_Reimbursement.LoginSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
        <div class="container">
        <div class="jumbotron col-xs-12 col-sm-6" style="margin-left: 25%">
            <h3 class="page-header" style="border-bottom:solid">Sales Lead Tracker</h3>
            <div class="form-group">
                <label for="User name" class="sr-only">User name:</label>
                <asp:TextBox type="text" class="form-control" ID="TxtUserName" runat="server" placeholder="Enter username"/>
                <asp:RequiredFieldValidator ID="TxtUserNameValidator" runat="server" ControlToValidate="TxtUserName"
                    ErrorMessage="Last name is a required field." ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="pwd" class="sr-only">Password:</label>
                <asp:TextBox type="password" class="form-control" ID="TxtPassWord" runat="server" placeholder="Enter password"/>
                <asp:RequiredFieldValidator ID="TxtPassWordValidator" runat="server" ControlToValidate="TxtPassWord"
                    ErrorMessage="Last name is a required field." ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <asp:Button type="submit" Text="LogIn" runat="server" class="btn btn-default" ID="BtnLogIn" OnClick="BtnLogIn_Click" />
            <asp:Button type="submit" CausesValidation="false" Text="Clear" runat="server" class="btn btn-default" ID="BtnClearLogIn" OnClick="BtnClearLogIn_Click" />
            <asp:Label runat="server" Display="Dynamic" ID="lblErrorMessage" for="error"></asp:Label>
        </div>
    </div>
</asp:Content>

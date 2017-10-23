<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpenseEditor.aspx.cs" Inherits="Purchase_Order_Expense_Reimbursement.ExpenseEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <asp:Label ID="lblEmployeeName" runat="server" Text="Label"></asp:Label>
            <asp:Button type="submit" CausesValidation="false" Text="Logout" runat="server" 
                class="btn btn-default" style="float:right" ID="btnLogout" OnClick="btnLogout_Click" />
            <asp:Button type="submit" CausesValidation="false" Text="Expense List" runat="server" 
                class="btn btn-default" style="float:right" ID="btnExpenseLog" OnClick="btnExpenseLog_Click" />
            <div class="form-group">
                <label for="FirstName">Expense Name:</label>
                <asp:TextBox type="text" class="form-control" ID="txtExpenseName" runat="server" />
                <asp:RequiredFieldValidator ID="txtExpenseNameValidator" runat="server" ControlToValidate="txtExpenseName"
                    ErrorMessage="First name is a required field." Display="Dynamic" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="LastName">Expense Amount:</label>
                <asp:TextBox type="text" class="form-control" ID="txtExpenseAmount" runat="server" />
                <asp:RequiredFieldValidator ID="txtExpenseAmountValidator" runat="server" ControlToValidate="txtExpenseAmount"
                    ErrorMessage="Expense Amount is a required field." ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:RadioButtonList ID="radioExpenseType" runat="server"
                    onselectedindexchanged="radioExpenseType_SelectedIndexChanged">
                    <asp:ListItem Text="Purchase order" Value="Purchase order"></asp:ListItem>
                    <asp:ListItem Text="Expense Reimbursement" Value="Expense Reimbursement"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="radioExpenseTypeValidator" runat="server" ControlToValidate="radioExpenseType"
                    ErrorMessage="Must select Expense type." ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="User name">Address:</label>
                <asp:TextBox type="text" class="form-control" ID="txtAddress" runat="server" />
                <asp:RequiredFieldValidator ID="txtAddressValidator" runat="server" ControlToValidate="txtAddress"
                    ErrorMessage="Username is a required field." ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="pwd">Date of Expense:</label>
                <asp:Label type="Date" class="form-control" ID="lblDate" runat="server"></asp:Label>
                <asp:Calendar runat="server" ID="DatePicker" OnSelectionChanged="Date_SelectionChanged"></asp:Calendar>
            </div>
            <div class="form-group">
                <label for="pwd">Additional Comment on Expense:</label>
                <asp:TextBox type="text" class="form-control" ID="txtComment" runat="server" />
            </div>
            <asp:Button type="submit" Text="Submit" runat="server" class="btn btn-default" ID="BtnSubmit" OnClick="BtnSubmit_Click" />
            <asp:Button type="submit" CausesValidation="false" Text="Clear" runat="server" class="btn btn-default" ID="BtnClearRegister" OnClick="BtnClearRegister_Click" />
        </div>
    </div>
</asp:Content>

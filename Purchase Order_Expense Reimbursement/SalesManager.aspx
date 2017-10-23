<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesManager.aspx.cs" Inherits="Purchase_Order_Expense_Reimbursement.SalesManager" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
    <div class="container">
        <div class="jumbotron" style="overflow-x: auto">

            <asp:Button type="submit" CausesValidation="false" Text="Logout" runat="server"
                class="btn btn-default" Style="float: right" ID="btnLogout" OnClick="btnLogout_Click" />
            <asp:Label class="label label-default" ID="lblManagerName" Style="float: right; margin-top: 10px;" runat="server" Text="Label"></asp:Label>
            <asp:FileUpload ID="FileUpload" runat="server" />
            <div>
                <asp:Button Text="Import CSV file" class="btn btn-default" ID="btnImport" OnClick="ImportCSV" runat="server" />
                <asp:Button Text="Save" class="btn btn-default" ID="btnSave" OnClick="SaveCSV" runat="server" />
                <asp:Label ID="lblNotification" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_Change"></asp:DropDownList>
                <asp:Button Text="Assign" class="btn btn-default" ID="Button1" OnClick="btnAssign_Click" runat="server" />
            </div>
            <asp:GridView ID="GridSalesManager" Class="table table-striped table-bordered table-hover"
                runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" runat="server" oncheckedchanged="cb_Challenged_CheckedChanged"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjectDataSourceInsertEditDelete.aspx.cs" Inherits="GridViewUsefulStuff.Object_Data_Source.ObjectDataSourceInsertEditDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:GridView ID="GridView" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="EmployeeId" DataSourceID="ObjectDataSource1" ShowFooter="True">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="EmployeeId" SortExpression="EmployeeId">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("EmployeeId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmployeeId") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnb_Insert" OnClick="Lnb_Insert_Click" ValidationGroup="INSERT" runat="server">Insert</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="tb_Name" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_EditName" runat="server" 
                                ErrorMessage="Name is required!!!" 
                                ControlToValidate="tb_Name" 
                                ForeColor="Red" 
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="tb_InsertName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_InsertName" runat="server" 
                                ErrorMessage="Name is required when Inserting!!!" 
                                ControlToValidate="tb_InsertName" 
                                ForeColor="Red" 
                                Text="*"
                                ValidationGroup="INSERT">
                            </asp:RequiredFieldValidator>
                        
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddl_Gender" runat="server" SelectedValue='<%# Bind("Gender") %>'>
                                <asp:ListItem Text="Select Gender" Value="Select Gender"></asp:ListItem>
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_EditGender" runat="server" 
                                ErrorMessage="Gender is required!!!" 
                                ControlToValidate="ddl_Gender" 
                                ForeColor="Red" 
                                Text="*"
                                InitialValue="Select Gender">
                            </asp:RequiredFieldValidator>  
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddl_InsertGender" runat="server">
                                <asp:ListItem Text="Select Gender" Value="Select Gender"></asp:ListItem>
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="rfv_InsertGender" runat="server" 
                                ErrorMessage="Gender is required when Inserting!!!" 
                                ControlToValidate="ddl_InsertGender" 
                                ForeColor="Red" 
                                Text="*"
                                InitialValue="Select Gender"
                                ValidationGroup="INSERT">
                            </asp:RequiredFieldValidator> 
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cty" SortExpression="Cty">
                        <EditItemTemplate>
                            <asp:TextBox ID="tb_City" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_EditCity" runat="server" 
                                ErrorMessage="City is required!!!" 
                                ControlToValidate="tb_City" 
                                ForeColor="Red" 
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="tb_InsertCity" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_InsertCity" runat="server" 
                                ErrorMessage="City is required when Inserting!!!" 
                                ControlToValidate="tb_InsertCity" 
                                ForeColor="Red" 
                                Text="*"
                                ValidationGroup="INSERT">
                            </asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="DeleteEmployee" InsertMethod="InsertEmployee" SelectMethod="GetAllEmployees" TypeName="GridViewUsefulStuff.EmployeeDataAccessLayer" UpdateMethod="UpdateEmployee">
                <DeleteParameters>
                    <asp:Parameter Name="EmployeeId" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Gender" Type="String" />
                    <asp:Parameter Name="City" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="EmployeeId" Type="Int32" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Gender" Type="String" />
                    <asp:Parameter Name="City" Type="String" />
                </UpdateParameters>
        </asp:ObjectDataSource>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="INSERT"/>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" />
    </div>
    </form>
</body>
</html>

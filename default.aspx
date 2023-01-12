<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="hw08_brodzinski._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Registration System</title>
    <link rel="styleSheet" href="styles.css" />
</head>


<body style="width: 654px; height: 405px">
    <form id="form1" runat="server">
        <div>
            <h1>Course Registration System</h1>

            <asp:CheckBoxList ID="checkBoxListAddOn" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem  Value="1000">Dorm</asp:ListItem> 
                <asp:ListItem  Value="500">Meal Plan</asp:ListItem> 
                <asp:ListItem  Value="50">Football Tickets</asp:ListItem>
            </asp:CheckBoxList>

            <table style="width:100%;">
                <tr>
                    <td width="5%">Available Classes</td>
                    <td width="22%">&nbsp;</td>
                    <td class="auto-style2">Registered Classes</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:ListBox ID="lbxAvailableClasses" runat="server" SelectionMode="Multiple" Height="256px" Width="125px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="addBtn" runat="server" Text="Add" OnClick="addBtn_Click" CausesValidation="False" />
                        <br />
                        <asp:Button ID="removeBtn" runat="server" Text="Remove" OnClick="removeBtn_Click" CausesValidation="False" />
                        <br />
                        <asp:Button ID="resetBtn" runat="server" Text="Reset" OnClick="resetBtn_Click" CausesValidation="False" />
                        <br />
                        Total Hours:<asp:Label ID="lblHours" runat="server" Text="0"></asp:Label>
                        <br />
                        Total Cost:<asp:Label ID="lblCost" runat="server" Text="$0.00"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:ListBox ID="lbxRegisteredClasses" runat="server" SelectionMode="Multiple" Height="256px" Width="125px"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblAlert" runat="server" visible="False" ForeColor="Red"></asp:Label>
                    </td>
                    
                </tr>
            </table>

            <asp:Label ID="Label1" runat="server" Text="Class Number: "></asp:Label>
            <asp:TextBox ID="classNumBox" runat="server"></asp:TextBox>

            <asp:Label ID="Label2" runat="server" Text="Credits: "></asp:Label>
            <asp:TextBox ID="creditsBox" runat="server"></asp:TextBox>

        </div>
        
        <asp:Button ID="makeAval" runat="server" OnClick="makeAval_Click" Text="Make Available" Width="156px" />
        <asp:Button ID="removeAval" runat="server" Text="Remove From Available" Width="187px" OnClick="removeAval_Click" />
        <p>
            <asp:Label ID="lblExists" runat="server" ForeColor="Red"></asp:Label>
        </p>

        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="creditsBox" ErrorMessage="Please enter an integer 1 - 10!" ForeColor="Red" MaximumValue="10" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        
    </form>
    </body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PgeExport_Invoice_New.aspx.cs" Inherits="PgeExport_Invoice_New" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;
            if (KeyCode == 40) {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            }
            else if (KeyCode == 38) {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            }
            else if (KeyCode == 13) {
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;
                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtEdit_Doc_No") {
                    document.getElementById("<%=txtEdit_Doc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEdit_Doc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEdit_Doc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%= txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAc_Code_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv1") {
                    document.getElementById("<%= txtInv1.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv1.ClientID %>").focus();
                    document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                }
                if (hdnfClosePopupValue == "txtInv2") {
                    document.getElementById("<%= txtInv2.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv2.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv3") {
                    document.getElementById("<%= txtInv3.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv3.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv4") {
                    document.getElementById("<%= txtInv4.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv4.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv5") {
                    document.getElementById("<%= txtInv5.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv5.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv6") {
                    document.getElementById("<%= txtInv6.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv6.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv7") {
                    document.getElementById("<%= txtInv7.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv7.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv8") {
                    document.getElementById("<%= txtInv8.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv8.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv9") {
                    document.getElementById("<%= txtInv9.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv9.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtInv10") {
                    document.getElementById("<%= txtInv10.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtInv10.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtPart_no") {
                    document.getElementById("<%= txtPart_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblPart_no.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPart_no.ClientID %>").focus();
                }

            }
        }
        function SelectRow(CurrentRow, RowIndex) {
            UpperBound = parseInt('<%= this.grdPopup.Rows.Count %>') - 1;
            LowerBound = 0;
            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)
                if (SelectedRow != null) {
                    SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                    SelectedRow.style.color = SelectedRow.originalForeColor;
                }
            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#DCFC5C';
                CurrentRow.style.color = 'Black';
            }
            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }
    </script>
    <script type="text/javascript" language="javascript">

        function Ac_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtAc_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {


                e.preventDefault();
                var from = $("#<%=txtAc_Code.ClientID %>").val();

                from = "0" + from;
                $("#<%=txtAc_Code.ClientID %>").val(from);
                __doPostBack("txtAc_Code", "TextChanged");
            }
        }
        function Part_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtPart_no.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtPart_No", "TextChanged");
            }
        }
        function Inv1(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv1"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv2(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv2"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv3(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv3"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv4(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv4"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv5(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv5"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv6(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv6"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv7(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv7"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv8(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv8"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv9(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv9"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }
        function Inv10(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtInv10"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnGo.ClientID %>").focus();
            }
        }

        function save(e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();

            }
        }
        function chanegno(e) {

            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();

                var edi = "txtEdit_Doc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEdit_Doc_No", "TextChanged");

            }
        }
    </script>

      <script type="text/javascript">
          function BACK() {

              window.open('../Outword/PgeExport_Invoice_NewUtility.aspx', '_self');
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Export Invoice New " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" TabIndex="22" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                              &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" TabIndex="48" OnClientClick="BACK()" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 100%;" align="Left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Edit doc No
                            <asp:TextBox Height="24px" ID="txtEdit_Doc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEdit_Doc_No_TextChanged"
                                onKeyDown="chanegno(event);"></asp:TextBox>
                            Invoice No
                            <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                            <asp:Button Width="80px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName"></asp:Label>
                            Date
                            <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                    runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Customer
                            <asp:TextBox Height="24px" ID="txtAc_Code" runat="Server" CssClass="txt" TabIndex="4"
                                Width="90px" Style="text-align: left;" OnkeyDown="Ac_Code(event);" AutoPostBack="false"
                                OnTextChanged="txtAc_Code_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtAc_Code" runat="server" Text="..."
                                OnClick="btntxtAc_Code_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblAc_Code_Name" runat="server" CssClass="lblName"></asp:Label>
                            Our Invoice Value:
                            <asp:Label ID="lblExport_Amount" runat="server" CssClass="lblName"></asp:Label>
                            Our Invoice Qty:
                            <asp:Label ID="lblExport_Qty" runat="server" CssClass="lblName"></asp:Label>
                            Difference:
                            <asp:Label ID="lblDifference" runat="server" CssClass="lblName"></asp:Label>
                            Unique ID:
                            <asp:Label ID="lblUnique_ID" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Invoice No.1
                            <asp:TextBox Height="24px" ID="txtInv1" runat="Server" CssClass="txt" TabIndex="5"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv1_TextChanged"
                                OnkeyDown="Inv1(event);"></asp:TextBox>
                            Invoice No.2
                            <asp:TextBox Height="24px" ID="txtInv2" runat="Server" CssClass="txt" TabIndex="6"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv2_TextChanged"
                                OnKeyDown="Inv2(event);"></asp:TextBox>
                            Invoice No.3
                            <asp:TextBox Height="24px" ID="txtInv3" runat="Server" CssClass="txt" TabIndex="7"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv3_TextChanged"
                                OnKeyDown="Inv3(event);"></asp:TextBox>
                            Invoice No.4
                            <asp:TextBox Height="24px" ID="txtInv4" runat="Server" CssClass="txt" TabIndex="8"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv4_TextChanged"
                                OnKeyDown="Inv4(event);"></asp:TextBox>
                            Invoice No.5
                            <asp:TextBox Height="24px" ID="txtInv5" runat="Server" CssClass="txt" TabIndex="9"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv5_TextChanged"
                                OnKeyDown="Inv5(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Invoice No.6
                            <asp:TextBox Height="24px" ID="txtInv6" runat="Server" CssClass="txt" TabIndex="10"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv6_TextChanged"
                                OnKeyDown="Inv6(event);"></asp:TextBox>
                            Invoice No.7
                            <asp:TextBox Height="24px" ID="txtInv7" runat="Server" CssClass="txt" TabIndex="11"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv7_TextChanged"
                                OnKeyDown="Inv7(event);"></asp:TextBox>
                            Invoice No.8
                            <asp:TextBox Height="24px" ID="txtInv8" runat="Server" CssClass="txt" TabIndex="12"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv8_TextChanged"
                                OnKeyDown="Inv8(event);"></asp:TextBox>
                            Invoice No.9
                            <asp:TextBox Height="24px" ID="txtInv9" runat="Server" CssClass="txt" TabIndex="13"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv9_TextChanged"
                                OnKeyDown="Inv9(event);"></asp:TextBox>
                            Invoice No.10
                            <asp:TextBox Height="24px" ID="txtInv10" runat="Server" CssClass="txt" TabIndex="14"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtInv10_TextChanged"
                                OnKeyDown="Inv10(event);"></asp:TextBox>
                            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="btnHelp" Width="90px" ValidationGroup="save"
                                OnClick="btnGO_Click" Height="24px" TabIndex="15" />
                        </td>
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                    Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" TabIndex="28"
                                    Visible="false" />
                            </td>
                        </tr>
                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
                    margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
                    border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">
                            Detail Entry</h5>
                    </legend>
                </fieldset>
                <div style="width: 100%; position: relative;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlgrdGenerate" runat="server" ScrollBars="Both" Height="180px" Width="1000px"
                                BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                                Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                <asp:GridView ID="grdGenerate" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                    OnRowCommand="grdGenerate_RowCommand" CellPadding="5" Style="table-layout: fixed;"
                                    OnRowDataBound="grdGenerate_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditResult" runat="server" CommandName="EditRecord" Text="Edit"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteRessult" runat="server" CommandName="DeleteRecord" Text="Delete"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <table width="80%" align="Left">
                    <tr>
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Enter Name"></asp:Label>
                        </td>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                    Visible="false"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                               Item Code
                                <asp:TextBox ID="txtPart_no" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                    onkeydown="Part_No(event);" OnTextChanged="txtPart_no_TextChanged" Style="text-align: left;"
                                    TabIndex="16" Width="90px"></asp:TextBox>
                                <asp:Button ID="btntxtPart_no" runat="server" CssClass="btnHelp" OnClick="btntxtPart_no_Click"
                                    Text="..." />
                                <asp:Label ID="lblPart_no" runat="server" CssClass="lblName"></asp:Label>
                                Qty
                                <asp:TextBox ID="txtQty" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px"
                                    OnTextChanged="txtQty_TextChanged" Style="text-align: left;" TabIndex="17" Width="90px"></asp:TextBox>
                                Rate
                                <asp:TextBox ID="txtRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px"
                                    OnTextChanged="txtRate_TextChanged" Style="text-align: left;" TabIndex="18" Width="90px"
                                    OnKeyDown="save(event);"></asp:TextBox>
                                Value
                                <asp:TextBox ID="txtValue" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px"
                                    OnTextChanged="txtValue_TextChanged" Style="text-align: left;" TabIndex="19"
                                    Width="90px"></asp:TextBox>
                                <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnAdddetails_Click"
                                    TabIndex="20" Text="ADD" Width="80px" />
                                <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="25px"
                                    OnClick="btnClosedetails_Click" TabIndex="21" Text="Close" Width="80px" />
                                Net Qty:
                                <asp:Label ID="lblInvoice_Qty" runat="server" CssClass="lblName"></asp:Label>
                                Net Value
                                <asp:Label ID="lblInvoice_Value" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                    </tr>
                </table>
                <div style="width: 100%; position: relative;">
                    <asp:UpdatePanel ID="upGrid" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="180px" Width="1000px"
                                BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                                Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                    OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                                    Style="table-layout: fixed;">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="Edit"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
                min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
                left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Search Text:
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" Width="100%" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                    <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999;
                left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

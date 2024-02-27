<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeExport_ApAr.aspx.cs" Inherits="Foundman_Outword_pgeExport_ApAr" %>

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
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDoc_No") {
                    document.getElementById("<%= txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblDoc_No.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%= txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCash_Bank") {
                    document.getElementById("<%= txtCash_Bank.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblCash_Bank.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCash_Bank.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBill_No") {
                    document.getElementById("<%= txtBill_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= txtBill_Date.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= txtBill_Amount.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%= txtRecieved.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    document.getElementById("<%= txtPending.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    document.getElementById("<%= txtUnique_Id.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%= hdnfunique.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[5].innerText;

                    document.getElementById("<%=txtBill_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtLC_Number") {
                    document.getElementById("<%= txtLC_Number.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    document.getElementById("<%=txtLC_Number.ClientID %>").focus();
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
    <script type="text/javascript">
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEditDoc_No";
                $("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                $("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");
            }
        }
        function Doc_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtDoc_No.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtDoc_No", "TextChanged");
            }
        }
        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtAc_Code.ClientID %>").click();
                //("#<%=btntxtAc_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtAc_Code.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtAc_Code.ClientID %>").val(Accode);
                __doPostBack("txtAc_Code", "TextChanged");
            }
        }
        function Cash_Bank(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtCash_Bank.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtCash_Bank.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtCash_Bank.ClientID %>").val(Accode);
                __doPostBack("txtCash_Bank", "TextChanged");
            }
        }
        function Doc_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtDoc_No.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtDoc_No", "TextChanged");
            }
        }
        function Bill_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtBill_No.ClientID %> ").click();
            }
            //            if (e.keyCode == 9) {

            //                e.preventDefault();
            //                var Accode = $("#<%=txtBill_No.ClientID %>").val();

            //                Accode = "0" + Accode;
            //                $("#<%=txtBill_No.ClientID %>").val(Accode);
            //                __doPostBack("txtBill_No", "TextChanged");
            //            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %> ").focus();
            }

        }

        function LCNo(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtLC_Number.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtLC_Number.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtLC_Number.ClientID %>").val(Accode);
                __doPostBack("txtLC_Number", "TextChanged");
            }
        }
    </script>
    <script type="text/javascript">

        function auth() {
            window.open('../Master/pgeAuthentication.aspx', '_self');
        }
        function authenticate() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Authenticate data?")) {
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
    <script type="text/javascript">
        function disableClick(elem) {
            elem.disabled = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Export Ap/Ar " Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfunique" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" TabIndex="24" />
                        &nbsp;
                        <%-- <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" TabIndex="25" />
                        &nbsp;--%>
                        <asp:Button OnClientClick="disableClick(this)" OnClick="btnSave_Click" runat="server"
                            Text="Save" UseSubmitBehavior="false" ID="btnSave" CssClass="btnHelp" ValidationGroup="add"
                            Width="90px" Height="24px" TabIndex="24" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" TabIndex="26" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px"
                            TabIndex="27" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" TabIndex="28" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" TabIndex="29" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" TabIndex="30" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px" TabIndex="31" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px" TabIndex="32" />
                        <asp:Button ID="btnAuthentication" runat="server" CssClass="btnHelp" OnClick="btnAuthentication_Click"
                            Width="200px" Height="24px" TabIndex="23" Text="Authentication" OnClientClick="authenticate()"
                            Visible="false" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Names="verdana" Font-Italic="true"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Change No
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                onKeyDown="chanegno(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Tran Type
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpTran_Type" runat="Server" CssClass="txt" TabIndex="2" Width="220px"
                                Height="30px" AutoPostBack="true" OnSelectedIndexChanged="drpTran_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Cash Reciept Against Sale Bill" Value="RC" />
                                <asp:ListItem Text="Cash Payment Against Purchase Bill" Value="PC" />
                                <asp:ListItem Text="Bank Reciept Against Sale Bill" Value="RB" Selected="True" />
                                <asp:ListItem Text="Bank Payment Against Purchase Bill" Value="BB" />
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%;">
                            Doc No
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="3"
                                Width="90px" Style="text-align: left;" OnkeyDown="Doc_No(event);" AutoPostBack="false"
                                OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                            <asp:Button Width="70px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">
                            Doc Date
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="4"
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
                        <td align="left" style="width: 10%;">
                            Ac Code
                        </td>
                        <td align="left" style="width: 10%;" colspan="4">
                            <asp:TextBox Height="24px" ID="txtAc_Code" runat="Server" CssClass="txt" TabIndex="5"
                                Width="90px" Style="text-align: left;" OnkeyDown="Ac_Code(event);" AutoPostBack="false"
                                OnTextChanged="txtAc_Code_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtAc_Code" runat="server" Text="..."
                                OnClick="btntxtAc_Code_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblAc_Code" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Cash Bank
                        </td>
                        <td align="left" style="width: 10%;" colspan="4">
                            <asp:TextBox Height="24px" ID="txtCash_Bank" runat="Server" CssClass="txt" TabIndex="6"
                                Width="90px" Style="text-align: left;" OnkeyDown="Cash_Bank(event);" AutoPostBack="false"
                                OnTextChanged="txtCash_Bank_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtCash_Bank" runat="server" Text="..."
                                OnClick="btntxtCash_Bank_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblCash_Bank" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            L.C.Number
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtLC_Number" runat="Server" CssClass="txt" TabIndex="7"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtLC_Number_TextChanged"
                                onKeyDown="LCNo(event);"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtLC_Number" runat="server" Text="..."
                                OnClick="btntxtLC_Number_Click" CssClass="btnHelp" />
                        </td>
                        <td align="left" style="width: 10%;">
                            Cheq No
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtCheq_No" runat="Server" CssClass="txt" TabIndex="8"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCheq_No_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">
                            Cheq Date
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtCheq_Date" runat="Server" CssClass="txt" TabIndex="9"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCheq_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtCheq_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtCheq_Date"
                                    runat="server" TargetControlID="txtCheq_Date" PopupButtonID="imgcalendertxtCheq_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Check Amount
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtCheck_Amount" runat="Server" CssClass="txt" TabIndex="10"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCheck_Amount_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">
                            Bank Date
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtBank_Date" runat="Server" CssClass="txt" TabIndex="11"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBank_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtBank_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtBank_Date"
                                    runat="server" TargetControlID="txtBank_Date" PopupButtonID="imgcalendertxtBank_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                        <td align="left" style="width: 10%;">
                            Remark
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="50px" ID="txtRemark" runat="Server" CssClass="txt" TabIndex="12"
                                TextMode="MultiLine" Width="250px" Style="text-align: left;" AutoPostBack="false"
                                OnTextChanged="txtRemark_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" Visible="false" />
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
                <table width="70%" align="left">
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
                        <td align="left" style="width: 10%;">
                            Type
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="txtType" runat="Server" CssClass="txt" TabIndex="13" Width="150px"
                                Height="30px" AutoPostBack="true" OnSelectedIndexChanged="txtType_SelectedIndexChanged">
                                <asp:ListItem Text="Debit Note" Value="DS" />
                                <asp:ListItem Text="Credit Note" Value="CN" />
                                <asp:ListItem Text="Sale Bill" Value="SB" Selected="True" />
                                <asp:ListItem Text="Excess Payment/Advance Payment" Value="EP" />
                            </asp:DropDownList>
                            <%-- <asp:TextBox ID="txtType" runat="Server" CssClass="txt" TabIndex="14" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtType_TextChanged"
                                Height="24px"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Bill No
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBill_No" runat="Server" CssClass="txt" TabIndex="14" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_No_TextChanged"
                                OnkeyDown="Bill_No(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtBill_No" runat="server" Text="..." OnClick="btntxtBill_No_Click"
                                CssClass="btnHelp" />
                            <asp:Label ID="lblBill_No" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">
                            Bill Date
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBill_Date" runat="Server" CssClass="txt" TabIndex="15" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtBill_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtBill_Date"
                                    runat="server" TargetControlID="txtBill_Date" PopupButtonID="imgcalendertxtBill_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                        <td align="left" style="width: 10%;">
                            Bill Amount
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBill_Amount" runat="Server" CssClass="txt" TabIndex="16" Width="90px"
                                Enabled="false" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_Amount_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 5%;">
                            Pending
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPending" runat="Server" CssClass="txt" TabIndex="17" Width="90px"
                                Enabled="false" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPending_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Recieved
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtRecieved" runat="Server" CssClass="txt" TabIndex="18" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRecieved_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">
                            Adjusted
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtAdjusted" runat="Server" CssClass="txt" TabIndex="19" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAdjusted_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">
                            Narration
                        </td>
                        <td align="left" style="width: 10%;" colspan="3">
                            <asp:TextBox ID="txtNarration" runat="Server" CssClass="txt" TabIndex="20" Width="450px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNarration_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Unique Id
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtUnique_Id" runat="Server" CssClass="txt" TabIndex="21" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtUnique_Id_TextChanged"
                                Height="24px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="left" colspan="2">
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                Height="25px" OnClick="btnAdddetails_Click" TabIndex="22" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                Height="25px" OnClick="btnClosedetails_Click" TabIndex="23" />
                        </td>
                        <td align="left" style="width: 10%;">
                            Received
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:Label ID="lblreceviedetot" runat="server" CssClass="lblName" Font-Size="Large"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">
                            Balance Amount
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:Label ID="lblbalamt" runat="server" CssClass="lblName" Font-Size="Large"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:Button ID="btnUpdateBillAdj" runat="server" Text="Update Bill AdjustedAmt" CssClass="btnHelp"
                                Width="200px" ValidationGroup="save" OnClick="btnUpdateBillAdj_Click" Height="24px"
                                TabIndex="40" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 100%; position: relative;">
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="200px" Width="1200px"
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
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 1000px;
                min-height: 700px; box-shadow: 1px 1px 8px 2px; background-position: center;
                left: 20%; top: 10%;">
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
                                Style="z-index: 5000; float: right; overflow: auto; height: 680px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="25"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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

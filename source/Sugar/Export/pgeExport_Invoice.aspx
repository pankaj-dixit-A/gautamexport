<%@ Page Title="Export Invoice" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeExport_Invoice.aspx.cs" Inherits="pgeExport_Invoice" %>

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
                if (hdnfClosePopupValue == "txtPart_no") {
                    document.getElementById("<%= txtPart_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblPart_no.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPart_no.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipTo_Code") {
                    document.getElementById("<%=txtShipTo_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    //document.getElementById("<%=txtShip_To.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtShip_To.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtShipTo_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtConsign_code") {
                    document.getElementById("<%=txtConsign_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    //document.getElementById("<%=txtShip_To.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtConsign.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtConsign_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstRate") {
                    document.getElementById("<%=txtGstRate.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                     document.getElementById("<%=lblGstRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                     document.getElementById("<%=txtGstRate.ClientID %>").focus();
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
        function SB(billno) {
            var billno = document.getElementById('<%=txtDoc_No.ClientID %>').value;

            window.open('../Report/rptExportInvice.aspx?billno=' + billno)

        }
        function Focusbtn(e) {
            debugger;

            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }

        //        function part(e) {
        //            if (e.keyCode == 112) {
        //                debugger;

        //                e.preventDefault();
        //                $("#<%=pnlPopup.ClientID %>").show();
        //                $("#<%=btntxtPart_no.ClientID %>").click();

        //            }
        //            if (e.keyCode == 9) {

        //                var partno = $("#<%=txtPart_no.ClientID %>").val();

        //                partno = "0" + partno;
        //                $("#<%=txtPart_no.ClientID %>").val(partno);


        //                __doPostBack("txtPart_no", "TextChanged");



        //            }

        //        }

        function newpart(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtPart_no.ClientID %>").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtPart_no.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtPart_no.ClientID %>").val(buss);
                __doPostBack("txtPart_no", "TextChanged");
            }
        }
        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

            }
        }
        function ShipTo(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtShipToCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtShipTo_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtShipTo_Code.ClientID %>").val(unit);
                __doPostBack("txtShipTo_Code", "TextChanged");

            }

        }
        function Consign(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtConsigncode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtConsign_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtConsign_code.ClientID %>").val(unit);
                __doPostBack("txtConsign_code", "TextChanged");

            }

        }
        function gstcode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGstRate.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGstRate.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGstRate.ClientID %>").val(unit);
                __doPostBack("txtGstRate", "TextChanged");

            }
        }

    </script>
    <script type="text/javascript">
        function disableClick(elem) {
            elem.disabled = true;
        }
    </script>
    <script type="text/javascript">
        function BACK() {

            window.open('../Outword/pgeExport_InvoiceUtility.aspx', '_self');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Export Invoice " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfshipto" runat="server" />
            <asp:HiddenField ID="hdnfconsign" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <%-- <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                        &nbsp;--%>
                        <asp:Button OnClientClick="disableClick(this)" OnClick="btnSave_Click" runat="server"
                            Text="Save" UseSubmitBehavior="false" ID="btnSave" CssClass="btnHelp" ValidationGroup="add"
                            Width="90px" Height="24px" />
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
                        &nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" />
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
                        <td align="left">Change No
                                <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                    onKeyDown="changeno(event);"></asp:TextBox>
                            Invoice No
                                <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                            <asp:Button Width="80px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Button Width="120px" Height="24px" ID="btnCarryForword" runat="server" Text="Carry Forword"
                                OnClick="btnCarryForword_Click" CssClass="btnHelp" />
                            Date
                                <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDoc_Date_TextChanged"
                                    onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                    runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            Country of Final Destination
                                <asp:TextBox Height="24px" ID="txtFinal_Dest" runat="Server" CssClass="txt" TabIndex="4"
                                    Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFinal_Dest_TextChanged"></asp:TextBox>
                            Pre Carriage
                                <asp:TextBox Height="24px" ID="txtPer_Carriage" runat="Server" CssClass="txt" TabIndex="5"
                                    Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPer_Carriage_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Order No
                                <asp:TextBox Height="50px" ID="txtOrder_No" runat="Server" CssClass="txt" TabIndex="6"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtOrder_No_TextChanged"
                                    TextMode="MultiLine" MaxLength="255" Font-Bold="true" Font-Size="15px"></asp:TextBox>
                            ShipTo Code 
                                 <asp:TextBox ID="txtShipTo_Code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                     Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtShipTo_Code_TextChanged"
                                     onKeyDown="ShipTo(event);"></asp:TextBox>
                            <asp:Button ID="btntxtShipToCode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtShipToCode_Click" />
                            <asp:TextBox Height="50px" ID="txtShip_To" runat="Server" CssClass="txt" TabIndex="7"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtShip_To_TextChanged"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            Consign
                                 <asp:TextBox ID="txtConsign_code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                     Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtConsign_code_TextChanged"
                                     onKeyDown="Consign(event);"></asp:TextBox>
                            <asp:Button ID="btntxtConsigncode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtConsigncode_Click" />
                            <asp:TextBox Height="50px" ID="txtConsign" runat="Server" CssClass="txt" TabIndex="8"
                                Width="400px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtConsign_TextChanged"
                                TextMode="MultiLine" MaxLength="255" Font-Bold="true" Font-Size="15px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Country Of Origin
                             <asp:TextBox Height="24px" ID="txtcountryorigin" runat="Server" CssClass="txt"
                                 TabIndex="9" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtcountryorigin_TextChanged"></asp:TextBox>
                            MAEQ
                                <asp:TextBox Height="24px" ID="txtMAEQ" runat="Server" CssClass="txt" TabIndex="10"
                                    Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMAEQ_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Place Of Receipt
                                <asp:TextBox Height="24px" ID="txtPlace_Of_Receipt" runat="Server" CssClass="txt"
                                    TabIndex="9" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPlace_Of_Receipt_TextChanged"></asp:TextBox>
                            Vessel/Flight No
                                <asp:TextBox Height="24px" ID="txtVessel" runat="Server" CssClass="txt" TabIndex="10"
                                    Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtVessel_TextChanged"></asp:TextBox>
                            Port Of Loading
                                <asp:TextBox Height="24px" ID="txtPort_Of_Loading" runat="Server" CssClass="txt"
                                    TabIndex="11" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPort_Of_Loading_TextChanged"></asp:TextBox>
                            Port Of Discharage
                                <asp:TextBox Height="24px" ID="txtPort_Of_Discharage" runat="Server" CssClass="txt"
                                    TabIndex="12" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPort_Of_Discharage_TextChanged"></asp:TextBox>
                            Final Destination
                                <asp:TextBox Height="24px" ID="txtFinal_Destination" runat="Server" CssClass="txt"
                                    TabIndex="13" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFinal_Destination_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Terms
                                <asp:TextBox Height="60px" ID="txtTerms" runat="Server" CssClass="txt" TabIndex="14"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTerms_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            Category
                                <asp:TextBox Height="60px" ID="txtCategory" runat="Server" CssClass="txt" TabIndex="15"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCategory_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            To
                                <asp:TextBox Height="60px" ID="txtToo" runat="Server" CssClass="txt" TabIndex="16"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtToo_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Number and kinds of bags
                                <asp:TextBox Height="24px" ID="txtNo_Of_Box" runat="Server" CssClass="txt" TabIndex="17"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNo_Of_Box_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                TargetControlID="txtNo_Of_Box">
                            </ajax1:FilteredTextBoxExtender>
                            HSN Code
                                <asp:TextBox Height="24px" ID="txtRITC_Code" runat="Server" CssClass="txt" TabIndex="18"
                                    Width="400px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRITC_Code_TextChanged"></asp:TextBox>
                            I.E. Code No
                                <asp:TextBox Height="24px" ID="txtIE_Code" runat="Server" CssClass="txt" TabIndex="19"
                                    Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIE_Code_TextChanged"></asp:TextBox>
                            Net Wt Incrise By
                                <asp:TextBox Height="24px" ID="txtNet_Wt_Incr" runat="Server" CssClass="txt" TabIndex="20"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNet_Wt_Incr_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kind Attention
                                <asp:TextBox Height="60px" ID="txtKind_Attention" runat="Server" CssClass="txt" TabIndex="21"
                                    Width="280px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtKind_Attention_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            Our Invoice Remark
                                <asp:TextBox Height="60px" ID="txtOur_Inv_Rmk" runat="Server" CssClass="txt" TabIndex="22"
                                    Width="280px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtOur_Inv_Rmk_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>DrawBack
                            <asp:DropDownList ID="drpDrawBack" runat="server" AutoPostBack="true" CssClass="ddl"
                                Height="25px" OnSelectedIndexChanged="drpDrawBack_SelectedIndexChanged" TabIndex="1"
                                Width="100px">
                                <asp:ListItem Selected="True" Text="LUT" Value="L"></asp:ListItem>
                                <asp:ListItem Text="Duty Drawback" Value="D"></asp:ListItem>
                            </asp:DropDownList>
                            LUT No
                                <asp:TextBox Height="60px" ID="txtLUT_No" runat="Server" CssClass="txt" TabIndex="23"
                                    Width="280px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtLUT_No_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" TabIndex="28"
                                Visible="false" />
                              GST Code:
                                <asp:TextBox ID="txtGstRate" runat="Server" CssClass="txt" TabIndex="8" Width="80px"
                                    onkeydown="gstcode(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtGstRate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGstRate" runat="server" Text="..." OnClick="btntxtGstRate_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblGstRateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tel. No
                                <asp:TextBox Height="24px" ID="txtTele_No" runat="Server" CssClass="txt" TabIndex="24"
                                    Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTele_No_TextChanged"></asp:TextBox>
                             Rate
                                <asp:TextBox Height="24px" ID="txtEuro_Rate" runat="Server" CssClass="txt" TabIndex="25"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEuro_Rate_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender5" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtEuro_Rate">
                            </ajax1:FilteredTextBoxExtender>
                            Amount in Rs
                                <asp:TextBox Height="24px" ID="txtAmount_In_Rs" runat="Server" CssClass="txt" TabIndex="26"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAmount_In_Rs_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtAmount_In_Rs">
                            </ajax1:FilteredTextBoxExtender>
                            Currency Type
                             <asp:DropDownList ID="drpCurrencytype" runat="server" AutoPostBack="true" CssClass="ddl"
                                Height="25px" OnSelectedIndexChanged="drpCurrencytype_SelectedIndexChanged" TabIndex="1"
                                Width="100px">
                                <asp:ListItem Selected="True" Text="Euro" Value="Euro"></asp:ListItem>
                                <asp:ListItem Text="Dollar" Value="Dollar"></asp:ListItem>
                                 <asp:ListItem Text="Pound" Value="Pound"></asp:ListItem>
                                 <asp:ListItem Text="Rupees" Value="R"></asp:ListItem>
                                 <asp:ListItem Text="Brazilian real" Value="Brazilian real"></asp:ListItem>
                            </asp:DropDownList>
                               <%-- <asp:TextBox Height="24px" ID="txtCurrent_type" runat="Server" CssClass="txt" TabIndex="27"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCurrent_type_TextChanged"></asp:TextBox>--%>
                            Currency In Word
                                <asp:TextBox Height="24px" ID="txtCurrencyInWord" runat="Server" CssClass="txt" TabIndex="28"
                                    Width="450px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCurrencyInWord_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">
                    <table width="100%" align="Left">
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
                            <td align="left">Container No
                                <asp:TextBox ID="txtBox_No" runat="Server" CssClass="txt" TabIndex="29" Width="90px"
                                    Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtBox_No_TextChanged"
                                    Height="24px" onKeyDown="Focusbtn(event);"></asp:TextBox>
                               <%-- <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                    TargetControlID="txtBox_No">
                                </ajax1:FilteredTextBoxExtender>--%>
                                Details
                                <asp:TextBox ID="txtBox_Size" runat="Server" CssClass="txt" TabIndex="30" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBox_Size_TextChanged"
                                    Height="24px"></asp:TextBox>
                                Item Code
                                <asp:TextBox ID="txtPart_no" runat="Server" CssClass="txt" TabIndex="31" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPart_no_TextChanged"
                                    Height="24px" onkeydown="newpart(event);"></asp:TextBox>
                                <asp:Button ID="btntxtPart_no" runat="server" Text="..." OnClick="btntxtPart_no_Click"
                                    CssClass="btnHelp" />
                                <asp:Label ID="lblPart_no" runat="server" CssClass="lblName"></asp:Label>
                                Qty
                                <asp:TextBox ID="txtQty" runat="Server" CssClass="txt" TabIndex="32" Width="90px"
                                    Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                    TargetControlID="txtQty">
                                </ajax1:FilteredTextBoxExtender>
                                Weight
                                <asp:TextBox ID="txtWeight" runat="Server" CssClass="txt" TabIndex="33" Width="90px"
                                    Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtWeight_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredtxtCGST" FilterType="Custom,Numbers"
                                    ValidChars="." TargetControlID="txtWeight">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Net Wt
                                <asp:TextBox ID="txtNet_Wt" runat="Server" CssClass="txt" TabIndex="34" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNet_Wt_TextChanged"
                                    Height="24px" ReadOnly="true" Enabled="false"></asp:TextBox>
                                Gross Wt
                                <asp:TextBox ID="txtGross_Wt" runat="Server" CssClass="txt" TabIndex="35" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGross_Wt_TextChanged"
                                    Height="24px" ReadOnly="false" Enabled="false"></asp:TextBox>
                                Item Rate
                                <asp:TextBox ID="txtItem_Rate" runat="Server" CssClass="txt" TabIndex="36" Width="90px"
                                    Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtItem_Rate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" FilterType="Custom,Numbers"
                                    ValidChars="." TargetControlID="txtItem_Rate">
                                </ajax1:FilteredTextBoxExtender>
                                Item Value
                                <asp:TextBox ID="txtItem_Value" runat="Server" CssClass="txt" TabIndex="37" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtItem_Value_TextChanged"
                                    Height="24px" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnAdddetails_Click" TabIndex="38" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnClosedetails_Click" TabIndex="39" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <div style="width: 100%; position: relative; margin-top: 0px;">
                    <asp:UpdatePanel ID="upGrid" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="250px" Width="1300px"
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
                <table width="100%" style="float: left; font-weight: bold; font-family: Verdana; margin-top: 5px;">
                    <tr>
                        <td align="left">Export Declaration: 
                <asp:TextBox runat="server" ID="txtExportDelaration" Width="300" Height="60" TextMode="MultiLine"></asp:TextBox>

                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                        <td>Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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
                BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

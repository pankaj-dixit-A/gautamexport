<%@ Page Title="Export Invoice" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeExportInvoice.aspx.cs" Inherits="Sugar_Export_pgeExportInvoice" %>

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

                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    //document.getElementById("<%=txtAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Address.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBuyer_code") {
                    document.getElementById("<%=txtBuyer_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    //document.getElementById("<%=txtBuyer_code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBuyer_Address.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtBuyer_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstRate") {
                    document.getElementById("<%=txtGstRate.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGstRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGstRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtManufacturer_code") {
                    document.getElementById("<%=txtManufacturer_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=txtManufacturer_Detail.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtManufacturer_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNotifyPartyCode1") {
                    document.getElementById("<%=txtNotifyPartyCode1.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=txtNotify_Party1.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtNotifyPartyCode1.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNotifyPartyCode2") {
                    document.getElementById("<%=txtNotifyPartyCode2.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    var shiptoname = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    shiptoname = shiptoname + " " + grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=txtNotify_Party2.ClientID %>").innerText = shiptoname
                    document.getElementById("<%=txtNotifyPartyCode2.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLITEMNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtsalebill_No") {
                    document.getElementById("<%=txtsalebill_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                      document.getElementById("<%=lblsalebill_Id.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                      document.getElementById("<%=txtsalebill_No.ClientID %>").focus();
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
            var SB_NO = document.getElementById('<%=txtsalebill_No.ClientID %>').value;

            window.open('../Report/rptExportInviceNew.aspx?billno=' + billno + '&SB_NO=' + SB_NO)

        }
        function Focusbtn(e) {
            debugger;

            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
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
                $("#<%=btntxtAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=btntxtAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=btntxtAc_Code.ClientID %>").val(unit);
                __doPostBack("txtAc_Code", "TextChanged");

            }

        }
        function Manufacturer(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtManufacturer_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtManufacturer_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtManufacturer_code.ClientID %>").val(unit);
                __doPostBack("txtManufacturer_code", "TextChanged");

            }

        }
        function Consign(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBuyer_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=btntxtBuyer_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=btntxtBuyer_code.ClientID %>").val(unit);
                __doPostBack("txtBuyer_code", "TextChanged");

            }

        }
        function NotifyParty1(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNotifyPartyCode1.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=btntxtNotifyPartyCode1.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=btntxtNotifyPartyCode1.ClientID %>").val(unit);
                __doPostBack("txtNotifyPartyCode1", "TextChanged");

            }

        }
        function NotifyParty2(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNotifyPartyCode2.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=btntxtNotifyPartyCode2.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=btntxtNotifyPartyCode2.ClientID %>").val(unit);
                __doPostBack("txtNotifyPartyCode2", "TextChanged");

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
        function Item(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtITEM_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtITEM_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtITEM_CODE.ClientID %>").val(unit);
                __doPostBack("txtITEM_CODE", "TextChanged");

            }


        }
        function salebill_No(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnsalebill_No.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtsalebill_No.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtsalebill_No.ClientID %>").val(unit);
                __doPostBack("txtsalebill_No", "TextChanged");

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
                      
                        <asp:Button OnClientClick="disableClick(this)" OnClick="btnSave_Click" runat="server"
                            Text="Save" UseSubmitBehavior="false" ID="btnSave" CssClass="btnHelp" ValidationGroup="add"
                            Width="90px" Height="24px" TabIndex="41" />
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
                            IEC Code
                                <asp:TextBox Height="24px" ID="txtIECCode" runat="Server" CssClass="txt" TabIndex="4"
                                    Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIECCode_TextChanged"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                              Buyer Code
                                 <asp:TextBox ID="txtBuyer_code" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                     Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtBuyer_code_TextChanged"
                                     onKeyDown="Consign(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBuyer_code" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtBuyer_code_Click" />
                            <asp:TextBox Height="50px" ID="txtBuyer_Address" runat="Server" CssClass="txt" TabIndex="8"
                                Width="400px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBuyer_Address_TextChanged"
                                TextMode="MultiLine" MaxLength="255" Font-Bold="true" Font-Size="15px"></asp:TextBox>
                            Sale Bill No:
                                  <asp:TextBox ID="txtsalebill_No" runat="Server" CssClass="txt" TabIndex="4" Width="90px"
                                    onkeydown="salebill_No(event);" Style="text-align: right;" AutoPostBack="true" Height="24px"
                                    OnTextChanged="txtsalebill_No_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnsalebill_No" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btnsalebill_No_Click" />
                                <asp:Label ID="lblsalebill_Id" runat="server" CssClass="lblName"></asp:Label>
                            Ac Code 
                                 <asp:TextBox ID="txtAc_Code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                     Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtAC_Code_TextChanged"
                                     onKeyDown="ShipTo(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAc_Code" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtAc_Code_Click" />
                            <asp:TextBox Height="50px" ID="txtAc_Address" runat="Server" CssClass="txt" TabIndex="6"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAc_Address_TextChanged"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                          
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Notify Party1
                            <asp:TextBox ID="txtNotifyPartyCode1" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtNotifyPartyCode1_TextChanged"
                                onKeyDown="NotifyParty1(event);"></asp:TextBox>
                            <asp:Button ID="btntxtNotifyPartyCode1" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtNotifyPartyCode1_Click" />
                            <asp:TextBox Height="50px" ID="txtNotify_Party1" runat="Server" CssClass="txt" TabIndex="10"
                                Width="400px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNotify_Party1_TextChanged"
                                TextMode="MultiLine" MaxLength="255" Font-Bold="true" Font-Size="15px"></asp:TextBox>
                            Notify Party2
                              <asp:TextBox ID="txtNotifyPartyCode2" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                  Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtNotifyPartyCode2_TextChanged"
                                  onKeyDown="NotifyParty2(event);"></asp:TextBox>
                            <asp:Button ID="btntxtNotifyPartyCode2" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtNotifyPartyCode2_Click" />
                            <asp:TextBox Height="50px" ID="txtNotify_Party2" runat="Server" CssClass="txt" TabIndex="12"
                                Width="400px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNotify_Party2_TextChanged"
                                TextMode="MultiLine" MaxLength="255" Font-Bold="true" Font-Size="15px"></asp:TextBox>
                            Country Of Origin
                             <asp:TextBox Height="24px" ID="txtcountryorigin" runat="Server" CssClass="txt"
                                 TabIndex="13" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtcountryorigin_TextChanged"></asp:TextBox>
                            Final Destination
                                <asp:TextBox Height="24px" ID="txtFinal_Destination" runat="Server" CssClass="txt"
                                    TabIndex="14" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFinal_Destination_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Pre Carriage
                                <asp:TextBox Height="24px" ID="txtPer_Carriage" runat="Server" CssClass="txt" TabIndex="15"
                                    Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPer_Carriage_TextChanged"></asp:TextBox>
                            Place Of Receipt
                                <asp:TextBox Height="24px" ID="txtPlace_Of_Receipt" runat="Server" CssClass="txt"
                                    TabIndex="16" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPlace_Of_Receipt_TextChanged"></asp:TextBox>
                            Vessel/Flight No
                                <asp:TextBox Height="24px" ID="txtVessel" runat="Server" CssClass="txt" TabIndex="17"
                                    Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtVessel_TextChanged"></asp:TextBox>
                            Port Of Loading
                                <asp:TextBox Height="24px" ID="txtPort_Of_Loading" runat="Server" CssClass="txt"
                                    TabIndex="18" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPort_Of_Loading_TextChanged"></asp:TextBox>
                            Terms
                                <asp:TextBox Height="60px" ID="txtTerms" runat="Server" CssClass="txt" TabIndex="17"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTerms_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td align="left">Export Declaration: 
                <asp:TextBox runat="server" ID="txtExportDelaration" Width="300" Height="60" TextMode="MultiLine"></asp:TextBox>
                            Country of Final Destination
                                <asp:TextBox Height="24px" ID="txtCountryFinalDestination" runat="Server" CssClass="txt"
                                    TabIndex="20" Width="150px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Port of Discharge
                                <asp:TextBox Height="24px" ID="txtPort_of_Discharge" runat="Server" CssClass="txt"
                                    TabIndex="21" Width="150px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Subsidies
                                <asp:TextBox Height="24px" ID="txtSubsidies" runat="Server" CssClass="txt"
                                    TabIndex="22" Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine" MaxLength="255"></asp:TextBox>


                        </td>
                    </tr>
                    <tr>
                        <td align="left">MAEQ
                                <asp:TextBox Height="24px" ID="txtMAEQ" runat="Server" CssClass="txt" TabIndex="23"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMAEQ_TextChanged" TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            GST No
                                <asp:TextBox Height="24px" ID="txtGSTNo" runat="Server" CssClass="txt" TabIndex="24"
                                    Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGSTNo_TextChanged"></asp:TextBox>
                            Marks & Nos
                                <asp:TextBox Height="24px" ID="txtMarksAndNos" runat="Server" CssClass="txt" TabIndex="25"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMarksAndNos_TextChanged" TextMode="MultiLine" MaxLength="255"></asp:TextBox>


                            Foregin Type
                             <asp:DropDownList ID="drpCurrencytype" runat="server" AutoPostBack="true" CssClass="ddl"
                                 Height="25px" OnSelectedIndexChanged="drpCurrencytype_SelectedIndexChanged" TabIndex="26"
                                 Width="100px">
                                 <asp:ListItem Selected="True" Text="Euro" Value="Euro"></asp:ListItem>
                                 <asp:ListItem Text="Dollar" Value="Dollar"></asp:ListItem>
                                 <asp:ListItem Text="Pound" Value="Pound"></asp:ListItem>
                                 <asp:ListItem Text="Rupees" Value="R"></asp:ListItem>
                                 <asp:ListItem Text="Brazilian real" Value="Brazilian real"></asp:ListItem>
                             </asp:DropDownList>
                            Item:
                                <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtITEM_CODE_TextChanged"
                                    onKeyDown="Item(event);"></asp:TextBox>
                            <asp:Button ID="btntxtITEM_CODE" runat="server" Text="..." OnClick="btntxtITEM_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLITEMNAME" runat="server" CssClass="lblName"></asp:Label>
                            HSN
                                <asp:TextBox Height="24px" ID="txtHSN" runat="Server" CssClass="txt"
                                    TabIndex="27" Width="150px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Particular
                                <asp:TextBox Height="60px" ID="txtParticular" runat="Server" CssClass="txt" TabIndex="28"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtParticular_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>

                            Supplier Detail
                                <asp:TextBox Height="60px" ID="txtSupplier_Detail" runat="Server" CssClass="txt" TabIndex="29"
                                    Width="280px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSupplier_Detail_TextChanged"
                                    TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            Manufacturer Detail
                            <asp:TextBox ID="txtManufacturer_code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Style="text-align: right;" AutoPostBack="true" Height="24px" OnTextChanged="txtManufacturer_code_TextChanged"
                                onKeyDown="Manufacturer(event);"></asp:TextBox>
                            <asp:Button ID="btntxtManufacturer_code" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtManufacturer_code_Click" />
                            <asp:TextBox Height="60px" ID="txtManufacturer_Detail" runat="Server" CssClass="txt" TabIndex="30"
                                Width="280px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtManufacturer_Detail_TextChanged"
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                            GST Code:
                                <asp:TextBox ID="txtGstRate" runat="Server" CssClass="txt" TabIndex="31" Width="80px"
                                    onkeydown="gstcode(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtGstRate_TextChanged"
                                    Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtGstRate" runat="server" Text="..." OnClick="btntxtGstRate_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGstRateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <td align="left"> Plant Name:
                            <asp:TextBox Height="60px" ID="txtplantName" runat="Server" CssClass="txt" TabIndex="30"
                                Width="280px" Style="text-align: left;" AutoPostBack="false"  
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox> 
                          Plant Code:
                            <asp:TextBox ID="txtplantCode" runat="Server" CssClass="txt" TabIndex="31" Width="80px"
                              Style="text-align: right;" AutoPostBack="false" Height="24px"></asp:TextBox>
                          Title Narration:
                            <asp:TextBox Height="60px" ID="txttitleNarration" runat="Server" CssClass="txt" TabIndex="30"
                                Width="280px" Style="text-align: left;" AutoPostBack="false"  
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox> 
                            Narration:
                            <asp:TextBox Height="60px" ID="txtNarration" runat="Server" CssClass="txt" TabIndex="30"
                                Width="280px" Style="text-align: left;" AutoPostBack="false"  
                                TextMode="MultiLine" MaxLength="255"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Quantl
                                <asp:TextBox ID="txtQty" runat="Server" CssClass="txt" TabIndex="32" Width="90px"
                                    Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"
                                    Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtQty" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Bags Perkg
                                <asp:TextBox Height="24px" ID="txtBags_Perkg" runat="Server" CssClass="txt" TabIndex="33"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBags_Perkg_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtBags_Perkg">
                            </ajax1:FilteredTextBoxExtender>
                            Number and kinds of bags
                                <asp:TextBox Height="24px" ID="txtNo_Of_bags" runat="Server" CssClass="txt" TabIndex="34"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNo_Of_bags_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtNo_Of_bags" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Net Wt Incrise By
                                <asp:TextBox Height="24px" ID="txtNet_Wt_Incr" runat="Server" CssClass="txt" TabIndex="35"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_Wt_Incr_TextChanged"></asp:TextBox>

                            Net Wt
                                <asp:TextBox ID="txtNet_Wt" runat="Server" CssClass="txt" TabIndex="36" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNet_Wt_TextChanged"
                                    Height="24px" ReadOnly="false" Enabled="false"></asp:TextBox>
                            Gross Wt
                                <asp:TextBox ID="txtGross_Wt" runat="Server" CssClass="txt" TabIndex="37" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGross_Wt_TextChanged"
                                    Height="24px" ReadOnly="false" Enabled="false"></asp:TextBox>
                            Rate
                                <asp:TextBox ID="txtRate_Foregin" runat="Server" CssClass="txt" TabIndex="38" Width="90px"
                                    Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtRate_Foregin_TextChanged"
                                    Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtRate_Foregin">
                            </ajax1:FilteredTextBoxExtender>
                            Amount
                                <asp:TextBox ID="txtAmount" runat="Server" CssClass="txt" TabIndex="39" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAmount_TextChanged"
                                    Height="24px" ReadOnly="false" Enabled="false"></asp:TextBox>
                            Report Type
                             <asp:DropDownList ID="drpReportType" runat="server" AutoPostBack="true" CssClass="ddl"
                                 Height="25px" OnSelectedIndexChanged="drpReportType_SelectedIndexChanged" TabIndex="40"
                                 Width="100px">
                                 <asp:ListItem Selected="True" Text="Qntl" Value="Qntl"></asp:ListItem>
                                 <asp:ListItem Text="MT" Value="MT"></asp:ListItem>

                             </asp:DropDownList>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExport_Ledger.aspx.cs"
    Inherits="Foundman_Report_rptExport_Ledger"  EnableEventValidation="false" Title=" Export Ledger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Ledger</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1365');
            printWindow.document.write('<html><head><link href="../../print.css" media="print" rel="Stylesheet" type="text/css" />');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        function DN() {
            window.open('../Master/pgeDebitCreditNote.aspx');
        }
        function OV() {
            window.open('../Sugar/pgeLoadingvoucher.aspx');
        }
        function DO() {
            window.open('../Sugar/pgeDeliveryOrderForGST.aspx');

        }
        function DOOld() {
            window.open('../Sugar/pgeDeliveryOrder2.aspx');
        }
        function PD() {
            window.open('../Transaction/pgeCashPaid.aspx');
        }

        function SB() {
            window.open('../Outword/pgeGSTInvoice.aspx');
        }



        function JV() {
            window.open('../Transaction/pgeJournalvoucher.aspx');
        }


        function RP() {
            window.open('../Transaction/pgeRecieptPayment.aspx');

        }
        function CQ() {
            window.open('../Transaction/pgeCheckPrinting.aspx');

        }
        function RC() {
            window.open('../Inword/pgeRCM.aspx');
        }
        function PB() {
            window.open('../Inword/pgePurchase.aspx');
        }
        function CE() {
            window.open('../Transaction/pgeCashExchange.aspx');
        }
        function APAR() {
            window.open('../Export/pgeExport_ApAr.aspx');
        }
        function EIN(accode, Action) {
           
            window.open('../Export/PgeExport_Invoice_New.aspx?Doc_No=' + accode + '&Action=' + Action);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 74%;
        }
        .style2
        {
            width: 21%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" OnClick="btnPrint_Click" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPDFDownload" runat="server" Width="110px" Text="PDF Download"
            OnClick="btnPDfDownload_Click" />
        <asp:Button ID="btnpdf" runat="server" Width="70px" Text="PDF Mail" OnClick="btnPDf_Click" />
        <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" CssClass="btnHelp"
            OnClick="btnSendEmail_Click" />&nbsp;&nbsp; Email:<asp:TextBox runat="server" ID="txtEmail"
                Width="300px"></asp:TextBox>&nbsp;&nbsp;<asp:Button runat="server" ID="btpGrdprint"
                    Text="PrintGrid" OnClick="btpGrdprint_Click" Visible="false" />
        <div align="right">
            <a href="javascript:;" id="scrollToBottom">&#x25BC;</a>
        </div>
    </div>
    <div>
        <asp:Panel runat="server" ID="PrintPanelmain" align="center" Font-Names="Calibri"
            Width="100%">
            <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
                <table width="68%" align="left" class="printwithmargin" id="tblMain" runat="server"
                    style="table-layout: fixed;" cellspacing="2">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" align="center">
                                <tr>
                                    <td align="left" style="width: 70%;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label1" runat="server" Text="Account Statement of :" Font-Bold="true"
                                            Font-Size="Medium"></asp:Label>
                                        <asp:Label ID="lblParty" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label><asp:Label
                                            ID="lblAcCode" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%;">
                                        <asp:Label ID="Label2" runat="server" Text="From:" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                        &nbsp; &nbsp;<asp:Label ID="lblFromDt" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="Label3" runat="server" Text="To:" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                        &nbsp;&nbsp;<asp:Label ID="lblToDt" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="150%">
                                <tr>
                                    <td align="left">
                                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="White"
                                            GridLines="Both" HeaderStyle-ForeColor="Black" HeaderStyle-Height="30px" EmptyDataText="No Records found"
                                            Width="900px" CellPadding="9" CellSpacing="0" Font-Bold="false" ForeColor="Black"
                                            RowStyle-Height="30px" ShowFooter="true" Font-Names="Verdana" Font-Size="Medium"
                                            OnRowDataBound="grdDetail_RowDataBound" CssClass="print" AllowPaging="false"
                                            PageSize="50" OnPageIndexChanging="grdDetails_PageIndexChanging">
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkDONOGo" OnClick="lnkDONOGo_Click" Style="color: Black;
                                                            text-decoration: none;" Text='<%#Eval("DONO") %>' ToolTip="Go To Document"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="TranType" HeaderText="Type" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="40px" HeaderStyle-CssClass="thead" />
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkGo" OnClick="lnkGo_Click" Style="color: Black;
                                                            text-decoration: none;" Text='<%#Eval("DocNo") %>' ToolTip="Go To Document"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="150px" HeaderStyle-CssClass="thead" />
                                                <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-VerticalAlign="Bottom" ItemStyle-Width="600px" HeaderStyle-CssClass="thead" />
                                                <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-HorizontalAlign="Right"
                                                    ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                    DataFormatString="{0:f2}" />
                                                <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-HorizontalAlign="Right"
                                                    ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                    DataFormatString="{0:f2}" />
                                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                                    ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                    DataFormatString="{0:f2}" />
                                                <asp:BoundField DataField="DrCr" HeaderText="DrCr" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="50px" HeaderStyle-CssClass="thead" />
                                                <%-- <asp:BoundField DataField="Do_No" HeaderText="Do_No" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="50px" HeaderStyle-CssClass="thead" />--%>
                                            </Columns>
                                            <RowStyle Height="10px" Wrap="true" ForeColor="Black" />
                                            <FooterStyle BackColor="Yellow" Font-Bold="true" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportInvice.aspx.cs" Inherits="Foundman_Report_rptExportInvice" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.2.61/jspdf.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnPDF" runat="server" Text="Open PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnPDF_Click" />
            <asp:Button ID="btnMail" runat="server" Text="Mail PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnMail_Click" />
            <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
            <CR:CrystalReportViewer ID="cryExportInvoice1" runat="server" AutoDataBind="true" BestFitPage="False" Width="1500px" />

            <br />

            <asp:Button ID="btnpdfpaking1" runat="server" Text="Open PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnpdfpaking1_Click" />
            <asp:Button ID="btnemailpacking1" runat="server" Text="Mail PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnemailpacking1_Click" />
            <asp:TextBox runat="server" ID="txtEmailpacking1" Width="300px"></asp:TextBox>
            <CR:CrystalReportViewer ID="cryExportInvoice2" runat="server" AutoDataBind="true" BestFitPage="False" Width="1500px" />

            <br />
            <asp:Button ID="btnpdfpaking2" runat="server" Text="Open PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnpdfpaking2_Click" />
            <asp:Button ID="btnemailpacking2" runat="server" Text="Mail PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnemailpacking2_Click" />
            <asp:TextBox runat="server" ID="txtEmailpacking2" Width="300px"></asp:TextBox>
            <CR:CrystalReportViewer ID="cryExportInvoice3" runat="server" AutoDataBind="true" BestFitPage="False" Width="1500px" />
        </div>
    </form>
</body>
</html>

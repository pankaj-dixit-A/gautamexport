using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Foundman_Report_pgeExport_GLedgerReport : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string AccountMasterTable = string.Empty;
    string searchStr = "";
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string searchString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "qryAccountsList";
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                BindContrydropdown();
                txtFromDt.Text = clsGV.Start_Date;
                txtToDt.Text = clsGV.To_date;
                drpFilter.SelectedValue = "A";
                pnlAcNameWise.Visible = true;
                pnlBSGroupWise.Visible = false;
                setFocusControl(txtAcCode);
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
           
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                
                lblPopupHead.Text = "--Select Ac name--";
                string qry = "select Ac_Code as Party_Code,Ac_Name_E as Party_name,GSTStateCode as Party_State from qrymstaccountmaster where ( Ac_Code Like '%" + txtSearchText.Text + "%'or Ac_Name_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
                
            }
            
            if (hdnfClosePopup.Value == "txtBSGroupCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Group--";
                string qry = "select [group_Code], [group_Name_E] as [Group Name] from " + tblPrefix + "BSGroupMaster  where (group_Code like '%" + txtSearchText.Text + "%' or group_Name_E like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                this.showPopup(qry);
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            //    hdnfClosePopup.Value = "Close";
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {

        }
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    protected void BindContrydropdown()
    {
        try
        {
            string qry = "Select [TRAN_TYPE] from [qryTranType] order by TRAN_TYPE";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    drptrantype.DataValueField = "TRAN_TYPE";
                    DataRow row = dt.NewRow();
                    row["TRAN_TYPE"] = "All";
                    //dt.Rows.Add(row);
                    dt.Rows.InsertAt(row, 0);
                    //drpsalebill.AppendDataBoundItems = false;
                    if (dt.Rows.Count > 0)
                    {
                        drptrantype.DataSource = dt;
                        //dt.Columns["TRAN_TYPE"].SetOrdinal(0);

                        drptrantype.DataBind();


                    }
                    else
                    {
                        drptrantype.DataSource = null;
                        drptrantype.DataBind();


                    }
                }
            }

        }
        catch
        {
        }
    }
    protected void btnAcCode_Click(object sender, EventArgs e)
    {
        try
        {
            searchStr = txtAcCode.Text;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAcCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtAcCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAcCode.Text != string.Empty)
            {
                searchStr = txtAcCode.Text;
                strTextbox = "txtAcCode";

                bool a = clsCommon.isStringIsNumeric(txtAcCode.Text);
                if (a == false)
                {
                    btnAcCode_Click(this, new EventArgs());
                }
                else
                {
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                    if (str != string.Empty)
                    {
                        lblAcCodeName.Text = str;
                        setFocusControl(btnGetData);
                    }
                    else
                    {
                        txtAcCode.Text = string.Empty;
                        lblAcCodeName.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }

    protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpFilter.SelectedValue == "A")
            {
                pnlAcNameWise.Visible = true;
                pnlBSGroupWise.Visible = false;
                setFocusControl(txtAcCode);
            }
            else
            {
                pnlAcNameWise.Visible = false;
                pnlBSGroupWise.Visible = true;
                setFocusControl(txtBSGroupCode);
            }
        }
        catch
        {

        }
    }

    protected void txtBSGroupCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBSGroupCode.Text != string.Empty)
            {
                searchStr = txtBSGroupCode.Text;
                strTextbox = "txtBSGroupCode";
                string str = clsCommon.getString("select [group_Name_E] from " + tblPrefix + "BSGroupMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [group_Code]=" + txtBSGroupCode.Text);
                if (str != string.Empty)
                {
                    lblGroupName.Text = str;
                    setFocusControl(txtFromDt);
                }
                else
                {
                    txtBSGroupCode.Text = string.Empty;
                    lblGroupName.Text = string.Empty;
                    setFocusControl(txtBSGroupCode);
                }
            }
        }
        catch
        {

        }
    }

    private void csCalculations()
    {
        try
        {

        }
        catch
        {

        }
    }

    protected void btnGroupCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBSGroupCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }

    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("250px");
            if (e.Row.Cells.Count > 2)
            {
                e.Row.Cells[2].Width = new Unit("100px");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
    #region [btnGetData_Click]
    protected void btnGetData_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            string fromdt1 = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string todt1 = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            if (drpFilter.SelectedValue == "A")
            {
                if (txtAcCode.Text == string.Empty)
                {
                    setFocusControl(txtAcCode);
                    return;
                }
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;

                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt1 + "','" + todt1 + "','DrCr')", true);
            }
            if (drpFilter.SelectedValue == "G")
            {
                string groupcode = txtBSGroupCode.Text;
                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp_multipleLedger('" + fromdt + "','" + todt + "','" + groupcode + "')", true);
            }
            else
            {
            }
        }
        catch (Exception eex)
        {

        }
    }
    #endregion
    protected void drptrantype_SelectedIndexChanged(object sender, EventArgs e)
    {

        //string trantype= drptrantype.sel
        //Sale_Bill = drpsalebill.SelectedValue.ToString();
        //showLastRecord();
    }
    #region [grdPopup_RowCreated]
    protected void grdPopup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
            e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;

                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return true;";

                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion
    private void showPopup(string qry)
    {
        try
        {

            this.setFocusControl(txtSearchText);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdPopup.DataSource = dt;
                        grdPopup.DataBind();

                        hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                        pnlPopup.Style["Display"] = "Block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();

                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["Display"] = "Block";
                    }
                }
            }
        }
        catch
        {

        }
    }
}
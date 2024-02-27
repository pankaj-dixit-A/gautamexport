using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;

public partial class Foundman_Outword_pgeExport_ApAr : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    int Detail_Id = 2;
    int Type = 3;
    //int Tran_type = 4;
    int Bill_No = 4;
    int Bill_Date = 5;
    int Bill_Amount = 6;
    int Pending = 7;
    int Recieved = 8;
    int Adjusted = 9;
    int Narration = 10;
    int Unique_Id = 11;
    int Rowaction = 12;
    int Srno = 13;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Export_BillTransaction_Head";
            tblDetails = "Export_BillTransaction_Detail";
            qryCommon = "qryExport_ApAr";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    if (Session["RB_NO"] != null)
                    {
                        hdnf.Value = Session["RB_NO"].ToString();
                        string trntype = Session["Tran_TYPE"].ToString();

                        drpTran_Type.SelectedValue = trntype;
                        qry = getDisplayQuery();
                        this.fetchRecord(qry);
                        this.enableDisableNavigateButtons();

                        if (Session["Allow"] == "Allow")
                        {
                            btnEdit.Enabled = false;
                            btnAdd.Enabled = false;
                            btnAuthentication.Visible = true;
                        }

                        else
                        {
                            btnAuthentication.Visible = false;
                        }

                        Session["RB_NO"] = null;
                        Session["Tran_TYPE"] = null;
                        Session["Allow"] = null;
                    }
                    else
                    {
                        btnAdd_Click(this, new EventArgs());
                        //this.showLastRecord();
                    }
                   
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
                if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
                {
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    pnlPopup.Style["display"] = "block";
                    objAsp = btnSearch;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where ";
                obj.code = "Doc_no";
                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    txtDoc_No.Text = ds.Tables[0].Rows[0][0].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [makeEmptyForm]
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                pnlPopup.Style["display"] = "none";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                drpTran_Type.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtLC_Number.Enabled = false;
                txtCash_Bank.Enabled = false;
                btntxtCash_Bank.Enabled = false;
                txtCheq_No.Enabled = false;
                txtCheq_Date.Enabled = false;
                CalendarExtenderDatetxtCheq_Date.Enabled = false;
                txtCheck_Amount.Enabled = false;
                txtBank_Date.Enabled = false;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtRemark.Enabled = false;
                txtType.Enabled = false;
                drpTran_Type.Enabled = true;
                txtBill_No.Enabled = false;
                btntxtBill_No.Enabled = false;
                txtBill_Date.Enabled = false;
                CalendarExtenderDatetxtBill_Date.Enabled = false;
                txtBill_Amount.Enabled = false;
                txtPending.Enabled = false;
                txtRecieved.Enabled = false;
                txtAdjusted.Enabled = false;
                txtNarration.Enabled = false;
                txtUnique_Id.Enabled = false;
                btnUpdateBillAdj.Visible = false;
                lblreceviedetot.Text = "";
                lblbalamt.Text = "";
            }
            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                lblreceviedetot.Text = "";
                lblbalamt.Text = "";
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();
                grdDetail.Enabled = true;
                ViewState["currentTable"] = null;
                txtType.Enabled = true;
                drpTran_Type.Enabled = false;
                txtBill_No.Enabled = true;
                lblBill_No.Text = string.Empty;
                btntxtBill_No.Enabled = true;
                txtBill_Date.Enabled = true;
                txtBill_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtBill_Date.Enabled = true;
                txtBill_Amount.Enabled = false;
                txtPending.Enabled = false;
                txtRecieved.Enabled = true;
                txtAdjusted.Enabled = true;
                txtNarration.Enabled = true;
                txtUnique_Id.Enabled = false;
                btnUpdateBillAdj.Visible = false;

                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtAc_Code.Enabled = true;
                lblAc_Code.Text = string.Empty;
                btntxtAc_Code.Enabled = true;
                txtLC_Number.Enabled = true;
                txtCash_Bank.Enabled = true;
                lblCash_Bank.Text = string.Empty;
                btntxtCash_Bank.Enabled = true;
                txtCheq_No.Enabled = true;
                txtCheq_Date.Enabled = true;
                txtCheq_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtCheq_Date.Enabled = true;
                txtCheck_Amount.Enabled = true;
                txtBank_Date.Enabled = true;
                txtBank_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtBank_Date.Enabled = true;
                txtRemark.Enabled = true;
                #region set Business logic for save
                #endregion
            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                drpTran_Type.Enabled = true;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtLC_Number.Enabled = false;
                txtCash_Bank.Enabled = false;
                btntxtCash_Bank.Enabled = false;
                txtCheq_No.Enabled = false;
                txtCheq_Date.Enabled = false;
                CalendarExtenderDatetxtCheq_Date.Enabled = false;
                txtCheck_Amount.Enabled = false;
                txtBank_Date.Enabled = false;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtRemark.Enabled = false;
                txtType.Enabled = false;
                btnUpdateBillAdj.Visible = false;

                txtBill_No.Enabled = false;
                btntxtBill_No.Enabled = false;
                txtBill_Date.Enabled = false;
                CalendarExtenderDatetxtBill_Date.Enabled = false;
                txtBill_Amount.Enabled = false;
                txtPending.Enabled = false;
                txtRecieved.Enabled = false;
                txtAdjusted.Enabled = false;
                txtNarration.Enabled = false;
                txtUnique_Id.Enabled = false;
                txtType.Text = string.Empty;

                txtBill_No.Text = string.Empty;
                btntxtBill_No.Enabled = false;
                txtBill_Date.Text = string.Empty;
                CalendarExtenderDatetxtBill_Date.Enabled = false;
                txtBill_Amount.Text = string.Empty;
                txtPending.Text = string.Empty;
                txtRecieved.Text = string.Empty;
                txtAdjusted.Text = string.Empty;
                txtNarration.Text = string.Empty;
                txtUnique_Id.Text = string.Empty;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                drpTran_Type.Enabled = false;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                txtLC_Number.Enabled = true;
                txtCash_Bank.Enabled = true;
                btntxtCash_Bank.Enabled = true;
                txtCheq_No.Enabled = true;
                txtCheq_Date.Enabled = true;
                CalendarExtenderDatetxtCheq_Date.Enabled = true;
                txtCheck_Amount.Enabled = true;
                txtBank_Date.Enabled = true;
                CalendarExtenderDatetxtBank_Date.Enabled = true;
                txtRemark.Enabled = true;
                txtType.Enabled = true;

                txtBill_No.Enabled = true;
                btntxtBill_No.Enabled = true;
                txtBill_Date.Enabled = true;
                CalendarExtenderDatetxtBill_Date.Enabled = true;
                txtBill_Amount.Enabled = false;
                txtPending.Enabled = false;
                txtRecieved.Enabled = true;
                txtAdjusted.Enabled = true;
                txtNarration.Enabled = true;
                txtUnique_Id.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btnUpdateBillAdj.Visible = true;
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion
    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(Doc_No) as Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "'";
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
                        hdnf.Value = dt.Rows[0]["Doc_No"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        //clsButtonNavigation.enableDisable("N");

                        if (recordExist == true)
                        {
                            btnAdd.Focus();
                            setFocusControl(drpTran_Type);
                        }
                        else                     //new code
                        {
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            this.makeEmptyForm("N");
                        }
                    }
                }
            }

        }
        catch
        {
        }
    }
    #endregion
    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        //#region enable disable previous next buttons
        //int RecordCount = 0;
        //string query = "";
        //query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() +
        //    "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +" and Tran_Type='" + drpTran_Type.SelectedValue + "'";
        //string cnt = clsCommon.getString(query);
        //if (cnt != string.Empty)
        //{
        //    RecordCount = Convert.ToInt32(cnt);
        //}
        //if (RecordCount != 0 && RecordCount == 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = false;
        //}
        //else if (RecordCount != 0 && RecordCount > 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = true;
        //}
        //if (txtDoc_No.Text != string.Empty)
        //{
        //    #region check for next or previous record exist or not
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code='"
        //        + Convert.ToInt32(Session["Company_Code"].ToString()) +
        //        "'  and Year_Code=" + Convert.ToInt32(Session["year"].ToString())+ " and Tran_Type='" + drpTran_Type.SelectedValue + "' ORDER BY Doc_No asc  ";
        //    string strDoc_No = clsCommon.getString(query);
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //    }
        //    else
        //    {
        //        btnNext.Enabled = false;
        //        btnLast.Enabled = false;
        //    }
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +" and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        //         "' and Tran_Type='" + drpTran_Type.SelectedValue + "' ORDER BY Doc_No desc  ";
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnPrevious.Enabled = true;
        //        btnFirst.Enabled = true;
        //    }
        //    else
        //    {
        //        btnPrevious.Enabled = false;
        //        btnFirst.Enabled = false;
        //    }
        //}
        //    #endregion
        //#endregion

        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() +
            "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "'";

        //query = "select count(*) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTrnType.SelectedValue + "'";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(query);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RecordCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
        }
        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
        }
        if (RecordCount > 0)
        {
            if (txtDoc_No.Text != string.Empty)
            {
                if (hdnf.Value != string.Empty)
                {
                    #region check for next or previous record exist or not
                    ds = new DataSet();
                    dt = new DataTable();
                    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) +
                        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "' ORDER BY Doc_No asc  ";
                    ds = clsDAL.SimpleQuery(query);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //next record exist
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;
                            }
                            else
                            {
                                //next record does not exist
                                btnNext.Enabled = false;
                                btnLast.Enabled = false;
                            }
                        }
                    }
                    ds = new DataSet();
                    dt = new DataTable();
                    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) +
                        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and Tran_Type='" + drpTran_Type.SelectedValue + "' ORDER BY Doc_No asc  ";
                    ds = clsDAL.SimpleQuery(query);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //previous record exist
                                btnPrevious.Enabled = true;
                                btnFirst.Enabled = true;
                            }
                            else
                            {
                                btnPrevious.Enabled = false;
                                btnFirst.Enabled = false;
                            }
                        }
                    }

                    #endregion
                }
            }
            this.makeEmptyForm("S");
        }
        else
        {
            this.makeEmptyForm("N");
        }
        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead +
                " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "')";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion
    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                      "'  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "'" + " ORDER BY Doc_No desc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Next]
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "'  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "' ORDER BY Doc_No asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Last]
    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead +
                " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Tran_Type='" + drpTran_Type.SelectedValue + "')";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string check = txtEditDoc_No.Text;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
            setFocusControl(txtDoc_Date);
            pnlPopupDetails.Style["display"] = "none";
            Int32 Doc_No = Convert.ToInt32(clsCommon.getString("select IDENT_CURRENT('" + tblHead + "') as Doc_No"));
            if (Doc_No != 0)
            {
                int doc_no = Doc_No + 1;
                Doc_No = doc_no;
            }
            else
            {
                Doc_No = 1;
            }
            txtDoc_No.Text = Convert.ToString(Doc_No);
        }
        else
        {
            btnCancel_Click(this, new EventArgs());
        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;

        string qry = getDisplayQuery();
        fetchRecord(qry);
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_Date);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {

                string str = string.Empty;
                //int userid = Convert.ToInt32(Session["User_Id"].ToString());
                //string pagevalidation = clsCommon.getString("Select Permission from tblUser_Detail where Tran_Type ='" + drpTran_Type.SelectedValue + "' and User_Id=" + userid + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString());
                //if (pagevalidation == "Y")
                //{

                //}
                //else
                //{
                //    setFocusControl(txtDoc_Date);
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You are Not Authorize Person to Delete Record please Contact Admistrator!!!!!');", true);
                //    return;
                //}
                DataSet xml_ds = new DataSet();
                if (str == string.Empty)
                {
                    string currentDoc_No = txtDoc_No.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                        string strRev = string.Empty;
                        child1.SetAttributeValue("Doc_No", txtDoc_No.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);
                        child1.SetAttributeValue("Year_Code", Year_Code);
                        child1.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
                        child1.SetAttributeValue("Authentication", user + "Deleted");

                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_Export_BillTransaction_Head";
                        string xmlfile = XMLReport;
                        string op = "";
                        string returnmaxno = "";
                        int flag = 10;
                        xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
                        strrev = op;





                    }
                    string query = "";
                    if (strrev == "-3")
                    {
                        //query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
                        //hdnf.Value = clsCommon.getString(query);
                        //if (hdnf.Value == string.Empty)
                        //{
                        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
                        //    hdnf.Value = clsCommon.getString(query);
                        //}
                        //if (hdnf.Value != string.Empty)
                        //{
                        //    query = getDisplayQuery();
                        //    bool recordExist = this.fetchRecord(query);
                        //    this.makeEmptyForm("S");
                        //    clsButtonNavigation.enableDisable("S");
                        //}
                        //else
                        //{
                        //    this.makeEmptyForm("N");
                        //    clsButtonNavigation.enableDisable("N");
                        //    btnEdit.Enabled = false;
                        //    btnDelete.Enabled = false;
                        //}
                        DataTable dt_dalete = xml_ds.Tables[0];
                        string HDNF_VALUE = "";
                        if (dt_dalete.Rows.Count > 0)
                        {
                            HDNF_VALUE = dt_dalete.Rows[0]["Doc_No"].ToString();
                        }
                        hdnf.Value = HDNF_VALUE;
                        if (hdnf.Value == string.Empty)
                        {
                            DataTable dt_dalete_two = xml_ds.Tables[1];
                            if (dt_dalete_two.Rows.Count > 0)
                            {
                                hdnf.Value = dt_dalete_two.Rows[0]["Doc_No"].ToString();
                            }
                        }
                        if (hdnf.Value != string.Empty)
                        {
                            query = getDisplayQuery();
                            bool recordExist = this.fetchRecord(query);
                            this.makeEmptyForm("S");
                            clsButtonNavigation.enableDisable("S");
                        }
                        else
                        {
                            this.makeEmptyForm("N");
                            clsButtonNavigation.enableDisable("N");
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                    this.enableDisableNavigateButtons();
                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (hdnf.Value != string.Empty)
        {
            string query = getDisplayQuery();
            bool recordExist = this.fetchRecord(query);
        }
        else
        {
            this.showLastRecord();
        }
        string qry = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        if (qry != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            this.enableDisableNavigateButtons();
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");
            this.enableDisableNavigateButtons();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        btnUpdateBillAdj.Visible = false;
    }
    #endregion
    #region [fetchrecord]
    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;
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
                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                        if (lblCreated != null)
                        {
                            lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        }
                        Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        if (lblCreatedDate != null)
                        {
                            if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                            {
                                lblCreatedDate.Text = "";
                            }
                            else
                            {
                                lblCreatedDate.Text = "Created Date" + dt.Rows[0]["Created_Date"].ToString();
                            }
                        }
                        if (lblModifiedDate != null)
                        {
                            if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblModifiedDate.Text = "Modified Date" + dt.Rows[0]["Modified_Date"].ToString();
                            }
                        }

                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();

                        drpTran_Type.SelectedValue = dt.Rows[0]["Tran_Type"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_Date"].ToString();
                        txtAc_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        lblAc_Code.Text = dt.Rows[0]["Ac_name_E"].ToString();
                        txtLC_Number.Text = dt.Rows[0]["LC_Number"].ToString();
                        txtCash_Bank.Text = dt.Rows[0]["Cash_Bank"].ToString();
                        lblCash_Bank.Text = dt.Rows[0]["cashname"].ToString();
                        txtCheq_No.Text = dt.Rows[0]["Cheq_No"].ToString();
                        txtCheq_Date.Text = dt.Rows[0]["Cheq_Date"].ToString();
                        txtCheck_Amount.Text = dt.Rows[0]["Check_Amount"].ToString();
                        txtBank_Date.Text = dt.Rows[0]["Bank_Date"].ToString();
                        txtRemark.Text = dt.Rows[0]["Remark"].ToString();

                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select Detail_Id,Type,[Bill_No],[Bill_Date],[Bill_Amount],[Pending],[Recieved],[Adjusted],[Narration],[Unique_Id] from " + qryCommon + " where Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"]).ToString() + " and Year_Code=" + Convert.ToInt32(Session["year"]).ToString() + " and Doc_No=" + txtDoc_No.Text + " and Tran_Type='" + drpTran_Type.SelectedValue + "'";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    grdDetail.DataSource = dt;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = dt;
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion
                        //pnlgrdDetail.Enabled = false;
                    }
                }
            }
            this.Calculation();
            hdnf.Value = txtDoc_No.Text;
            this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "drpTran_Type")
            {
                setFocusControl(drpTran_Type);
            }
            if (strTextBox == "txtDoc_Date")
            {
                setFocusControl(txtDoc_Date);
            }
            if (strTextBox == "txtAc_Code")
            {
                string acname = "";
                if (txtAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                    if (a == false)
                    {
                        btntxtAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                            + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblAc_Code.Text = acname;
                            setFocusControl(txtCash_Bank);

                        }
                        else
                        {
                            txtAc_Code.Text = string.Empty;
                            lblAc_Code.Text = acname;
                            setFocusControl(txtAc_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }
            }
            if (strTextBox == "txtLC_Number")
            {
                if (txtLC_Number.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtLC_Number.Text);
                    if (a == false)
                    {
                        btntxtLC_Number_Click(this, new EventArgs());
                        //setFocusControl(txtInvoice_No);
                    }
                    else
                    {
                    }

                    #region

                    qry = "select ''as Detail_Id,Type,Bill_No,Bill_Date,Bill_Amount,Pending,Recieved,Adjusted,Narration,Unique_Id,'A' as Rowaction, '1' as Srno from qryLCPostedEntry where Company_Code='"
                       + Session["Company_Code"].ToString() + "'  And Doc_No='" + txtLC_Number.Text + "'";

                    DataSet ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = null;
                                int rowIndex = 1;
                                //dt.Columns.Add((new DataColumn("Detail_ID", typeof(int))));
                                // dr = dt.NewRow();

                                #region calculate rowindex
                                int maxIndex = 0;
                                int[] index = new int[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    index[i] = dt.Rows[i]["Detail_ID"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString()) : 0;
                                }
                                if (index.Length > 0)
                                {
                                    for (int i = 0; i < index.Length; i++)
                                    {
                                        if (index[i] > maxIndex)
                                        {
                                            maxIndex = index[i];

                                            rowIndex = maxIndex + 1;
                                            dt.Rows[i]["Detail_ID"] = rowIndex;
                                        }
                                        maxIndex += 1;
                                        rowIndex = maxIndex;
                                        dt.Rows[i]["Detail_ID"] = rowIndex;
                                    }

                                }
                                else
                                {
                                    rowIndex = maxIndex;          //1
                                }
                                #endregion


                                grdDetail.DataSource = dt;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = dt;
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                    }
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
                    }
                    #endregion


                    string invoicedata = "select * from qryLCPostedEntry where Doc_No=" + txtLC_Number.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                    DataSet dsinvoice = new DataSet();
                    DataTable dtinvoice = new DataTable();
                    dsinvoice = clsDAL.SimpleQuery(invoicedata);
                    if (dsinvoice.Tables.Count > 0)
                    {
                        dtinvoice = dsinvoice.Tables[0];
                        if (dtinvoice.Rows.Count > 0)
                        {
                            txtCheq_No.Text = dtinvoice.Rows[0]["Cheq_No"].ToString();
                            string currentdate = txtDoc_Date.Text;
                            DateTime dt_duedate = Convert.ToDateTime(currentdate);
                            DateTime dt_returndate = dt_duedate.AddDays(2);
                            txtCheq_Date.Text = dtinvoice.Rows[0]["Bank_Date"].ToString();
                            txtCheck_Amount.Text = dtinvoice.Rows[0]["Check_Amount"].ToString();
                            txtBank_Date.Text = txtDoc_Date.Text;
                            txtRemark.Text = dtinvoice.Rows[0]["Remark"].ToString();

                        }
                    }




                    setFocusControl(txtCheq_No);
                }


                else
                {
                    txtLC_Number.Text = string.Empty;
                    //lblIssue_Challan_No.Text = acname;
                    setFocusControl(txtLC_Number);
                }
            }
            if (strTextBox == "txtCash_Bank")
            {
                string acname = "";
                if (txtCash_Bank.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtCash_Bank.Text);
                    if (a == false)
                    {
                        btntxtCash_Bank_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Type='B' and Ac_Code=" + txtCash_Bank.Text
                            + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblCash_Bank.Text = acname;
                            setFocusControl(txtLC_Number);

                        }
                        else
                        {
                            txtCash_Bank.Text = string.Empty;
                            lblCash_Bank.Text = acname;
                            setFocusControl(txtCash_Bank);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtCash_Bank);
                }
            }
            if (strTextBox == "txtCheq_No")
            {
                setFocusControl(txtCheq_No);
            }
            if (strTextBox == "txtCheq_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtCheq_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtCheq_Date);
                    //}
                    //else
                    //{
                    //    txtCheq_Date.Text = "";
                    //    setFocusControl(txtCheq_Date);
                    //}
                }
                catch
                {
                    txtCheq_Date.Text = "";
                    setFocusControl(txtCheq_Date);
                }
            }
            if (strTextBox == "txtCheck_Amount")
            {
                setFocusControl(txtCheck_Amount);
            }
            if (strTextBox == "txtBank_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtBank_Date);
                    //}
                    //else
                    //{
                    //    txtBank_Date.Text = "";
                    //    setFocusControl(txtBank_Date);
                    //}
                }
                catch
                {
                    txtBank_Date.Text = "";
                    setFocusControl(txtBank_Date);
                }
            }
            if (strTextBox == "txtRemark")
            {
                setFocusControl(txtRemark);
            }
            if (strTextBox == "txtType")
            {
                setFocusControl(txtType);
            }
            if (strTextBox == "txtBill_No")
            {
                setFocusControl(txtBill_No);
            }
            if (strTextBox == "txtBill_Date")
            {
                setFocusControl(txtBill_Date);
            }
            if (strTextBox == "txtBill_Amount")
            {
                setFocusControl(txtBill_Amount);
            }
            if (strTextBox == "txtPending")
            {
                setFocusControl(txtPending);
            }
            if (strTextBox == "txtRecieved")
            {
                setFocusControl(txtRecieved);
            }
            if (strTextBox == "txtAdjusted")
            {
                setFocusControl(txtAdjusted);
            }
            if (strTextBox == "txtNarration")
            {
                setFocusControl(txtNarration);
            }
            if (strTextBox == "txtUnique_Id")
            {
                setFocusControl(txtUnique_Id);
            }

            Calculation();

        }
        catch
        {
        }
    }
    #endregion

    protected void btnAuthentication_Click(object sender, EventArgs e)
    {
        if (hdconfirm.Value == "Yes")
        {

            #region -Head part declearation

            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));

            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string retValue = string.Empty;
            string strRev = string.Empty;
            #endregion-End of Head part declearation


            #region Save Head Part

            child1.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
            child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

            child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child1.SetAttributeValue("Ac_Code", txtAc_Code.Text != string.Empty ? txtAc_Code.Text : "0");
            child1.SetAttributeValue("Cash_Bank", txtCash_Bank.Text != string.Empty ? txtCash_Bank.Text : "0");

            child1.SetAttributeValue("Taxable_Amount", txtCheck_Amount.Text != string.Empty ? txtCheck_Amount.Text : "0.00");

            child1.SetAttributeValue("Remark", txtRemark.Text + " Chq no." + txtCheq_No.Text);
            child1.SetAttributeValue("Authentication", Session["user"].ToString());
            child1.SetAttributeValue("Modify_Date", DateTime.Parse(DateTime.Now.ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child1.SetAttributeValue("Company_Code", Company_Code);
            child1.SetAttributeValue("Year_Code", Year_Code);
            string s = txtBank_Date.Text;

            //DateTime bankdate = Convert.ToDateTime(1900 - 01 - 01);
            string bankdate = s;
            DateTime bdate;
            string dateba = "";
            if (s == string.Empty || s == "01/01/1900")
            {
                bdate = DateTime.Parse(txtDoc_Date.Text);
                bdate = bdate.AddDays(2);
                child1.SetAttributeValue("Bank_Date", bdate.ToString("yyyy/MM/dd"));
            }
            else
            {

                child1.SetAttributeValue("Bank_Date", DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            }

            root.Add(child1);



            string XMLReport = root.ToString();
            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            DataSet xml_ds = new DataSet();
            string spname = "SP_Export_BillTransaction_Head";
            string xmlfile = XMLReport;
            string op = "";
            string returnmaxno = "";
            int flag;

            #region[Insert]
            flag = 7;
            xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion


            //txtDoc_No.Text = returnmaxno;
            //hdnf.Value = txtDoc_No.Text;
            // retValue = op;
            #endregion

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salebill", "javascript:auth();", true);
        }
        else
        {
            setFocusControl(btnAuthentication);
            return;

        }
    }

    #region calculation
    public void Calculation()
    {
        #region calculate gross
        double TotalAmt = 0.00;
        double CheckAmt = 0.00;
        double BalAmt = 0.00;
        if (grdDetail.Rows.Count > 0)
        {

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                //if (grdDetail.Rows[i].Cells[Rowaction].Text != "R" && grdDetail.Rows[i].Cells[Rowaction].Text != "D")
                //{
                //    double AmountGrid = Convert.ToDouble(grdDetail.Rows[i].Cells[Recieved].Text.Trim());
                //    TotalAmt = Math.Round((TotalAmt + AmountGrid), 2);
                //}

                string value1 = Server.HtmlDecode(grdDetail.Rows[i].Cells[Recieved].Text);
                value1 = value1.Trim();
                if (value1 != string.Empty)
                {
                    double value = Convert.ToDouble(grdDetail.Rows[i].Cells[Recieved].Text.Trim());
                    TotalAmt = TotalAmt + value;
                }
            }
            CheckAmt = Convert.ToDouble(txtCheck_Amount.Text != string.Empty ? txtCheck_Amount.Text : "0");
            //BalAmt = (TotalAmt - CheckAmt);
            BalAmt = CheckAmt - TotalAmt;

            lblreceviedetot.Text = TotalAmt.ToString();
            ////lblbalamt.Text = BalAmt.ToString();
            lblbalamt.Text = Math.Round(BalAmt, 2).ToString();

        }


        #endregion

    }
    #endregion
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                "' and Doc_No=" + hdnf.Value + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue + "'";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion
    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtDoc_No.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        pnlPopupDetails.Style["display"] = "block";
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["Detail_Id"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_Id"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["Detail_Id"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_Id"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from " + tblDetails + " where Detail_Id='" + lblID.Text + "' and Doc_No=" + txtDoc_No.Text + " " +
                            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                            " and Tran_Type='" + drpTran_Type.SelectedValue + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        if (id != string.Empty)
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Type", typeof(string))));

                    dt.Columns.Add((new DataColumn("Bill_No", typeof(int))));

                    dt.Columns.Add((new DataColumn("Bill_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
                    dt.Columns.Add((new DataColumn("Pending", typeof(string))));
                    dt.Columns.Add((new DataColumn("Recieved", typeof(string))));
                    dt.Columns.Add((new DataColumn("Adjusted", typeof(string))));
                    dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("Unique_Id", typeof(string))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["Detail_Id"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Type", typeof(string))));

                dt.Columns.Add((new DataColumn("Bill_No", typeof(int))));

                dt.Columns.Add((new DataColumn("Bill_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
                dt.Columns.Add((new DataColumn("Pending", typeof(string))));
                dt.Columns.Add((new DataColumn("Recieved", typeof(string))));
                dt.Columns.Add((new DataColumn("Adjusted", typeof(string))));
                dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                dt.Columns.Add((new DataColumn("Unique_Id", typeof(string))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Type"] = txtType.Text;
            //dr["Tran_type"] = txtTran_type.Text;
            if (txtType.SelectedValue != "EP")
            {
                if (txtBill_No.Text != string.Empty)
                {
                    dr["Bill_No"] = txtBill_No.Text;
                }
                else
                {
                    setFocusControl(txtBill_No);
                    return;
                }
            }
            else
            {
                dr["Bill_No"] = 0;
            }

            dr["Bill_Date"] = txtBill_Date.Text;
            dr["Bill_Amount"] = txtBill_Amount.Text != string.Empty ? txtBill_Amount.Text : "0";
            if (txtPending.Text != string.Empty)
            {
                dr["Pending"] = txtPending.Text;
            }
            else
            {
                dr["Pending"] = 0;
            }
            if (txtRecieved.Text != string.Empty && txtRecieved.Text != "0")
            {
                dr["Recieved"] = txtRecieved.Text;
            }
            else
            {
                dr["Recieved"] = 0;
            }
            if (txtAdjusted.Text != string.Empty)
            {
                dr["Adjusted"] = txtAdjusted.Text;
            }
            else
            {
                dr["Adjusted"] = 0;
            }

            dr["Narration"] = txtNarration.Text;
            dr["Unique_Id"] = txtUnique_Id.Text;
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dt.Rows.Add(dr);
            }
            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
            ViewState["currentTable"] = dt;
            if (btnAdddetails.Text == "ADD")
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtType);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->

            //txtTran_type.Text = string.Empty;
            txtBill_No.Text = string.Empty;
            txtBill_Date.Text = string.Empty;
            txtBill_Amount.Text = string.Empty;
            txtPending.Text = string.Empty;
            txtRecieved.Text = string.Empty;
            txtAdjusted.Text = string.Empty;
            txtNarration.Text = string.Empty;
            txtUnique_Id.Text = string.Empty;
            btnAdddetails.Text = "ADD";
            setFocusControl(txtBill_No);
            Calculation();
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;

        //txtTran_type.Text = string.Empty;
        txtBill_No.Text = string.Empty;
        txtBill_Date.Text = string.Empty;
        txtBill_Amount.Text = string.Empty;
        txtPending.Text = string.Empty;
        txtRecieved.Text = string.Empty;
        txtAdjusted.Text = string.Empty;
        txtNarration.Text = string.Empty;
        txtUnique_Id.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtType);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtType.Text = Server.HtmlDecode(gvrow.Cells[Type].Text);
        //txtTran_type.Text = Server.HtmlDecode(gvrow.Cells[Tran_type].Text);
        txtBill_No.Text = Server.HtmlDecode(gvrow.Cells[Bill_No].Text);
        txtBill_Date.Text = Server.HtmlDecode(gvrow.Cells[Bill_Date].Text);
        txtBill_Amount.Text = Server.HtmlDecode(gvrow.Cells[Bill_Amount].Text);
        txtPending.Text = Server.HtmlDecode(gvrow.Cells[Pending].Text);
        txtRecieved.Text = Server.HtmlDecode(gvrow.Cells[Recieved].Text);
        txtAdjusted.Text = Server.HtmlDecode(gvrow.Cells[Adjusted].Text);
        txtNarration.Text = Server.HtmlDecode(gvrow.Cells[Narration].Text);
        txtUnique_Id.Text = Server.HtmlDecode(gvrow.Cells[Unique_Id].Text);
        hdnfunique.Value = Server.HtmlDecode(gvrow.Cells[Unique_Id].Text);

        setFocusControl(txtBill_No);
    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_Id"].ToString());
                string IDExisting = clsCommon.getString("select Detail_Id from " + tblDetails + " where Doc_No='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "N";// Add rowaction id
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].Style["overflow"] = "hidden";
            e.Row.Cells[2].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------
            e.Row.Cells[Type].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[Type].Style["overflow"] = "hidden";
            e.Row.Cells[Type].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------

            //--------------------------------------------------
            e.Row.Cells[Bill_No].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Bill_No].Style["overflow"] = "hidden";
            e.Row.Cells[Bill_No].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Bill_Date].ControlStyle.Width = new Unit("140px");
            e.Row.Cells[Bill_Date].Style["overflow"] = "hidden";
            e.Row.Cells[Bill_Date].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Bill_Amount].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[Bill_Amount].Style["overflow"] = "hidden";
            e.Row.Cells[Bill_Amount].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Pending].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[Pending].Style["overflow"] = "hidden";
            e.Row.Cells[Pending].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Recieved].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[Recieved].Style["overflow"] = "hidden";
            e.Row.Cells[Recieved].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Adjusted].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[Adjusted].Style["overflow"] = "hidden";
            e.Row.Cells[Adjusted].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Narration].ControlStyle.Width = new Unit("200px");
            e.Row.Cells[Narration].Style["overflow"] = "hidden";
            e.Row.Cells[Narration].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Unique_Id].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[Unique_Id].Style["overflow"] = "hidden";
            e.Row.Cells[Unique_Id].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            //    e.Row.Cells[0].Visible =true;

        }
        catch
        {
        }
    }
    #endregion
    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;

            if (v == "txtCash_Bank" || v == "txtAc_Code")
            {
                e.Row.Cells[0].Width = new Unit("120px");
                //e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("120px");
                e.Row.Cells[2].Width = new Unit("120px");
            }
            if (v == "txtDoc_No" || v == "txtEditDoc_No")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                //e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[2].Width = new Unit("220px");
                e.Row.Cells[2].Style["overflow"] = "hidden";
                e.Row.Cells[3].Width = new Unit("250px");
                e.Row.Cells[4].Width = new Unit("100px");
            }
            if (v == "txtBill_No")
            {
                e.Row.Cells[0].Width = new Unit("120px");
                e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("120px");
                e.Row.Cells[2].Width = new Unit("120px");
                e.Row.Cells[3].Width = new Unit("120px");
                e.Row.Cells[4].Width = new Unit("120px");
                e.Row.Cells[5].Width = new Unit("120px");
            }

        }
    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    #endregion
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
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion
    #region [RowCommand]
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[Rowaction].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "UPDATE";
                            setFocusControl(txtBill_No);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);
                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtEditDoc_No.Text;
        //strTextBox = "txtEditDoc_No";
        //csCalculations();
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                searchString = txtEditDoc_No.Text;
                strTextBox = "txtEditDoc_No";
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry1 = getDisplayQuery();
                fetchRecord(qry1);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();
    }
    #endregion
    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [drpTran_Type_TextChanged]
    protected void drpTran_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Focus();
        this.showLastRecord();
    }
    #endregion
    #region [drpTran_Type_TextChanged]
    protected void txtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(txtBill_No);
    }
    #endregion

    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtAc_Code_Click]
    protected void btntxtAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAc_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtLC_Number_TextChanged]
    protected void txtLC_Number_TextChanged(object sender, EventArgs e)
    {
        string s = txtLC_Number.Text;
        // s = s.Replace("0", "");
        txtLC_Number.Text = s;
        txtLC_Number.Text = txtLC_Number.Text.Trim();
        searchString = txtLC_Number.Text;
        strTextBox = "txtLC_Number";
        csCalculations();
    }
    #endregion
    #region [btntxtLC_Number_Click]
    protected void btntxtLC_Number_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtLC_Number";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtCash_Bank_TextChanged]
    protected void txtCash_Bank_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCash_Bank.Text;
        strTextBox = "txtCash_Bank";
        csCalculations();
    }
    #endregion
    #region [btntxtCash_Bank_Click]
    protected void btntxtCash_Bank_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCash_Bank";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtCheq_No_TextChanged]
    protected void txtCheq_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheq_No.Text;
        strTextBox = "txtCheq_No";
        csCalculations();
    }
    #endregion
    #region [txtCheq_Date_TextChanged]
    protected void txtCheq_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheq_Date.Text;
        strTextBox = "txtCheq_Date";
        csCalculations();
    }
    #endregion
    #region [txtCheck_Amount_TextChanged]
    protected void txtCheck_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheck_Amount.Text;
        strTextBox = "txtCheck_Amount";
        csCalculations();
    }
    #endregion
    #region [txtBank_Date_TextChanged]
    protected void txtBank_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBank_Date.Text;
        strTextBox = "txtBank_Date";
        csCalculations();
    }
    #endregion
    #region [txtRemark_TextChanged]
    protected void txtRemark_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRemark.Text;
        strTextBox = "txtRemark";
        csCalculations();
    }
    #endregion

    #region [txtType_TextChanged]
    protected void txtType_TextChanged(object sender, EventArgs e)
    {
        searchString = txtType.Text;
        strTextBox = "txtType";
        csCalculations();
    }
    #endregion


    #region [txtBill_No_TextChanged]
    protected void txtBill_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_No.Text;
        strTextBox = "txtBill_No";

        string unique = hdnfunique.Value;
        txtUnique_Id.Text = unique;
        //csCalculations();
        setFocusControl(txtRecieved);
        //double billamt = 0.00;
        //double rcv = 0.00;
        // double Pending = 0.00;
        //billamt = Convert.ToDouble(txtBill_Amount.Text);

        //rcv = Convert.ToDouble(txtRecieved.Text);

        //Pending = billamt - rcv;

        //txtPending.Text = Pending.ToString();


    }
    #endregion

    #region [btntxtBill_No_Click]
    protected void btntxtBill_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBill_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBill_Date_TextChanged]
    protected void txtBill_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_Date.Text;
        strTextBox = "txtBill_Date";
        csCalculations();
    }
    #endregion


    #region [txtBill_Amount_TextChanged]
    protected void txtBill_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_Amount.Text;
        strTextBox = "txtBill_Amount";
        csCalculations();
    }
    #endregion


    #region [txtPending_TextChanged]
    protected void txtPending_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPending.Text;
        strTextBox = "txtPending";
        csCalculations();
    }
    #endregion


    #region [txtRecieved_TextChanged]
    protected void txtRecieved_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRecieved.Text;
        strTextBox = "txtRecieved";
        csCalculations();
    }
    #endregion


    #region [txtAdjusted_TextChanged]
    protected void txtAdjusted_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAdjusted.Text;
        strTextBox = "txtAdjusted";
        csCalculations();
    }
    #endregion




    #region [txtNarration_TextChanged]
    protected void txtNarration_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration.Text;
        strTextBox = "txtNarration";
        csCalculations();
    }
    #endregion


    #region [txtUnique_Id_TextChanged]
    protected void txtUnique_Id_TextChanged(object sender, EventArgs e)
    {
        searchString = txtUnique_Id.Text;
        strTextBox = "txtUnique_Id";
        csCalculations();
    }
    #endregion

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
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtDoc_No.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDoc_No.Text = string.Empty;
                    txtDoc_No.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtDoc_No);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtDoc_No.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                     string qry = " select distinct Doc_No,Doc_Date,cashname,Ac_Name_E as PartyName,Cheq_No,Check_Amount as Amount from " + qryCommon +
                       " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + drpTran_Type.SelectedValue +
                       "' and (Doc_Date Like '%" + txtSearchText.Text + "%'or cashname like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Cheq_No like '%" + txtSearchText.Text + "%')  group by Doc_No,Doc_Date,cashname,Ac_Name_E ,Cheq_No,Check_Amount  order by  Doc_No desc, convert(varchar(10), Doc_Date, 121)  desc";
                    this.showPopup(qry);
                }
            }


            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                lblPopupHead.Text = "--Select Ac Code--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtCash_Bank")
            {
                
                lblPopupHead.Text = "--Select Cash Bank Code--";
                string qry = "select Ac_Code as Party_Code,Ac_Name_E as Party_name,cityname as Party_City from qrymstaccountmaster where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%' ) and Ac_Type='B' order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBill_No")
            {
                if (btnSave.Text == "Save")
                {
                    //qry = "select Doc_No as BillNo ,convert(varchar(10),Doc_Date,103) as BillDate ,Bill_Amount,recieved,Balance,Unique_Id from qryPendingSaleBill  where" + name +
                    //   " and Ac_Code=" + txtAc_Code.Text + " and Balance<>0 order by Doc_Date desc, Doc_No desc";

                  qry = "select Doc_No as BillNo ,convert(varchar(10),Doc_Date,103) as BillDate ,Invoice_Value as Bill_Amount,"+
                       " recieved,Balance,UpdatedDoc_No as Unique_Id from qryPendingSaleBill_Export  where (Doc_No Like '%" + txtSearchText.Text + "%'or Invoice_Value like '%" + txtSearchText.Text + "%') "+
                     " and Ac_Code=" + txtAc_Code.Text + " and Balance<>0 order by Doc_Date desc, Doc_No desc";
                }
                else
                {
                    //qry = "select Doc_No as BillNo ,convert(varchar(10),Doc_Date,103) as BillDate ,Bill_Amount,recieved,Balance,Unique_Id from qryPendingSaleBill  where"
                    //   + name + "  and Ac_Code=" + txtAc_Code.Text + " order by Doc_Date desc, Doc_No desc";

                    qry = "select Doc_No as BillNo ,convert(varchar(10),Doc_Date,103) as BillDate ,Invoice_Value as Bill_Amount,"+
                        " recieved,Balance,UpdatedDoc_No as Unique_Id from qryPendingSaleBill_Export  where" +
                      " (Doc_No Like '%" + txtSearchText.Text + "%'or Invoice_Value like '%" + txtSearchText.Text + "%' ) and Ac_Code=" + txtAc_Code.Text + " order by Doc_Date desc, Doc_No desc";
                }
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtLC_Number")
            {
                

                lblPopupHead.Text = "--Select--";
                if (txtAc_Code.Text != string.Empty)
                {
                    string qry = "select DISTINCT Doc_No,Cheq_No,Cheq_Date,Check_Amount from qrypendingLC where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                   + "  and  ( Doc_No Like '%" + txtSearchText.Text + "%'or Cheq_No like '%" + txtSearchText.Text + "%' or Check_Amount like '%" + txtSearchText.Text + "%' ) order by Doc_No desc";
                    this.showPopup(qry);
                }
                else
                {
                    pnlPopup.Style["display"] = "none";
                    setFocusControl(txtAc_Code);
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Popup Button Code]
    protected void showPopup(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
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
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "Close";
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
    #region [txtSearchText_TextChanged]
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "Close")
            {
                txtSearchText.Text = string.Empty;
                pnlPopup.Style["display"] = "none";
                grdPopup.DataSource = null;
                grdPopup.DataBind();
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                searchString = txtSearchText.Text;
                strTextBox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        #region [Validation Part]
        bool isValidated = true;
        //        if textbox is date then if condition will be like this if(clsCommon.isValidDate(txtDoc_Date.Text==true))
        Calculation();
        if (Convert.ToDouble(lblreceviedetot.Text) <= Convert.ToDouble(txtCheck_Amount.Text))
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('plz check Cheque Amount!!!!!');", true);
            return;
        }

        string Post_date = Session["Post_Date"].ToString();
        if (Convert.ToDateTime(txtDoc_Date.Text) >= Convert.ToDateTime(Post_date))
        {
            isValidated = true;
        }
        else
        {
            setFocusControl(txtDoc_Date);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Post Date Error!!!!!');", true);
            return;
        }
        if (txtDoc_Date.Text != string.Empty)
        {
            string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            if (clsCommon.isValidDate(dt) == true)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtDoc_Date);
                return;
            }
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDoc_Date);
            return;
        }
        //if (btnSave.Text == "Update")
        //{
        //    string auth = clsCommon.getString("Select authanticated from programlist where Tran_Type ='" + drpTran_Type.SelectedValue + "' and Doc_No=" + txtDoc_No.Text +
        //       " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString() + " and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());

        //    if (auth != string.Empty)
        //    {
        //        int userid = Convert.ToInt32(Session["User_Id"].ToString());
        //        string pagevalidation = clsCommon.getString("Select Permission from tblUser_Detail where Tran_Type ='" + drpTran_Type.SelectedValue +
        //            "' and User_Id=" + userid + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString());
        //        if (pagevalidation == "Y")
        //        {
        //            isValidated = true;
        //        }
        //        else
        //        {
        //            setFocusControl(txtDoc_Date);
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You are Not Authorize Person to Update Record!!!!!');", true);
        //            return;
        //        }
        //    }
        //}


        if (txtDoc_No.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDoc_No);
            return;
        }
        //if (drpTran_Type.SelectedIndex != 0)
        //{
        //    isValidated = true;
        //}
        //else
        //{
        //    isValidated = false;
        //    setFocusControl(drpTran_Type);
        //    return;
        //}
        /*  In Grid At Least One Record is required
            int count = 0;
              if (grdDetail.Rows.Count == 0)
          {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"","alert('Please Enter Details!!!!             isValidated = false;
          setFocusControl(btnOpenDetailsPopup); 
          return;  
          }
           if (grdDetail.Rows.Count >= 1) 
          for (int i = 0; i < grdDetail.Rows.Count; i++)
          {
           if (grdDetail.Rows[i].Cells[10].Text == "D")
          {
          count++; 
          }
          }
            if (grdDetail.Rows.Count == count)
          {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"","alert('Please Enter Minumun One Details!!!!          isValidated = false; 
            setFocusControl(btnOpenDetailsPopup); 
          return;
         } 
          }
          */
        #endregion

        #region -Head part declearation

        XElement root = new XElement("ROOT");
        XElement child1 = new XElement("Head");
        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
        int Branch_Code = Convert.ToInt32(Convert.ToInt32(Session["Branch_Code"].ToString()));
        string Created_By = Session["user"].ToString();
        string Modified_By = Session["user"].ToString();
        string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string retValue = string.Empty;
        string strRev = string.Empty;
        int Status = 1;
        #endregion-End of Head part declearation

        #region Save Head Part

        child1.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
        child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        child1.SetAttributeValue("Ac_Code", txtAc_Code.Text != string.Empty ? txtAc_Code.Text : "0");
        child1.SetAttributeValue("LC_Number", txtLC_Number.Text);
        child1.SetAttributeValue("Cash_Bank", txtCash_Bank.Text != string.Empty ? txtCash_Bank.Text : "0");
        child1.SetAttributeValue("Cheq_No", txtCheq_No.Text);
        child1.SetAttributeValue("Cheq_Date", DateTime.Parse(txtCheq_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        child1.SetAttributeValue("Check_Amount", txtCheck_Amount.Text != string.Empty ? txtCheck_Amount.Text : "0.00");
        string s = txtBank_Date.Text;

        //DateTime bankdate = Convert.ToDateTime(1900 - 01 - 01);
        string bankdate = s;
        DateTime bdate;
        string dateba = "";
        if (s == string.Empty || s == "01/01/1900")
        {
            bdate = DateTime.Parse(txtDoc_Date.Text);
            bdate = bdate.AddDays(2);
            child1.SetAttributeValue("Bank_Date", bdate.ToString("yyyy/MM/dd"));
        }
        else
        {

            child1.SetAttributeValue("Bank_Date", DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        }
        //child1.SetAttributeValue("Bank_Date", DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));

        child1.SetAttributeValue("Remark", txtRemark.Text);
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_Code", Year_Code);
        child1.SetAttributeValue("Branch_Code", Branch_Code);
        child1.SetAttributeValue("Status", Status);
        if (btnSave.Text != "Save")
        {
            child1.SetAttributeValue("Modified_By", Modified_By);
            child1.SetAttributeValue("Modified_Date", Modified_Date);
            child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

        }
        else
        {
            child1.SetAttributeValue("Created_By", Created_By);
            child1.SetAttributeValue("Created_Date", Created_Date);
        }
        root.Add(child1);
        #endregion-End of Head part Save

        #region save Head Master
        #region --------------------  Details --------------------
        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {
            string CheckingFlag = string.Empty;
            XElement child2 = new XElement("Details");
            Int32 Detail_Id = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);
            if (btnSave.Text != "Save")
            {
                if (grdDetail.Rows[i].Cells[Rowaction].Text == "A")//RowAction Value
                {
                    CheckingFlag = "1";
                }
                else if (grdDetail.Rows[i].Cells[Rowaction].Text == "U" || grdDetail.Rows[i].Cells[Rowaction].Text == "N")
                {
                    CheckingFlag = "2";
                }
                else
                {
                    CheckingFlag = "3";
                }
            }
            child2.SetAttributeValue("Doc_No", txtDoc_No.Text);
            child2.SetAttributeValue("Company_Code", Company_Code);
            child2.SetAttributeValue("Year_Code", Year_Code);
            //child2.SetAttributeValue("Branch_Code", txtbr);

            child2.SetAttributeValue("Detail_Id", Detail_Id);
            child2.SetAttributeValue("Type", Server.HtmlDecode(grdDetail.Rows[i].Cells[Type].Text));
            child2.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
            string BillNo = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bill_No].Text);
            BillNo = BillNo.Trim();

            if (BillNo != "")
            {
                child2.SetAttributeValue("Bill_No", BillNo);
            }
            else
            {
                child2.SetAttributeValue("Bill_No", "0");
            }
            string BankDT = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bill_Date].Text);
            BankDT = BankDT.Trim();

            if (BankDT != "")
            {
                child2.SetAttributeValue("Bill_Date", DateTime.Parse(grdDetail.Rows[i].Cells[Bill_Date].Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            }
            else
            {
                child2.SetAttributeValue("Bill_Date", "");

            }

            //child2.SetAttributeValue("Bill_Date", DateTime.Parse(grdDetail.Rows[i].Cells[Bill_Date].Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child2.SetAttributeValue("Bill_Amount", Server.HtmlDecode(grdDetail.Rows[i].Cells[Bill_Amount].Text));
            child2.SetAttributeValue("Pending", Server.HtmlDecode(grdDetail.Rows[i].Cells[Pending].Text.Trim()) != string.Empty ? Server.HtmlDecode(grdDetail.Rows[i].Cells[Pending].Text.Trim()) : "0");
            child2.SetAttributeValue("Recieved", Server.HtmlDecode(grdDetail.Rows[i].Cells[Recieved].Text.Trim()) != string.Empty ? Server.HtmlDecode(grdDetail.Rows[i].Cells[Recieved].Text.Trim()) : "0");
            child2.SetAttributeValue("Adjusted", Server.HtmlDecode(grdDetail.Rows[i].Cells[Adjusted].Text.Trim()) != string.Empty ? Server.HtmlDecode(grdDetail.Rows[i].Cells[Adjusted].Text.Trim()) : "0");
            child2.SetAttributeValue("Narration", Server.HtmlDecode(grdDetail.Rows[i].Cells[Narration].Text));
            child2.SetAttributeValue("Unique_Id", Server.HtmlDecode(grdDetail.Rows[i].Cells[Unique_Id].Text));
            if (btnSave.Text != "Save")
            {
                child2.SetAttributeValue("Flag", CheckingFlag);
            }
            child1.Add(child2);
        }
        #endregion
        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_Export_BillTransaction_Head";
        string xmlfile = XMLReport;
        string op = "";
        string returnmaxno = "";
        int flag;
        if (btnSave.Text == "Save")
        {
            #region[Insert]
            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        else
        {
            #region[Update]
            flag = 6;
            xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        txtDoc_No.Text = returnmaxno;
        hdnf.Value = txtDoc_No.Text;
        retValue = op;

        if (retValue == "-1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Added! No=" + returnmaxno + "');", true);
        }
        if (retValue == "-2" || retValue == "-3")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated! No=" + returnmaxno + "');", true);
        }
        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
        btnUpdateBillAdj.Visible = false;
        #endregion
    }
    #endregion
    #region [btnCancel_Click]
    protected void btnUpdateBillAdj_Click(object sender, EventArgs e)
    {

        #region calculate gross
        double TotalAmt = 0.00;

        if (grdDetail.Rows.Count > 0)
        {

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[Rowaction].Text != "R" && grdDetail.Rows[i].Cells[Rowaction].Text != "D")
                {
                    double AmountGrid = Convert.ToDouble(grdDetail.Rows[i].Cells[Recieved].Text.Trim());
                    TotalAmt = TotalAmt + AmountGrid;
                }
            }
            // txtCheck_Amount.Text = TotalAmt.ToString();

        }

        double checkamt = Convert.ToDouble(txtCheck_Amount.Text);
        #endregion
        if (TotalAmt == checkamt)
        {
            #region -Head part declearation

            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
            int Branch_Code = Convert.ToInt32(Convert.ToInt32(Session["Branch_Code"].ToString()));
            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string retValue = string.Empty;
            string strRev = string.Empty;
            int Status = 0;
            #endregion-End of Head part declearation

            #region Save Head Part

            child1.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
            child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child1.SetAttributeValue("Ac_Code", txtAc_Code.Text != string.Empty ? txtAc_Code.Text : "0");
            child1.SetAttributeValue("LC_Number", txtLC_Number.Text);
            child1.SetAttributeValue("Cash_Bank", txtCash_Bank.Text != string.Empty ? txtCash_Bank.Text : "0");
            child1.SetAttributeValue("Cheq_No", txtCheq_No.Text);
            child1.SetAttributeValue("Cheq_Date", DateTime.Parse(txtCheq_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child1.SetAttributeValue("Check_Amount", txtCheck_Amount.Text != string.Empty ? txtCheck_Amount.Text : "0.00");
            string s = txtBank_Date.Text;

            //DateTime bankdate = Convert.ToDateTime(1900 - 01 - 01);
            string bankdate = s;
            DateTime bdate;
            string dateba = "";
            if (s == string.Empty || s == "01/01/1900")
            {
                bdate = DateTime.Parse(txtDoc_Date.Text);
                bdate = bdate.AddDays(2);
                child1.SetAttributeValue("Bank_Date", bdate.ToString("yyyy/MM/dd"));
            }
            else
            {

                child1.SetAttributeValue("Bank_Date", DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            }
            //child1.SetAttributeValue("Bank_Date", DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));

            child1.SetAttributeValue("Remark", txtRemark.Text);
            child1.SetAttributeValue("Company_Code", Company_Code);
            child1.SetAttributeValue("Year_Code", Year_Code);
            child1.SetAttributeValue("Branch_Code", Branch_Code);
            child1.SetAttributeValue("Status", Status);

            if (btnSave.Text != "Save")
            {
                child1.SetAttributeValue("Modified_By", Modified_By);
                child1.SetAttributeValue("Modified_Date", Modified_Date);
                child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

            }
            else
            {
                child1.SetAttributeValue("Created_By", Created_By);
                child1.SetAttributeValue("Created_Date", Created_Date);
            }
            root.Add(child1);
            #endregion-End of Head part Save

            #region save Head Master
            #region --------------------  Details --------------------
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                string CheckingFlag = string.Empty;
                XElement child2 = new XElement("Details");
                Int32 Detail_Id = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);
                if (btnSave.Text != "Save")
                {
                    if (grdDetail.Rows[i].Cells[Rowaction].Text == "A")//RowAction Value
                    {
                        CheckingFlag = "1";
                    }
                    else if (grdDetail.Rows[i].Cells[Rowaction].Text == "U" || grdDetail.Rows[i].Cells[Rowaction].Text == "N")
                    {
                        CheckingFlag = "2";
                    }
                    else
                    {
                        CheckingFlag = "3";
                    }
                }
                child2.SetAttributeValue("Doc_No", txtDoc_No.Text);
                child2.SetAttributeValue("Company_Code", Company_Code);
                child2.SetAttributeValue("Year_Code", Year_Code);
                //child2.SetAttributeValue("Branch_Code", txtbr);

                child2.SetAttributeValue("Detail_Id", Detail_Id);
                child2.SetAttributeValue("Type", Server.HtmlDecode(grdDetail.Rows[i].Cells[Type].Text));
                child2.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
                string BillNo = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bill_No].Text);
                BillNo = BillNo.Trim();

                if (BillNo != "")
                {
                    child2.SetAttributeValue("Bill_No", BillNo);
                }
                else
                {
                    child2.SetAttributeValue("Bill_No", "0");
                }
                string BankDT = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bill_Date].Text);
                BankDT = BankDT.Trim();

                if (BankDT != "")
                {
                    child2.SetAttributeValue("Bill_Date", DateTime.Parse(grdDetail.Rows[i].Cells[Bill_Date].Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
                }
                else
                {
                    child2.SetAttributeValue("Bill_Date", "");

                }

                //child2.SetAttributeValue("Bill_Date", DateTime.Parse(grdDetail.Rows[i].Cells[Bill_Date].Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
                child2.SetAttributeValue("Bill_Amount", Server.HtmlDecode(grdDetail.Rows[i].Cells[Bill_Amount].Text));
                child2.SetAttributeValue("Pending", Server.HtmlDecode(grdDetail.Rows[i].Cells[Pending].Text.Trim()) != string.Empty ? Server.HtmlDecode(grdDetail.Rows[i].Cells[Pending].Text.Trim()) : "0");
                child2.SetAttributeValue("Recieved", Server.HtmlDecode(grdDetail.Rows[i].Cells[Recieved].Text.Trim()) != string.Empty ? Server.HtmlDecode(grdDetail.Rows[i].Cells[Recieved].Text.Trim()) : "0");
                child2.SetAttributeValue("Adjusted", Server.HtmlDecode(grdDetail.Rows[i].Cells[Adjusted].Text.Trim()) != string.Empty ? Server.HtmlDecode(grdDetail.Rows[i].Cells[Adjusted].Text.Trim()) : "0");
                child2.SetAttributeValue("Narration", Server.HtmlDecode(grdDetail.Rows[i].Cells[Narration].Text));
                child2.SetAttributeValue("Unique_Id", Server.HtmlDecode(grdDetail.Rows[i].Cells[Unique_Id].Text));
                if (btnSave.Text != "Save")
                {
                    child2.SetAttributeValue("Flag", CheckingFlag);
                }
                child1.Add(child2);
            }
            #endregion
            string XMLReport = root.ToString();
            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            DataSet xml_ds = new DataSet();
            string spname = "SP_Export_BillTransaction_Head";
            string xmlfile = XMLReport;
            string op = "";
            string returnmaxno = "";
            int flag;
            if (btnSave.Text == "Save")
            {
                #region[Insert]
                flag = 1;
                xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
                #endregion
            }
            else
            {
                #region[Update]
                flag = 6;
                xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
                #endregion
            }
            txtDoc_No.Text = returnmaxno;
            hdnf.Value = txtDoc_No.Text;
            retValue = op;

            if (retValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Added! No=" + returnmaxno + "');", true);
            }
            if (retValue == "-2" || retValue == "-3")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated! No=" + returnmaxno + "');", true);
            }
            clsButtonNavigation.enableDisable("S");
            this.enableDisableNavigateButtons();
            this.makeEmptyForm("S");
            qry = getDisplayQuery();
            this.fetchRecord(qry);
            btnUpdateBillAdj.Visible = false;
            #endregion
        }
    }
    #endregion
}
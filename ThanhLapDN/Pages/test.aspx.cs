using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using vpro.functions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using ThanhLapDN.Data;

namespace ThanhLapDN.Pages
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //ReplaceCustomXML();
        }

        private bool Save_FileDoc1()
        {
            try
            {
                var application = new Microsoft.Office.Interop.Word.Application();
                var document = new Microsoft.Office.Interop.Word.Document();
                document = application.Documents.Add(Template: Server.MapPath("/File/Template/templateHDDV.doc"));

                application.Visible = true;

                foreach (Microsoft.Office.Interop.Word.Field field in document.Fields)
                {
                    if (field.Code.Text.Contains("MerPos01"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerPos02"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerPos03"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerPos04"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerPos05"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerName"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerAddress"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerPhone"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerTaxCode"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerEmail"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerRepresent"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerPosition"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerBeginM"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerDeadlineInt"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerDeadlineString"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerCostTitle01"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerCostTitle02"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                    else if (field.Code.Text.Contains("MerCostDetail"))
                    {
                        field.Select();
                        application.Selection.TypeText("text1");
                    }
                }
                document.SaveAs(FileName: String.Format(Server.MapPath("/File/WordFile/HopDongDVKeToan_Code_{0}.doc"), 123));

                document.Close();
                application.Quit();

                return true;
            }
            catch (Exception) { throw; return false; }
        }

        protected void ReplaceCustomXML()
        {
            //Copy từ file gốc ra 1 bản 
            string _pathOrigin = Server.MapPath("/File/Template/TemplateHDDV.docx");
            string _pathNew = Server.MapPath("/File/WordFile/W001.docx");
            if (File.Exists(_pathNew))
                File.Delete(_pathNew);
            if (File.Exists(_pathOrigin))
            {
                File.Copy(_pathOrigin, _pathNew);

                var docText = "";
                WordprocessingDocument docR = WordprocessingDocument.Open(_pathNew, true);
                MainDocumentPart mainPartR = docR.MainDocumentPart;
                using (StreamReader sr = new StreamReader(docR.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                    docText = docText.Replace("MerPos01", "001");
                    docText = docText.Replace("MerPos02", "002");
                    docText = docText.Replace("MerPos03", "003");
                    docText = docText.Replace("MerPos04", "004");
                    docText = docText.Replace("MerPos05", "005");
                }
                using (StreamWriter sw = new StreamWriter(docR.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                docR.Close();
            }
        }

        //#region Import
        //UserRepo _UserRepo = new UserRepo();
        //private DataTable getDataexcel(string SourceFilePath)
        //{

        //    DataTable dtExcel = new DataTable();
        //    // Connection String to Excel Workbook
        //    string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", SourceFilePath);
        //    OleDbConnection connection = new OleDbConnection();
        //    connection.ConnectionString = excelConnectionString;
        //    connection.Open();
        //    OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
        //    OleDbDataAdapter data = new OleDbDataAdapter(command);
        //    data.Fill(dtExcel);
        //    return dtExcel;
        //}
        //private void Import_data()
        //{
        //    try
        //    {
        //        if (fileUpload.HasFile == true)
        //        {
        //            string path = Server.MapPath("/File/ExcelFile/" + fileUpload.FileName);
        //            fileUpload.SaveAs(path);
        //            DataTable dt = getDataexcel(path);
        //            int _count = dt.Rows.Count;
        //            for (int i = 0; i < _count; i++)
        //            {
        //                string _maNV = Utils.CStrDef(dt.Rows[i][0]);
        //                if (_maNV.Contains("Mã nhân viên"))
        //                {
        //                    string _getMaCC = _maNV.Substring(14, 5);
        //                    var obj = _UserRepo.GetInfoUserByMaCC(_getMaCC);
        //                    if (obj.Count > 0)
        //                    {
        //                        i += 6;
        //                        while (Utils.CStrDef(dt.Rows[i][0]) != "")
        //                        {
        //                            CHAM_CONG c = new CHAM_CONG();
        //                            c.CC_MACC = _getMaCC;
        //                            c.CC_USERID = obj[0].USER_ID;

        //                            i++;
        //                        }
        //                    }
        //                }
        //            }

        //            string strScript = "<script>";
        //            strScript += "alert('Đã Import dữ liệu thành công');";
        //            strScript += "window.location='test.aspx';";
        //            strScript += "</script>";
        //            Page.RegisterClientScriptBlock("strScript", strScript);
        //        }
        //        else
        //        {
        //            string strScript = "<script>";
        //            strScript += "alert('Xin chọn file để Import');";
        //            strScript += "window.location='danh-sach-cong-no.aspx';";
        //            strScript += "</script>";
        //            Page.RegisterClientScriptBlock("strScript", strScript);
        //        }
        //    }
        //    catch (Exception ex) { throw; }
        //}
        //protected void imgBtnImport_Click1(object sender, EventArgs e)
        //{
        //    Import_data();
        //}
        //#endregion
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

public class Connection
{
    public SqlConnection con;
    public Connection()
    {
        // Read Connection string from webconfig
        //try
        //{
        //    con = new SqlConnection(ConfigurationManager.ConnectionStrings["c1"].ToString());
        //    con.Open();
        //}
        //finally
        //{
        //    con.Close();
        //}
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["web"].ToString());
        con.Open();
    }

    public SqlDataReader GetDataReader(string sql)
    {
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader rd = cmd.ExecuteReader();
        return rd;
    }

    public string GetStringData(string sql)
    {
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        string data = ds.Tables[0].Rows[0].ItemArray[0].ToString();
        return data;
    }

    public DataSet GetDS(string sql)
    {
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        return ds;
    }

    //to perform insert, update, delete
    public bool ExecuteSql(string sql)
    {
        SqlCommand cmd = new SqlCommand(sql, con);
        //try
        //{
        cmd.ExecuteNonQuery();
        return true;
        //}
        //catch (Exception ex)
        //{
        // return false;
        //}
    }
    //to fill datatable
    public DataTable GetDatatable(string sel)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(sel, con);
        adp.Fill(dt);
        return dt;
    }
    public string GetStringFromDs(string sql)
    {
        SqlDataAdapter ad = new SqlDataAdapter(sql, con);
        string data = "";
        DataSet ds = new DataSet();
        ad.Fill(ds);
        //try
        //{
        data = ds.Tables[0].Rows[0].ItemArray[0].ToString();
        //}
        //catch (Exception ex)
        //{
        //}
        return data;
    }
    public String insertgetid(String Sql)

    {
        Sql += ";Select SCOPE_IDENTITY()";
        SqlCommand cmd = new SqlCommand(Sql, con);
        object result = cmd.ExecuteScalar();
        String a = result.ToString();
        return a;
    }



    public string GetScalar(string sql)
    {
        SqlCommand cmd = new SqlCommand(sql, con);
        string data = "";
        // try
        //{
        data = cmd.ExecuteScalar().ToString();
        //}
        //catch (Exception ex)
        //{
        //}
        return data;
    }

    //to fill grid
    public void FillGrid(GridView grd, string str)
    {
        DataTable fdt = GetDatatable(str);
        grd.DataSource = fdt;
        grd.DataBind();

    }
    public void FillDataList(DataList grd, string str)
    {
        DataTable fdt = GetDatatable(str);
        grd.DataSource = fdt;
        grd.DataBind();

    }
    public void sendmail(string toemail, string subject, string body)
    {
        try
        {
            SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587);
            smpt.EnableSsl = true;
            smpt.Credentials = new System.Net.NetworkCredential("fezterprjt@gmail.com", "fezter123");
            smpt.Send("fezterprjt@gmail.com", toemail, subject, body);
        }
        catch
        {
        }
    }



    //to fill Repeater
    public void FillRepeater(Repeater grd, string str)
    {
        DataTable fdt = GetDatatable(str);
        grd.DataSource = fdt;
        grd.DataBind();

    }

    //to fill dropdown
    public void FillDropdown(DropDownList dl, string str, string display, string value)
    {

        DataTable dt1 = GetDatatable(str);
        dl.DataSource = dt1;
        dl.DataTextField = display;
        dl.DataValueField = value;
        dl.DataBind();
    }
}


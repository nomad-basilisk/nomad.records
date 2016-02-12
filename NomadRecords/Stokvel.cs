using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//2016/01/13 -- Added by Mark 
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Configuration;

namespace NomadRecords
{
    public class Stokvel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string purpose { get; set; }
        public decimal contribution_amount { get; set; }
        public decimal joining_fee { get; set; }
        public DateTime inception_date { get; set; }


        public String insert()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("stokvel_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 250);
            SqlParameter contributionParam = new SqlParameter("@contribution_amount", SqlDbType.Money);
            SqlParameter joiningFeeParam = new SqlParameter("@joining_fee", SqlDbType.Money);
            SqlParameter purposeParam = new SqlParameter("@purpose", SqlDbType.Text);
            SqlParameter inceptionDateParam = new SqlParameter("@inception_date", SqlDbType.DateTime);
            SqlParameter user_idParam = new SqlParameter("@user_id", SqlDbType.BigInt);

            nameParam.Value = name;
            contributionParam.Value = contribution_amount;
            joiningFeeParam.Value = joining_fee;
            purposeParam.Value = purpose;
            inceptionDateParam.Value = inception_date;
            user_idParam.Value = User.id;

            cmd.Parameters.Add(nameParam);
            cmd.Parameters.Add(contributionParam);
            cmd.Parameters.Add(joiningFeeParam);
            cmd.Parameters.Add(purposeParam);
            cmd.Parameters.Add(inceptionDateParam);
            cmd.Parameters.Add(user_idParam);

            try
            {
                con.Open();
                var rowCount = cmd.ExecuteScalar();
                StaticStokvel.id = rowCount.ToString();
                MessageBox.Show(String.Format("Record {0} inserted", rowCount));

                return rowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
                return ex.ToString();
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public bool remove() 
        {
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("stokvel_remove", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter stokvel_idParam = new SqlParameter("@stokvel_id", SqlDbType.BigInt);

            stokvel_idParam.Value = id;
            
            cmd.Parameters.Add(stokvel_idParam);

            try
            {
                con.Open();
                var rowCount = cmd.ExecuteScalar();
                StaticStokvel.id = rowCount.ToString();
                MessageBox.Show("Stokvel succesfully removed");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString());
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }


    }

}

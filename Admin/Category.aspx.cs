using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Ecormerce.Admin
{
	public partial class Category : System.Web.UI.Page
	{
        // MySQL Connection objects
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter sda;
        DataTable dt;
		protected void Page_Load(object sender, EventArgs e)
		{
            lblMsg.Visible = false;
		}

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            ImagePreview.ImageUrl = string.Empty;
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int category_id = Convert.ToInt32(hfCategoryId.Value);
            con = new MySqlConnection(Utilities.getConnection());
            cmd = new MySqlCommand("Category_Crud", con);
            cmd.Parameters.AddWithValue("@Action", category_id == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@category_id", category_id);
            cmd.Parameters.AddWithValue("@category_name", txtCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if (FuCategoryImage.HasFile)
            {
                if (Utilities.isValidExtension(FuCategoryImage.FileName))
                {
                    string newImageName = Utilities.getUniqueId();
                    fileExtension = Path.GetExtension(FuCategoryImage.FileName);
                    imagePath = "Image/Category/" + newImageName.ToString() + fileExtension;
                    FuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category") + newImageName.ToString() + fileExtension );
                    cmd.Parameters.AddWithValue("@category_image_url", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "please select .jpg, .jpeg or .png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;

                }
            }
            else
            {
                isValidToExecute = true; 
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = category_id == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category" + actionName + "successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                catch(Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text ="Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger"; 
                }

                finally
                {
                    con.Close();
                }
            }


        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace icz_03
{

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateData("All States");
            }
        }

        private void GenerateData(string p)
        {
            string path = Server.MapPath("~/projects.xml");
            DataTable dt = new DataTable("project");
            try
            {

                dt.Columns.Add("id", typeof(System.String));
                dt.Columns.Add("name", typeof(System.String));
                dt.Columns.Add("abbreviation", typeof(System.String));
                dt.Columns.Add("customer", typeof(System.String));
                dt.Columns.Add("akcia", typeof(System.String));
      
                dt.ReadXml(path);

                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/projects.xml"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["id"] = ds.Tables[0].Rows[i].ItemArray[3];
                }

                if (!String.IsNullOrEmpty(p) && p != "All States")
                {
                    List<datatState> ar = new List<datatState>();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["name"].ToString() == p)
                            {
                                ar.Add(new datatState()
                                {
                                    Id = dt.Rows[i]["id"].ToString(),
                                    Name = dt.Rows[i]["name"].ToString(),
                                    Abbreviation = dt.Rows[i]["abbreviation"].ToString(),
                                    Customer = dt.Rows[i]["customer"].ToString()
                                });
                            }
                        }
                    }
                    dt.Clear();
                    DataRow dr;
                    foreach (var item in ar)
                    {
                        dr = dt.NewRow();
                        dr["id"] = item.Id;
                        dr["name"] = item.Name;
                        dr["abbreviation"] = item.Abbreviation;
                        dr["customer"] = item.Customer;
                        dt.Rows.Add(dr);
                    }

                    gvProjects.DataSource = dt;
                    gvProjects.DataBind();
                }
                else
                {

                    DataTable dtState = new DataTable("project");
                    dtState.Columns.Add("name", typeof(System.String));
                    dtState.ReadXml(path);

                    DataView dView = new DataView(dtState);
                    string[] arrColumns = { "name" };
                    
                    ddlState.Items.Clear();
                    ListItem l = new ListItem("- Všetky -", "All States", true);
                    l.Selected = true;
                    ddlState.Items.Add(l);

                    ddlState.DataSource = dView.ToTable(true, arrColumns);
                    ddlState.DataValueField = "name";
                    ddlState.DataBind();

                    gvProjects.DataSource = dt;
                    gvProjects.DataBind();

                }

            }
            catch (Exception ex)
            {
                lblErr.Text = ex.ToString();
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateData(ddlState.SelectedValue);
        }

        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            
            txtName.Text = Context.Server.HtmlDecode(gvRow.Cells[1].Text);
            txtAbbreviation.Text = Context.Server.HtmlDecode(gvRow.Cells[2].Text);
            txtCustomer.Text = Context.Server.HtmlDecode(gvRow.Cells[3].Text);

            hfProjID.Value = "1";
            hfEditID.Value = gvRow.Cells[0].Text; //gvRow.RowIndex.ToString();

            mpeProj.Show();

        }

        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;

            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/projects.xml"));
            ds.Tables[0].Rows.RemoveAt(gvRow.RowIndex);
            ds.WriteXml(Server.MapPath("~/projects.xml"));

            GenerateData("");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProjectEntity projekt = new ProjectEntity();
            projekt.ProjektId = Convert.ToInt32(hfProjID.Value);
            projekt.ProjektName = txtName.Text.Trim();
            projekt.ProjektAbbreviation = txtAbbreviation.Text.Trim();
            projekt.ProjektCustomer = txtCustomer.Text.Trim();

            if (projekt.ProjektId == 0)
                AddNewProjekt(projekt);
            else
            {

                UpdateProjekt(projekt, hfEditID.Value);
            }
                

            GenerateData("");
        }

        public bool AddNewProjekt(ProjectEntity projekt)
        {
            System.Xml.XmlDocument myXml = new System.Xml.XmlDocument();
            myXml.Load(Server.MapPath("~/projects.xml"));
            System.Xml.XmlElement ParentElement = myXml.CreateElement("project");
            
            System.Xml.XmlElement NamE = myXml.CreateElement("name");
            NamE.InnerText = projekt.ProjektName;
            System.Xml.XmlElement AbbreviatioN = myXml.CreateElement("abbreviation");
            AbbreviatioN.InnerText = projekt.ProjektAbbreviation;
            System.Xml.XmlElement CustomeR = myXml.CreateElement("customer");
            CustomeR.InnerText = projekt.ProjektCustomer;

            System.Xml.XmlNodeList nodeListCount = myXml.GetElementsByTagName("project");
            int nodeCount = nodeListCount.Count + 1;
            ParentElement.SetAttribute("id", "prj"+ nodeCount.ToString());

            ParentElement.AppendChild(NamE);
            ParentElement.AppendChild(AbbreviatioN);
            ParentElement.AppendChild(CustomeR);

            myXml.DocumentElement.AppendChild(ParentElement);
            myXml.Save(Server.MapPath("~/projects.xml"));            

            return true;

        }

        public bool UpdateProjekt(ProjectEntity projekt, string xmlrow)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/projects.xml"));

            int j = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if(ds.Tables[0].Rows[i].ItemArray[3].ToString() == xmlrow)
                {
                    j = i;
                    break;
                }
            }
            
            ds.Tables[0].Rows[j]["name"] = projekt.ProjektName;
            ds.Tables[0].Rows[j]["abbreviation"] = projekt.ProjektAbbreviation;
            ds.Tables[0].Rows[j]["customer"] = projekt.ProjektCustomer;

            ds.WriteXml(Server.MapPath("~/projects.xml"));

            return true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearPopupControls();
            mpeProj.Show();
        }

        private void ClearPopupControls()
        {
            hfProjID.Value = "0";
            txtName.Text = String.Empty;
            txtAbbreviation.Text = String.Empty;
            txtCustomer.Text = String.Empty;
        }
    }

    public class datatState
    {
        public string Id;
        public string Name;
        public string Abbreviation;
        public string Customer;
    }

    public class ProjectEntity
    {
        public Int32 ProjektId { get; set; }
        public String ProjektName { get; set; }
        public String ProjektAbbreviation { get; set; }
        public String ProjektCustomer { get; set; }
    }
}
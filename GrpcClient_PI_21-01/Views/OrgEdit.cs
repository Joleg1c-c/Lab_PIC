using GrpcClient_PI_21_01.Controllers;
//using GrpcClient_PI_21_01.Data;
using GrpcClient_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GrpcClient_PI_21_01.Views
{
    public partial class OrgEdit : Form
    {
        private string OrgId;
        public OrgEdit()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MessageBox.Show("Currently not working"); // remove later
            this.Close(); // remove later
        }

        public OrgEdit(string id)
        {
            InitializeComponent();
            OrgId = id;
            FillOrgEdit();
            MessageBox.Show("Currently not working"); // remove later
            this.Close(); // remove later
            name.Items.Clear();
            //foreach (Organization org in OrgRepository.Organizations)
            //{
            //    name.Items.Add(org.name);
            //}

            AdressReg.Items.Clear();
            //foreach (Location loc in LocationCostReposiroty.locationCosts)
            //{
            //    AdressReg.Items.Add(loc.City);
            //}
        }

        private void FillOrgEdit()
        {
            //var Idorg = OrgRepository.Organizations.FindIndex(x => x.idOrg == Convert.ToInt32(OrgId));
            //Organization org = OrgRepository.Organizations[Idorg];
            //name.Text = org.name;
            //INN.Text = org.INN;
            //KPP.Text = org.KPP;
            //AdressReg.Text = org.registrationAdress;
            //Type.Text = org.type;
            //Status.Text = org.status;
        }

        private void OKorgEdit_Click(object sender, EventArgs e)
        {


            //var org = new Organization(Convert.ToInt32(OrgId), name.Text, INN.Text, KPP.Text, AdressReg.Text, Type.Text, Status.Text);
            //OrgService.EditOrganization(org);
            this.Close();
        }

        private void CancelOrgEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrgEdit_Load(object sender, EventArgs e)
        {

        }
    }
}

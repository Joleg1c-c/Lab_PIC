using GrpcClient_PI_21_01.Controllers;
//using GrpcClient_PI_21_01.Data;
using GrpcClient_PI_21_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrpcClient_PI_21_01.Views
{
    public partial class OrgAdd : Form
    {
        public OrgAdd()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

        private void OKorgAdd_Click(object sender, EventArgs e)
        {
            //var org = new Organization(OrgRepository.Organizations.Max(x => x.idOrg) + 1, name.Text, INN.Text, KPP.Text, AdressReg.Text, Type.Text, Status.Text);
            //OrgService.AddOrganization(org);
            this.Close();
        }

        private void CancelOrgEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

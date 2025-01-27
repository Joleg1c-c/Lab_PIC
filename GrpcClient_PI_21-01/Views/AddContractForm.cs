﻿using GrpcClient_PI_21_01.Controllers;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace GrpcClient_PI_21_01.Views
{
    public partial class AddContractForm : Form
    {
        public bool ContToEdit;
        public int ContId;
        public AddContractForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ContToEdit = false;
            FillEditor();
        }

        public AddContractForm(int id)
        {
            InitializeComponent();
            ContToEdit = true;
            ContId = id;
            FillEditor();
        }

        public async void FillEditor()
        {
            if (ContToEdit)
            {
                //var index = ContractRepository.contract.FindIndex(x => x.IdContract == ContId);
                //Contract cont = ContractRepository.contract[index];
                var cont = await ContractService.GetContract(ContId);
                dateConclusion.Value = cont.DateConclusion;
                dateAction.Value = cont.ActionDate;
                await FullComboBox();
                cityCombo.Text = cont.LocationCost.City;
                CostText.Text = cont.Cost.ToString();
                executerCombo.Text = cont.Executer.name;
                customerCombo.Text = cont.Costumer.name;
            }
            else
                await FullComboBox();
        }

        public async Task FullComboBox()
        {
            var orgs = await OrgService.GetOrganizations();
            cityCombo.DataSource = new BindingSource(await LocationService.GetLocations(), null);
            cityCombo.DisplayMember = "City";
            cityCombo.ValueMember = "IdLocation";

            executerCombo.DataSource = new BindingSource(orgs, null);
            executerCombo.DisplayMember = "name";
            executerCombo.ValueMember = "idOrg";

            customerCombo.DataSource = new BindingSource(orgs, null);
            customerCombo.DisplayMember = "name";
            customerCombo.ValueMember = "idOrg";
        }

        private void CancelcontEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void OKcontAdd_Click(object sender, EventArgs e)
        {
            if (ContToEdit)
                if (CostText.Text == "")
                    MessageBox.Show("Вы не указали цену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (int.Parse(CostText.Text) == 0)
                    MessageBox.Show("Вы не можете указать цену раной 0.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!int.TryParse(CostText.Text, out int _))
                    MessageBox.Show("Вы ввели некоректную цену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //var cont = new string[]
                    //{
                    //        ContId.ToString(), dateConclusion.Value.ToString(),
                    //                dateAction.Value.ToString(), cityCombo.SelectedValue.ToString(),
                    //                CostText.Text, executerCombo.SelectedValue.ToString(),
                    //                customerCombo.SelectedValue.ToString()
                    //};
                    var contr = new Contract(ContId, dateConclusion.Value,
                        dateAction.Value, cityCombo.SelectedItem as Location,
                        int.Parse(CostText.Text), executerCombo.SelectedItem as Organization,
                        customerCombo.SelectedItem as Organization);
                    await ContractService.UpdateContract(contr);
                    this.Close();
                }
            else
                if (CostText.Text == "")
                MessageBox.Show("Вы не указали цену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (int.Parse(CostText.Text) == 0)
                MessageBox.Show("Вы не можете указать цену раной 0.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!int.TryParse(CostText.Text, out int _))
                MessageBox.Show("Вы ввели некоректную цену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                //var id = ContractRepository.contract.Max(x => x.IdContract) + 1;
                //var cont = new Contract(id,
                //                    dateConclusion.Value, dateAction.Value,
                //                    LocationCostReposiroty.locationCosts[int.Parse(cityCombo.SelectedValue.ToString()) - 1],
                //                    int.Parse(CostText.Text),
                //                    OrgRepository.Organizations[int.Parse(executerCombo.SelectedValue.ToString()) - 1],
                //                    OrgRepository.Organizations[int.Parse(customerCombo.SelectedValue.ToString()) - 1]);
                var contr = new Contract(-1,
                    dateConclusion.Value, dateAction.Value,
                    cityCombo.SelectedItem as Location,
                    int.Parse(CostText.Text),
                    executerCombo.SelectedItem as Organization,
                    customerCombo.SelectedItem as Organization);
                await ContractService.AddContract(contr);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var locationAdd = new LocationAdd();
            locationAdd.Show();
        }
    }
}

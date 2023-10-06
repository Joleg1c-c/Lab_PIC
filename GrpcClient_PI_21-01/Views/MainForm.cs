using GrpcClient_PI_21_01.Controllers;
//using GrpcClient_PI_21_01.Data;
using GrpcClient_PI_21_01.Models;
using GrpcClient_PI_21_01.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrpcClient_PI_21_01
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Task.Run(Setup);
        }
        DataSet dsApplication = new DataSet();
        DataSet dsOrganization = new DataSet();

        private async Task Setup()
        {
            await CreateData();
            await SetDataGridAct();
            await ShowContract();

            await SetDataGridApp();
            await SetDataGridOrg();
        }

        private async Task CreateData()
        {
            ContractTable.Rows.Clear();
            var cont = await ContractService.GetContracts();
            //var cont = ContractRepository.ShowCont(dateTimePicker3.Value.ToString(), dateTimePicker1.Value.ToString());
            foreach (var i in cont.Where(c => c.DateConclusion >= dateTimePicker3.Value
            && c.DateConclusion <= dateTimePicker1.Value).Select(c => ContractService.ToDataArray(c)))
                ContractTable.Rows.Add(i);
        }
        private async Task<bool> CheckPrivilege(NameMdels model)
        {
            if (!await PreveligeService.IsUserPrevilegedFor(model))
            {
                MessageBox.Show("У вас недостаточно прав!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }


        /* -----------------------------------ACT----------------------------------------------------- */

        private async Task SetDataGridAct()
        {
            DataGridViewActs.Rows.Clear();
            var actss = await ActService.GetActs();
            //var actss = ActService.ShowAct(dateTimePickerAct.Value.ToString());
            foreach (var act in actss.Where(a => a.Date >= dateTimePickerAct.Value)
                .Select(a => ActService.ToDataArray(a)))
                DataGridViewActs.Rows.Add(act);
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Act)) 
            {
                ActEdit editWindow = new ActEdit();
                editWindow.ShowDialog();
                await SetDataGridAct();
            }
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Act))
                if (DataGridViewActs.CurrentRow != null)
                {
                    ActEdit editWindow = new ActEdit(int.Parse(DataGridViewActs.CurrentRow.Cells[0].Value.ToString()));
                    editWindow.ShowDialog();
                    await SetDataGridAct();
                }
        }

        private void DeleteActButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //if (CheckPrivilege(NameMdels.Act))
            //    if (DataGridViewActs.CurrentRow != null)
            //    {
            //        ActService.DeleteAct(int.Parse(DataGridViewActs.CurrentRow.Cells[0].Value.ToString()));
            //        SetDataGridAct();
            //    }
        }

        private async void buttonAnimalCard_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Act))
                if (DataGridViewActs.CurrentRow != null)
                {
                    bool IsDog = int.Parse(DataGridViewActs.CurrentRow.Cells[1].Value.ToString()) > 0 ? true : false;
                    bool IsCat = int.Parse(DataGridViewActs.CurrentRow.Cells[2].Value.ToString()) > 0 ? true : false;

                    if (IsDog)
                    {
                        AnimalCardForm otvet = new AnimalCardForm(int.Parse(DataGridViewActs.CurrentRow.Cells[0].Value.ToString()), "Собака");
                        otvet.ShowDialog();
                    }
                    if (IsCat)
                    {
                        AnimalCardForm otvet = new AnimalCardForm(int.Parse(DataGridViewActs.CurrentRow.Cells[0].Value.ToString()), "Кот");
                        otvet.ShowDialog();
                    }
                }
        }

        private async void dateTimePickerAct_ValueChanged(object sender, EventArgs e)
        {
            await SetDataGridAct();
        }


        private async Task SetDataGridOrg()
        {
            /*-Organization-------------------------*/
            dsOrganization.Tables.Clear();
            dsOrganization.Tables.Add("Score");
            dsOrganization.Tables[0].Columns.Add("Номер");
            dsOrganization.Tables[0].Columns.Add("Наименование");
            dsOrganization.Tables[0].Columns.Add("ИНН");
            dsOrganization.Tables[0].Columns.Add("КПП");
            dsOrganization.Tables[0].Columns.Add("Адрес регистрации");
            dsOrganization.Tables[0].Columns.Add("Тип");
            dsOrganization.Tables[0].Columns.Add("Статус");
            var orgs = await OrgService.GetOrganizations();
            //var orgs = OrgService.ShowOrganizations();
            foreach (var org in orgs.Select(o => OrgService.ToDataArray(o)))
            {
                dsOrganization.Tables[0].Rows.Add(org);
            }
            dataGridViewOrg.DataSource = dsOrganization.Tables[0];
        }
        private async Task SetDataGridApp()
        {
            /*-Applications-------------------------*/
            dsApplication.Tables.Clear();
            dsApplication.Tables.Add("Score");
            dsApplication.Tables[0].Columns.Add("Дата подачи");
            dsApplication.Tables[0].Columns.Add("Номер");
            dsApplication.Tables[0].Columns.Add("Населенный пункт");
            dsApplication.Tables[0].Columns.Add("Территория");
            dsApplication.Tables[0].Columns.Add("Место обитания");
            dsApplication.Tables[0].Columns.Add("Срочность исполнения");
            dsApplication.Tables[0].Columns.Add("Описание животного");
            dsApplication.Tables[0].Columns.Add("Категория заявителя");
            var apps = await AppService.GetApplications();
            foreach (var app in apps.Select(a => AppService.ToDataArray(a)))
            {
                dsApplication.Tables[0].Rows.Add(app);
            }
            dataGridViewApp.DataSource = dsApplication.Tables[0];
        }

        /*------------------------------------------------------------------*/
        private void AppDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //if (CheckPrivilege(NameMdels.App))
            //    if (dataGridViewApp.CurrentRow != null)
            //    {
            //        int app = Convert.ToInt32(dataGridViewApp.CurrentRow.Cells[1].Value.ToString());
            //        AppService.DeleteApplication(app);
            //        await SetDataGridApp();
            //    }
        }

        private void AppEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //if (CheckPrivilege(NameMdels.App))
            //    if (dataGridViewApp.CurrentRow != null)
            //    {
            //        string app = dataGridViewApp.CurrentRow.Cells[1].Value.ToString();
            //        AppEdit appEdit = new AppEdit(app);
            //        appEdit.ShowDialog();
            //        await SetDataGridApp();
            //    }
        }

        private void OrgDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //if (CheckPrivilege(NameMdels.Org))
            //    if (dataGridViewOrg.CurrentRow != null)
            //    {
            //        string org = dataGridViewOrg.CurrentRow.Cells[2].Value.ToString();
            //        OrgService.DeleteOrganization(org);
            //        await SetDataGridOrg();
            //    }
        }

        private async void OrgEdit_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Org))
                if (dataGridViewOrg.CurrentRow != null)
                {
                    string org = dataGridViewOrg.CurrentRow.Cells[0].Value.ToString();
                    OrgEdit orgEdit = new OrgEdit(org);
                    orgEdit.ShowDialog();
                    await SetDataGridOrg();
                }
        }

        private async void OrgAdd_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Org))
            {
                OrgAdd orgAdd = new OrgAdd();
                orgAdd.ShowDialog();
                await SetDataGridOrg();
            }
        }

        private async void AppAdd_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.App))
            {
                AppAdd appAdd = new AppAdd();
                appAdd.ShowDialog();
                await SetDataGridApp();
            }
        }

        private async Task ShowContract()
        {
            ContractTable.Rows.Clear();
            var cont = await ContractService.GetContracts();
            //var contract = ContractService.ShowContract(dateTimePicker3.Value.ToString(), dateTimePicker1.Value.ToString());
            foreach (var i in cont.Where(c => c.DateConclusion >= dateTimePicker3.Value
            && c.DateConclusion <= dateTimePicker1.Value).Select(c => ContractService.ToDataArray(c)))
            {
                ContractTable.Rows.Add(i);
            }
        }
        private async void AddButton_Click_1(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Contract))
            {
                AddContractForm contAdd = new AddContractForm();
                contAdd.ShowDialog();
                await ShowContract();
            }
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (await CheckPrivilege(NameMdels.Contract))
                if (ContractTable.CurrentRow != null)
                {
                    AddContractForm contAdd = new AddContractForm(int.Parse(ContractTable.CurrentRow.Cells[0].Value.ToString()));
                    contAdd.ShowDialog();
                    await ShowContract();
                }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //if (CheckPrivilege(NameMdels.Contract))
            //    if (ContractTable.CurrentRow != null)
            //    {
            //        ContractService.DeleteContract(ContractTable.CurrentRow.Cells[0].Value.ToString());
            //        await ShowContract();
            //    }
        }

        private async void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            await ShowContract();
        }

        private async void ContractTable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AddContractForm contAdd = new AddContractForm(int.Parse(ContractTable.CurrentRow.Cells[0].Value.ToString()));
            contAdd.ShowDialog();
            await ShowContract();
        }

        private void filterAppDate_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //dsApplication.Tables[0].Rows.Clear();
            //var apps = AppService.FilterByDate(filterAppDate.Value.ToString(), filterAppDate2.Value.ToString());
            //foreach (var app in apps)
            //    dsApplication.Tables[0].Rows.Add(app);
            //dataGridViewApp.DataSource = dsApplication.Tables[0];
        }

        private void filterAppDate2_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Currently not working");
            //dsApplication.Tables[0].Rows.Clear();
            //var apps = AppService.FilterByDate(filterAppDate.Value.ToString(), filterAppDate2.Value.ToString());
            //foreach (var app in apps)
            //    dsApplication.Tables[0].Rows.Add(app);
            //dataGridViewApp.DataSource = dsApplication.Tables[0];
        }

        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            await ShowContract();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenReport();
        }

        private async void OpenReport()
        {
            if (await CheckPrivilege(NameMdels.Report))
            {
                ReportForm rep = new ReportForm();
                rep.ShowDialog();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                tabControl1.SelectedTab = tabPage1;
                OpenReport();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}

﻿using Lab_PIC_5.Controllers;
using Lab_PIC_5.Data;
using Lab_PIC_5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_PIC_5.Views
{
    public partial class ActEdit : Form
    {
        private bool actToEdit;
        private int actId;
        
        public ActEdit()
        {
            InitializeComponent();
            actToEdit = false;
            Isus.Text = "Добавление акта";
            FillEditor();
        }
        public ActEdit(int id)
        {
            InitializeComponent();
            actToEdit = true;
            actId = id;
            Isus.Text = "Редактирование акта";
            FillEditor();
        }

        private void FillEditor()
        {
            if (actToEdit)
            {
                var index = ActRepository.acts.FindIndex(x => x.ActNumber == actId);
                Act act = ActRepository.acts[index];
                numericUpDownDog.Value = act.CountDogs;
                numericUpDownCat.Value = act.CountCats;
                dateAct.Value = act.Date;
                textBoxTarget.Text = act.TargetCapture;
                FullComboBox();
                comboBoxOrganization.Text = act.Organization.name;
                comboBoxContract.Text = act.Contracts.IdContract.ToString();
                comboBoxApp.Text = act.Application.number.ToString();
            }
            else
            {
                FullComboBox();
            }
        }

        private void FullComboBox()
        {
            comboBoxOrganization.DataSource = new BindingSource(
                                    OrgRepository.Organizations, null);
            comboBoxOrganization.DisplayMember = "name";
            comboBoxOrganization.ValueMember = "idOrg";

            comboBoxContract.DataSource = new BindingSource(
                    ContractRepository.contract, null);
            comboBoxContract.DisplayMember = "IdContract";
            comboBoxContract.ValueMember = "IdContract";

            comboBoxApp.DataSource = new BindingSource(
                    AppRepository.Applicatiions, null);
            comboBoxApp.DisplayMember = "number";
            comboBoxApp.ValueMember = "number";
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (actToEdit)
            {
                var act = new string[] {actId.ToString(), numericUpDownDog.Value.ToString(),numericUpDownCat.Value.ToString(), comboBoxOrganization.SelectedValue.ToString(),
                    dateAct.Value.ToString(), textBoxTarget.Text, comboBoxApp.SelectedValue.ToString(), comboBoxContract.SelectedValue.ToString()};
                ActService.EditAct(act);
            }
            else
            {
                var act = new string[] {numericUpDownDog.Value.ToString(),numericUpDownCat.Value.ToString(), comboBoxOrganization.SelectedValue.ToString(),
                    dateAct.Value.ToString(), textBoxTarget.Text, comboBoxApp.SelectedValue.ToString(), comboBoxContract.SelectedValue.ToString()};

                int kolD = int.Parse(act[0]) > 0 ? 1 : 0;
                int kol = int.Parse(act[1]) > 0 ? 1 + kolD : 0 + kolD;
                bool flag = true;
                Dictionary<int, string> animalDictionary = new Dictionary<int, string>() { { 0, "Собака" }, { 1, "Кот" } };
                List<string[]> listAnimals = new List<string[]>();

                for (int i = 0; i < kol; i++)
                {
                    var animForm = new AnimalCardForm(animalDictionary[i]);
                    DialogResult otvet = animForm.ShowDialog();
                    if (otvet == DialogResult.OK)
                    {
                        listAnimals.Add(animForm.returnAnime);
                    }
                    if (otvet == DialogResult.Cancel)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    ActService.Save(act);
                    foreach (var animal in listAnimals)
                        AnimalCardService.AddAnimalCard(animal);
                }
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrpcClient_PI_21_01.Controllers;
//using GrpcClient_PI_21_01.Data;
using GrpcClient_PI_21_01.Models;

namespace GrpcClient_PI_21_01.Views
{
    public partial class LocationAdd : Form
    {
        public LocationAdd()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MessageBox.Show("Currently not working"); // remove later
        }

        private void OKcontAdd_Click(object sender, EventArgs e)
        {
            //if (CityText.Text == "")
            //    MessageBox.Show("Вы не указали город.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //{
            //    var loc = new Location(LocationCostReposiroty.locationCosts.Max(x => x.IdLocation) + 1, CityText.Text);
            //    LocationCostReposiroty.locationCosts.Add(loc);
            //    this.Close();
            //}
        }

        private void CancelcontEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
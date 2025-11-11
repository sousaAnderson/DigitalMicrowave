using DigitalMicrowave.Application.Services;
using DigitalMicrowave.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Infrastructure.Repositories;

namespace DigitalMicrowave
{
    public partial class HeatingPrograms : System.Web.UI.Page
    {
        private static HeatingProgramService _service;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_service == null)
            {
                var repo = new HeatingProgramRepository();
                _service = new HeatingProgramService(repo);
            }

            if (!IsPostBack)
                LoadGrid();
        }
        void LoadGrid()
        {
            gridPrograms.DataSource = _service.GetAll();
            gridPrograms.DataBind();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            modalTitle.InnerText = "Novo Programa";
            txtId.Value = "";
            txtName.Text = txtFood.Text = txtTime.Text = txtPower.Text = txtChar.Text = txtInstructions.Text = "";
            lblError.Text = "";
            modal.Visible = true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var program = new HeatingProgram
                {
                    Id = string.IsNullOrWhiteSpace(txtId.Value) ? 0 : int.Parse(txtId.Value),
                    ProgramName = txtName.Text,
                    Food = txtFood.Text,
                    Time = int.Parse(txtTime.Text),
                    Power = int.Parse(txtPower.Text),
                    HeatingCharacteristic = txtChar.Text,
                    Instructions = txtInstructions.Text
                };

                if (program.Id == 0)
                    _service.Create(program);
                else
                    _service.Update(program);

                modal.Visible = false;
                LoadGrid();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            modal.Visible = false;
        }
        protected void gridPrograms_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "edit")
            {
                var p = _service.GetAll().First(x => x.Id == id);

                txtId.Value = p.Id.ToString();
                txtName.Text = p.ProgramName;
                txtFood.Text = p.Food;
                txtTime.Text = p.Time.ToString();
                txtPower.Text = p.Power.ToString();
                txtChar.Text = p.HeatingCharacteristic;
                txtInstructions.Text = p.Instructions;
                lblError.Text = "";

                modalTitle.InnerText = "Editar Programa";
                modal.Visible = true;
            }

            if (e.CommandName == "delete")
            {
                _service.Delete(id);
                LoadGrid();
            }
        }
        protected void gridPrograms_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }
    }
}
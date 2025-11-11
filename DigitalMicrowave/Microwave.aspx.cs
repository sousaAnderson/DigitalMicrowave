using DigitalMicrowave.Application.Services;
using DigitalMicrowave.Domain.Enums;
using DigitalMicrowave.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigitalMicrowave
{
    public partial class Microwave : System.Web.UI.Page
    {
        private HeatingProgramService _programService;
        private MicrowaveServices MicrowaveService
        {
            get
            {
                if (Session["microwave"] == null)
                    Session["microwave"] = new MicrowaveServices();
                return (MicrowaveServices)Session["microwave"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_programService == null)
            {
                var repo = new HeatingProgramRepository();
                _programService = new HeatingProgramService(repo);
            }
            if (!IsPostBack)
            {
                LoadPrograms();
            }
        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            bool isPreset = ViewState["IsPreset"] != null && (bool)ViewState["IsPreset"];

            if (isPreset && timer.Enabled)
            {
                lblProgress.Text = "Não é permitido adicionar tempo em programas pré-definidos!";
                return;
            }

            int time = string.IsNullOrWhiteSpace(txtTempo.Text) ? 0 : int.Parse(txtTempo.Text);
            int power = string.IsNullOrWhiteSpace(txtPotencia.Text) ? 0 : int.Parse(txtPotencia.Text);

            try
            {
                MicrowaveService.Start(time, power);
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                lblProgress.Text = ex.Message;
            }
        }

        protected void btnPauseCancel_Click(object sender, EventArgs e)
        {
            MicrowaveService.PauseOrCancel();

            if (MicrowaveService.Heated.State == MicrowaveStateEnum.Cancelled)
            {
                timer.Enabled = false;
                lblProgress.Text = "";
                lblTime.Text = "--";
            }
            else if (MicrowaveService.Heated.State == MicrowaveStateEnum.Paused)
            {
                timer.Enabled = false;
            }
        }

        protected void btnQuick_Click(object sender, EventArgs e)
        {
            MicrowaveService.Start(30, 10);
            timer.Enabled = true;
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            if (MicrowaveService.Heated.State == MicrowaveStateEnum.Paused)
                MicrowaveService.Resume();

            MicrowaveService.ProcessTick(ViewState["HeatingChar"]?.ToString());

            lblProgress.Text = MicrowaveService.Heated.ProgressText;
            lblTime.Text = MicrowaveService.GetFormattedTime();

            if (MicrowaveService.Heated.State == MicrowaveStateEnum.Finished)
                timer.Enabled = false;
        }
        protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPrograms.SelectedValue))
            {
                txtTempo.Text = "";
                txtPotencia.Text = "";
                txtTempo.Enabled = true;
                txtPotencia.Enabled = true;
                return;
            }

            int id = int.Parse(ddlPrograms.SelectedValue);
            var program = _programService.GetAll().First(x => x.Id == id);

            txtTempo.Text = program.Time.ToString();
            txtPotencia.Text = program.Power.ToString();

            if (program.ProgramDefault)
            {
                txtTempo.Enabled = false;
                txtPotencia.Enabled = false;
                ViewState["IsPreset"] = true;
                ViewState["HeatingChar"] = program.HeatingCharacteristic;
                btnQuick.Enabled = false;
            }
            else
            {
                txtTempo.Enabled = true;
                txtPotencia.Enabled = true;
                ViewState["IsPreset"] = false;
                ViewState["HeatingChar"] = program.HeatingCharacteristic;
                btnQuick.Enabled = true;
            }
        }

        void LoadPrograms()
        {
            var list = _programService.GetAll();

            ddlPrograms.Items.Clear();
            ddlPrograms.Items.Add(new ListItem("-- Selecione um programa --", ""));

            foreach (var p in list)
            {
                var item = new ListItem(p.ProgramName, p.Id.ToString());

                if (!p.ProgramDefault)
                    item.Attributes.Add("style", "font-style:italic;");

                ddlPrograms.Items.Add(item);
            }
        }
    }
}
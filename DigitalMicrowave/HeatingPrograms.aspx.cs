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
using System.Drawing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace DigitalMicrowave
{
    public partial class HeatingPrograms : System.Web.UI.Page
    {
        private static HeatingProgramService _service;
        
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["AuthToken"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }
            
            if (_service == null)
            {
                var repo = new HeatingProgramRepository();
                _service = new HeatingProgramService(repo);
            }

            if (!IsPostBack)
                LoadGrid();
        }
        protected async void LoadGrid()
        {
            try
            {
                using (var client = ApiHelper.GetHttpClient())
                {
                    var response = await client.GetAsync("api/heatingprograms");
                    if (!response.IsSuccessStatusCode)
                    {
                        lblError.Text = $"Erro ao carregar programas de aquecimento: {response.ReasonPhrase}";
                        gridPrograms.Enabled = false;
                        return;
                    }

                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<HeatingProgram>>(json);
                    gridPrograms.DataSource = data;
                    gridPrograms.DataBind();
                }                  

               
            }
            catch (Exception ex) 
            {
                lblError.Text = ex.Message;
            }
           
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            modalTitle.InnerText = "Novo Programa";
            txtId.Value = "";
            txtName.Text = txtFood.Text = txtTime.Text = txtPower.Text = txtChar.Text = txtInstructions.Text = "";
            lblError.Text = "";
            modal.Visible = true;
        }
        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var http = ApiClient.Client();
                string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

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

                string jsonContent = JsonConvert.SerializeObject(program);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                if (program.Id == 0)
                    await http.PostAsync($"{apiUrl}/heatingprograms", content);
                else
                    await http.PutAsync($"{apiUrl}/heatingprograms", content);

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
        protected async void gridPrograms_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            var http = ApiClient.Client();
            string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            if (e.CommandName == "edit")
            {
                var response = await http.GetAsync($"{apiUrl}/heatingprograms");
                string json = await response.Content.ReadAsStringAsync();
                var heatings = JsonConvert.DeserializeObject<List<HeatingProgram>>(json);
                var heating = heatings.First(x => x.Id == id);

                txtId.Value = heating.Id.ToString();
                txtName.Text = heating.ProgramName;
                txtFood.Text = heating.Food;
                txtTime.Text = heating.Time.ToString();
                txtPower.Text = heating.Power.ToString();
                txtChar.Text = heating.HeatingCharacteristic;
                txtInstructions.Text = heating.Instructions;
                lblError.Text = "";

                modalTitle.InnerText = "Editar Programa";
                modal.Visible = true;
            }

            if (e.CommandName == "delete")
            {
                await http.DeleteAsync($"{apiUrl}/{id}");
                LoadGrid();
            }
        }
        protected void gridPrograms_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }
    }
}
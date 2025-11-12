using System;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;

namespace DigitalMicrowave
{
    public partial class Login : System.Web.UI.Page
    {
        private class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var http = new HttpClient();
                var loginData = new List<KeyValuePair<string, string>>
                {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("user", txtUser.Text),
                new KeyValuePair<string, string>("password", FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text,  "SHA1"))
                };

                var content = new FormUrlEncodedContent(loginData);
                string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
                var response = await http.PostAsync($"{apiUrl}/auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    lblMsg.Text = "Login inválido!";
                    return;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

                Session["AuthToken"] = token.AccessToken;
                SaveToken(token.AccessToken);

                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Login OK!";
                Response.Redirect("Microwave.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = $"Ocorreu um erro: {ex.Message}";
            }
        }
        private void SaveToken(string token)
        {
            var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings.Remove("JwtToken");
            config.AppSettings.Settings.Add("JwtToken", token);
            config.Save();
        }
    }
}
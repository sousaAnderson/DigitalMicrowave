<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Microwave.aspx.cs" Inherits="DigitalMicrowave.Microwave" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>

                <h2>Micro-ondas</h2>
                Programas:
                <asp:DropDownList ID="ddlPrograms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPrograms_SelectedIndexChanged" />
                 <br />
                Tempo (s):
            <asp:TextBox ID="txtTempo" runat="server" />
                <br />

                Potência (1-10):
            <asp:TextBox ID="txtPotencia" runat="server" />
                <br />
                <br />

                <asp:Button ID="btnStart" runat="server" Text="Iniciar" OnClick="btnStart_Click" />
                <asp:Button ID="btnQuick" runat="server" Text="Início Rápido" OnClick="btnQuick_Click" />
                <asp:Button ID="btnPauseCancel" runat="server" Text="Pausar / Cancelar" OnClick="btnPauseCancel_Click" />

                <br />
                <br />

                <strong>Tempo Restante:</strong>
                <asp:Label ID="lblTime" runat="server" Text="--" />

                <br />
                <br />

                <strong>Status:</strong><br />
                <asp:Label ID="lblProgress" runat="server" Text="" />

                <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="timer_Tick" Enabled="false" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </main>
</asp:Content>

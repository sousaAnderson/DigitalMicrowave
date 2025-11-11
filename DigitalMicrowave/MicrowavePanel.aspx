<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MicrowavePanel.aspx.cs" Inherits="DigitalMicrowave.MicrowavePanel" %>
<asp:Content ID="PanelContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
    <section class="row" aria-labelledby="aspnetTitle">
        <h1 id="aspnetTitle">MICRO-ONDAS DIGITAL</h1>
        <p class="lead">Esse é um Portal WEB ASP.NET desenvolvido para simular um micro-ondas digital.</p>
    </section>

    <div class="row">
        <section class="col-md-4" aria-labelledby="gettingStartedTitle">
            <h2 id="gettingStartedTitle">Requisitos Obrigatórios</h2>
           <ul>
              <li>Utilize conceitos de orientação a objetos;</li>
              <li>.Net Framework 4.0 ou superior;</li>
              <li>Não se preocupar com o visual do formulário, mas sim com a implementação do micro-ondas </li>
              <li>Separar as camadas de interface de usuário e de negócio;</li>
              <li>O programa desenvolvido deve funcionar conforme os requisitos de cada nível;</li>
            </ul>
        </section>
        <section class="col-md-4" aria-labelledby="librariesTitle">
            <h2 id="librariesTitle">Requisitos Desejáveis</h2>
           <ul>
               <li>Observar os princípios SOLID.</li>
               <li>Design patterns.</li>
               <li>Boas práticas e qualidade de código visando facilidade de leitura e compreensão.</li>
                <li>Implementar as classes de maneira a prevenir o uso incorreto, protegendo devidamente o acesso aos dados
e métodos.</li>
                 <li>O programa desenvolvido deve funcionar conforme os requisitos de cada nível;</li>
              <li>Documentar o código quando necessário.</li>
               <li>Implementar testes unitários para a camada de negócio..</li>
             </ul>
        </section>
        <section class="col-md-4" aria-labelledby="hostingTitle">
            <h2 id="hostingTitle">Padrões e Tecnologias Empregadas</h2>
            <ul>
               <li>ASP.NET Web Forms</li>
               <li>C# com .Net Framework 4.7.2;</li>
               <li>Design patterns - Arquitetura em camadas </li>
               <li>Banco de dados SQL Server;</li>
            </ul>
        </section>
    </div>
</main>
</asp:Content>

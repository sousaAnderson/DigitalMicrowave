<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DigitalMicrowave.Login" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <title>LOGIN</title>
</head>
<body>
     <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
     <div class="container">
         <a class="navbar-brand" runat="server" href="~/Login">MICRO-ONDAS DIGITAL</a>         
     </div>
 </nav>
 <div class="container body-content">
        <form id="form1" runat="server">            
        <div class="row">
            <div class="col-12">
                 Username: <asp:TextBox ID="txtUser" runat="server" />
            </div>
           <div class="col-12">
               Password: <asp:TextBox ID="txtPass" TextMode="Password" runat="server" />
           </div>
        </div>
            <div class="row">
                <div class="col-3">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                </div>                
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
                </div>                
            </div>
    </form>
     <hr />
     <footer>
         <p>&copy; José Anderson Pereira de Sousa</p>
     </footer>
 </div>    
</body>
</html>

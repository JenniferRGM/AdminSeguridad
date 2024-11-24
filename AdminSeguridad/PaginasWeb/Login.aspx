<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Inicio de Sesión - Sistema de Seguridad</title>
    <link rel="stylesheet" href="Estilos/Login.css">
</head>
<body>
    <form id="form1" runat="server">
        <section>
            
            <span></span><span></span><span></span><span></span><span></span>
            <span></span><span></span><span></span><span></span><span></span>
            <span></span><span></span><span></span><span></span><span></span>
            <span></span><span></span><span></span><span></span><span></span>
            
           
            <div class="signin">
                <div class="content">
                    <h2>Inicio de Sesión</h2>
                    <div class="form">
                        
                        <div class="inputBox">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="input" placeholder=" " required="required"></asp:TextBox>
                            <i>Nombre de usuario</i>
                        </div>
                        
                        <div class="inputBox">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" placeholder=" " required="required"></asp:TextBox>
                            <i>Contraseña</i>
                        </div>
                        
                        <div class="links">
                            <a href="#">Olvidé mi contraseña</a>
                            <a href="#">Registrarse</a>
                        </div>
                        
                        <div class="inputBox">
                            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btnLogin" OnClick="btnLogin_Click" />
                        </div>
                        
                        <div class="inputBox">
                            <asp:Label ID="lblError" runat="server" CssClass="error" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>


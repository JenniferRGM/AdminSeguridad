<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.GestionUsuarios" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Usuarios</title>
    <link rel="stylesheet" href="../Estilos/GestionUsuarios.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Gestión de Usuarios</h1>

            <asp:GridView ID="gvUsuarios" runat="server" CssClass="table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="UsuarioID" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido1" HeaderText="Primer Apellido" />
                    <asp:BoundField DataField="Apellido2" HeaderText="Segundo Apellido" />
                    <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha de Creación" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaActualizacion" HeaderText="Fecha de Actualización" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="RolNombre" HeaderText="Rol" />
                </Columns>
            </asp:GridView>

            <div class="form-container">
                <h2>Crear/Actualizar Usuario</h2>
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />

                <label for="txtApellido1">Primer Apellido</label>
                <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control" />

                <label for="txtApellido2">Segundo Apellido</label>
                <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control" />

                <label for="txtEmail">Correo Electrónico</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />

                <label for="txtClave">Contraseña</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" />

                <label for="ddlProvincia">Provincia</label>
                <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-control" />

                <label for="txtCanton">Cantón</label>
                <asp:DropDownList ID="txtCanton" runat="server" CssClass="form-control" />

                <label for="txtDistrito">Distrito</label>
                <asp:DropDownList ID="txtDistrito" runat="server" CssClass="form-control" />

                <label for="txtOtrasSenas">Otras señas</label>
                <asp:TextBox ID="txtOtrasSenas" runat="server" CssClass="form-control" />

                <label for="ddlRol">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" />

                <label for="txtTelefono">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />

               
                <div class="form-buttons">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn" OnClick="GuardarUsuario_Click" />
                    <asp:Button ID="btnEscribir" runat="server" Text="Escribir" CssClass="btn" />
                    <asp:Button ID="btnLeer" runat="server" Text="Leer" CssClass="btn" />
                    <asp:Button ID="btnRegresarHome" runat="server" Text="Regresar" CssClass="btn btn-home" OnClick="BtnRegresarHome_Click" />
                </div>
            </div>

            <div class="details-container">
                <h2>Detalles del Usuario</h2>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre: " CssClass="details-label" />
                <asp:Label ID="lblApellido1" runat="server" Text="Primer Apellido: " CssClass="details-label" />
                <asp:Label ID="lblApellido2" runat="server" Text="Segundo Apellido: " CssClass="details-label" />
                <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico: " CssClass="details-label" />
            </div>

            <div class="message">
                <asp:Label ID="lblMensaje" runat="server" CssClass="message-label" />
            </div>
        </div>
    </form>
</body>
</html>




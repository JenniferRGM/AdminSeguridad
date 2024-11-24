<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.Reportes" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Reportes</title>
    <link rel="stylesheet" href="../Estilos/Reportes.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Gestión de Reportes</h1>

            
            <asp:Label ID="lblMensajePermiso" runat="server" CssClass="message-label" />

           
            <div class="buttons">
                <asp:Button ID="btnLeer" runat="server" Text="Leer" CssClass="btn" OnClick="btnLeer_Click" Visible="false" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn" Visible="false" />
                <asp:Button ID="btnRegresarHome" runat="server" Text="Regresar a Home" CssClass="btn btn-home" OnClick="BtnRegresarHome_Click" />
            </div>

           
            <asp:GridView ID="gvReportes" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="UsuarioNombre" HeaderText="Generado Por" />
                </Columns>
            </asp:GridView>

            
            <div class="details-container">
                <h2>Detalles del Reporte</h2>
                <p><strong>ID:</strong> <asp:Label ID="lblID" runat="server" Text="N/A" /></p>
                <p><strong>Descripción:</strong> <asp:Label ID="lblDescripcion" runat="server" Text="N/A" /></p>
                <p><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" Text="N/A" /></p>
            </div>

            
            <div class="message">
                <asp:Label ID="lblMensaje" runat="server" CssClass="message-label" />
            </div>
        </div>
    </form>
</body>
</html>






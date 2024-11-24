<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.Configuracion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Página de Configuración</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Configuración</h2>

            
            <asp:Label ID="lblMensajePermiso" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br /><br />

            
            <asp:GridView ID="gvConfiguraciones" runat="server" AutoGenerateColumns="false" DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Valor" HeaderText="Valor" />
                </Columns>
            </asp:GridView>
            <br />

            
            <asp:Label ID="lblID" runat="server" Text="ID:" Visible="false"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" Placeholder="Descripción"></asp:TextBox>
            <asp:TextBox ID="txtValor" runat="server" Placeholder="Valor"></asp:TextBox>
            <br /><br />

            
            <asp:Button ID="btnLeer" runat="server" Text="Leer Configuración" OnClick="BtnLeer_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar Configuración" OnClick="BtnModificar_Click" />
            <br /><br />

           
            <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>



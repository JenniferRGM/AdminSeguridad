<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historial.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.Historial" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Historial</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .container {
            max-width: 900px;
            margin: 0 auto;
        }
        h1 {
            text-align: center;
            color: #333;
        }
        .message-permission {
            color: #0066cc;
            font-size: 14px;
            margin-bottom: 10px;
            display: block;
            text-align: center;
        }
        .table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 16px;
            text-align: left;
        }
        .table th, .table td {
            border: 1px solid #ddd;
            padding: 8px;
        }
        .table th {
            background-color: #f2f2f2;
            color: #333;
        }
        .table tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .table tr:hover {
            background-color: #f1f1f1;
        }
        .message {
            color: #28a745;
            font-weight: bold;
            text-align: center;
            margin: 15px 0;
        }
        .btn-home {
            display: block;
            margin: 20px auto;
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            text-align: center;
            text-decoration: none;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-home:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Historial</h1>

           
            <asp:Label ID="lblMensajePermiso" runat="server" CssClass="message-permission"></asp:Label>

           
            <asp:GridView ID="gvHistorial" runat="server" CssClass="table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="UsuarioID" HeaderText="UsuarioID" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Modulo" HeaderText="Módulo" />
                    <asp:BoundField DataField="TipoEvento" HeaderText="Tipo de Evento" />
                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha de Creación" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </asp:GridView>

            
            <asp:Label ID="lblMensaje" runat="server" CssClass="message"></asp:Label>

            
            <asp:Button ID="btnRegresarHome" runat="server" Text="Regresar a Home" CssClass="btn-home" OnClick="BtnRegresarHome_Click" />
        </div>
    </form>
</body>
</html>








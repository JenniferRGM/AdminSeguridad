<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard - AdminSeguridad</title>
    <link rel="stylesheet" href="../style.css" />
    <style>
        .container {
            max-width: 800px;
            margin: 0 auto;
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .container h1 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        .gridview {
            margin: 20px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
            overflow: hidden;
        }

        .gridview th {
            background-color: #007BFF;
            color: #fff;
            padding: 10px;
        }

        .gridview td {
            padding: 10px;
            text-align: center;
        }

        .actions {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }

        .input-text {
            flex: 1;
            padding: 10px;
            margin-right: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .btn {
            padding: 10px 20px;
            border: none;
            background-color: #007BFF;
            color: #fff;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .lblMensaje {
            text-align: center;
            color: #d9534f;
            margin-top: 10px;
        }

        .details {
            margin-top: 20px;
            padding: 15px;
            background-color: #e9ecef;
            border-radius: 5px;
        }

        .details h3 {
            margin-bottom: 10px;
        }

        .details p {
            margin: 5px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Menú del Dashboard</h1>

            
            <asp:GridView ID="gvDashboard" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                </Columns>
            </asp:GridView>

            
            <div class="actions">
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-text" Placeholder="Descripción"></asp:TextBox>
                <asp:Button ID="btnLeer" runat="server" Text="Leer" CssClass="btn" OnClick="BtnLeer_Click" />
                <asp:Button ID="btnEscribir" runat="server" Text="Escribir" CssClass="btn" OnClick="BtnEscribir_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn" OnClick="BtnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn" OnClick="BtnEliminar_Click" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar al Menú Principal" CssClass="btn" OnClick="BtnRegresar_Click" />

            </div>

            
            <asp:Label ID="lblMensaje" runat="server" CssClass="lblMensaje"></asp:Label>

            
            <div class="details">
                <h3>Detalles del Registro</h3>
                <p><strong>ID:</strong> <asp:Label ID="lblID" runat="server" /></p>
                <p><strong>Descripción:</strong> <asp:Label ID="lblDescripcion" runat="server" /></p>
                <p>Selecciona un elemento para ver sus detalles.</p>
            </div>
        </div>
    </form>
</body>
</html>



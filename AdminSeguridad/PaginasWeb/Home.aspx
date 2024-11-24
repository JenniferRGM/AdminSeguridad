<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home - AdminSeguridad</title>
    <link href="../style.css" rel="stylesheet" />
    <style>
        .menu-container {
            width: 300px;
            margin: 20px auto;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
            padding: 15px;
        }

        .menu-container h1 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        .menu-container ul {
            list-style: none;
            padding: 0;
        }

        .menu-container ul li {
            margin: 10px 0;
            text-align: center;
        }

        .menu-container ul li a {
            display: block;
            padding: 10px;
            background-color: #007BFF;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            transition: all 0.3s ease;
        }

        .menu-container ul li a:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menu-container">
            <h1>Bienvenido al Sistema de Administración</h1>
            <p>Seleccione una opción del menú:</p>

            <asp:Literal ID="menuList" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>

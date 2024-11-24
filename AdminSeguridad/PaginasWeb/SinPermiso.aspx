<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SinPermiso.aspx.cs" Inherits="AdminSeguridad.PaginasWeb.SinPermiso" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Acceso Denegado</title>
    <link rel="stylesheet" href="Estilos/style.css">
</head>
<body>
    <div class="sin-permiso-container">
        <div class="sin-permiso-content">
            <h1>Acceso Denegado</h1>
            <p>No tienes los permisos necesarios para acceder a esta página.</p>
            <a href="~/Login.aspx" class="btn-regresar">Volver al inicio</a>
        </div>
    </div>
</body>
</html>


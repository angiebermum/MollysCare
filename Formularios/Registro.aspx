<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="MollysCare.Formularios.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Registro de usuarios - Molly's</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: radial-gradient(circle at 20% 10%, #FFD6E7, transparent 60%),
                        radial-gradient(circle at 80% 20%, #D8F0FF, transparent 60%),
                        radial-gradient(circle at 20% 80%, #CFF6E3, transparent 60%),
                        radial-gradient(circle at 90% 85%, #FFE4C7, transparent 60%),
                        #E6E6FA;
            font-weight: 600;
        }

        .login-card {
            max-width: 450px;
            width: 100%;
            border-radius: 1.25rem;
            background-color: #ffffffcc;
            backdrop-filter: blur(6px);
            box-shadow: 0 10px 30px rgba(0,0,0,.09);
            padding: 2rem 2.5rem;
        }

        .brand-badge{
            background: linear-gradient(135deg, #ff9ac4, #a9e9d2);
            -webkit-background-clip: text;
            background-clip: text;
            color: transparent;
            font-weight:800;
            letter-spacing:.5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">
            <div class="text-center mb-3">
                <img src='<%= ResolveUrl("~/images/logo.png") %>' alt="Molly's Care" style="max-width:120px; height:auto;">
                <h1 class="h4 mt-2 mb-0 brand-badge">Molly's Care</h1>
                <p class="text-muted mb-3">Registro de usuarios</p>
            </div>

            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre completo</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtCorreo" class="form-label">Correo electrónico</label>
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtContrasena" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtConfirmar" class="form-label">Confirmar contraseña</label>
                <asp:TextBox ID="txtConfirmar" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="ddlRol" class="form-label">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Administrador" Value="ADMIN" />
                    <asp:ListItem Text="Cliente" Value="CLIENTE" />
                </asp:DropDownList>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-2 text-danger"></asp:Label>

            <div class="d-grid gap-2">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar usuario" CssClass="btn btn-primary"
                    OnClick="btnRegistrar_Click" />
                <a href="Menu.aspx" class="btn btn-outline-secondary">Volver al menú</a>
            </div>
        </div>
    </form>

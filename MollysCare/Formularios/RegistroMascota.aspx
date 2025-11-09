<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroMascota.aspx.cs" Inherits="TuProyecto.RegistroMascota" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Mascota</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(to bottom right, #ffe5ec, #e0f7fa);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .card {
            border-radius: 15px;
            background-color: #fff;
            box-shadow: 0px 4px 10px rgba(0,0,0,0.1);
        }
        .btn-pastel {
            background-color: #c8f7c5;
            border: none;
            color: #2e7d32;
            font-weight: bold;
        }
        .btn-pastel:hover {
            background-color: #a5d6a7;
        }
        .form-control, .form-select {
            border: 2px solid #ccc;
            border-radius: 8px;
        }
        .form-control:focus, .form-select:focus {
            border-color: #ffb6c1;
            box-shadow: 0 0 5px rgba(255,182,193,0.5);
        }
        .thumb {
            max-height: 120px;
            border-radius: 10px;
            margin-top: 10px;
        }

        .mensaje-centrado {
            text-align: center;
            font-weight: bold;
            font-size: 1.1rem;
        }
        
        .mensaje-exito {
            color: green;
        }
        
        .mensaje-error {
            color: red;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="container py-5">
            <div class="row justify-content-center">
                <div class="col-md-7 col-lg-6">
                    <div class="card p-4 p-md-5">
                        <h2 class="text-center mb-4">Registrar Mascota</h2>

                        <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="fw-bold d-block mb-3"></asp:Label>

                       
                        <div class="mb-3">
                            <asp:Label Text="Nombre:" AssociatedControlID="txtNombre" CssClass="form-label" runat="server" />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>

                        
                        <div class="mb-3">
                            <asp:Label Text="Fecha de nacimiento:" AssociatedControlID="txtFechaNacimiento" CssClass="form-label" runat="server" />
                            <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" />
                        </div>

                        
                        <div class="mb-3">
                            <asp:Label Text="Especie:" AssociatedControlID="ddlEspecie" CssClass="form-label" runat="server" />
                            <asp:DropDownList ID="ddlEspecie" runat="server" CssClass="form-select" />
                        </div>

                       
                        <div class="mb-3">
                            <asp:Label Text="Raza:" AssociatedControlID="ddlRaza" CssClass="form-label" runat="server" />
                            <asp:DropDownList ID="ddlRaza" runat="server" CssClass="form-select" />
                        </div>

                      
                        <div class="mb-3">
                            <asp:Label Text="Dueño:" AssociatedControlID="ddlDueno" CssClass="form-label" runat="server" />
                            <asp:DropDownList ID="ddlDueno" runat="server" CssClass="form-select" />
                        </div>

                    
                        <div class="mb-3">
                            <asp:Label Text="Sexo:" AssociatedControlID="ddlSexo" CssClass="form-label" runat="server" />
                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Seleccione..." Value="" />
                                <asp:ListItem Text="Macho" Value="M" />
                                <asp:ListItem Text="Hembra" Value="H" />
                            </asp:DropDownList>
                        </div>

                      
                        <div class="mb-3">
                            <asp:Label Text="Color:" AssociatedControlID="txtColor" CssClass="form-label" runat="server" />
                            <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" />
                        </div>

                      
                        <div class="mb-3">
                            <asp:Label Text="Observaciones médicas:" AssociatedControlID="txtObs" CssClass="form-label" runat="server" />
                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                        </div>

                    
                        <div class="mb-3">
                            <asp:Label Text="Foto de la mascota:" AssociatedControlID="fuFoto" CssClass="form-label" runat="server" />
                            <asp:FileUpload ID="fuFoto" runat="server" CssClass="form-control" />
                            <small class="text-muted">JPG/PNG, máx. 2 MB.</small>
                            <div class="mt-2">
                                <asp:Image ID="imgPreview" runat="server" CssClass="thumb d-none" />
                            </div>
                        </div>

                        <div class="d-flex gap-2">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-pastel btn-lg flex-fill"
                                OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnVolver" runat="server" Text="Regresar" CssClass="btn btn-pastel btn-lg flex-fill"
                                OnClick="btnVolver_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

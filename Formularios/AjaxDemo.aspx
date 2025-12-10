<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxDemo.aspx.cs" Inherits="MollysCare.Formularios.AjaxDemo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Demo AJAX – Molly's Care</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

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

        .ajax-card {
            max-width: 520px;
            width: 100%;
            border-radius: 1.25rem;
            background-color: #ffffffcc;
            backdrop-filter: blur(6px);
            box-shadow: 0 10px 30px rgba(0,0,0,.09);
            padding: 2rem 2.5rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ajax-card">
            <h1 class="h4 mb-2">Demo AJAX – Molly's Care</h1>
            <p class="text-muted mb-3">
                Ejemplo sencillo de JavaScript + AJAX: enviamos un nombre al servidor y mostramos la respuesta sin recargar la página.
            </p>

            <div class="mb-3">
                <label for="<%= txtNombre.ClientID %>" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-2">
                <button type="button" class="btn btn-primary btn-sm" onclick="consultarSaludo()">
                    Consultar saludo (AJAX)
                </button>
                <a href="Menu.aspx" class="btn btn-outline-secondary btn-sm">Volver al menú</a>
            </div>

            <asp:Label ID="lblResultado" runat="server" CssClass="text-success d-block mb-1"></asp:Label>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-0"></asp:Label>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
       
        function consultarSaludo() {
            var txt = document.getElementById('<%= txtNombre.ClientID %>');
            var lblOk = document.getElementById('<%= lblResultado.ClientID %>');
            var lblErr = document.getElementById('<%= lblError.ClientID %>');

            lblOk.innerText = '';
            lblErr.innerText = '';

            var nombre = txt.value.trim();
            if (!nombre) {
                lblErr.innerText = 'Por favor, escriba un nombre.';
                return;
            }

            var url = 'AjaxDemo.aspx?ajax=1&nombre=' + encodeURIComponent(nombre);

            fetch(url, { method: 'GET', cache: 'no-cache' })
                .then(function (resp) { return resp.text(); })
                .then(function (texto) {
                    lblErr.innerText = '';
                    lblOk.innerText = texto;
                })
                .catch(function () {
                    lblOk.innerText = '';
                    lblErr.innerText = 'La consulta no fue exitosa.';
                });
        }
    </script>
</body>
</html>

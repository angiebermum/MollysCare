<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ReporteVentas.aspx.cs"
    Inherits="MollysCare.Formularios.ReporteVentas" %>

<%
    // SOLO ADMIN: ajusta Rol / ADMIN si usas otro nombre
    if (Session["Rol"] == null || Session["Rol"].ToString().ToUpperInvariant() != "ADMIN")
    {
        Response.Redirect("Menu.aspx");
    }
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Informe de Ventas - Molly's Care</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <style>
        :root{
            --pastel-rose:#FFD6E7;
            --pastel-mint:#CFF6E3;
            --pastel-lav:#E6E6FA;
            --pastel-sky:#D8F0FF;
            --pastel-peach:#FFE4C7;
            --text:#333333;
            --card-bg:#ffffffcc;
        }
        body{
            background: radial-gradient(circle at 20% 10%, var(--pastel-rose), transparent 60%),
                        radial-gradient(circle at 80% 20%, var(--pastel-sky), transparent 60%),
                        radial-gradient(circle at 20% 80%, var(--pastel-mint), transparent 60%),
                        radial-gradient(circle at 90% 85%, var(--pastel-peach), transparent 60%),
                        var(--pastel-lav);
            color: var(--text);
        }
        .report-wrapper{ max-width: 1100px; }
        .report-card{
            background-color: var(--card-bg);
            backdrop-filter: blur(6px);
            border-radius: 1.25rem;
            border: 0;
            box-shadow: 0 10px 30px rgba(0,0,0,.08);
        }
        .section-title{
            font-size:1.2rem;
            font-weight:700;
            margin-top:1.5rem;
            margin-bottom:.6rem;
        }
        .chart-card{
            background-color:#fff;
            border-radius:1rem;
            padding:0.75rem 1rem;
            box-shadow:0 6px 15px rgba(0,0,0,.05);
            border:1px solid rgba(0,0,0,.04);
            margin-top:1rem;
            text-align:center;
        }
        .chart-title{
            font-size:.95rem;
            font-weight:600;
            margin-bottom:.35rem;
        }

        /* tamaño fijo de los gráficos */
        .chart-canvas{
            width: 380px;
            height: 180px;
            max-width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container py-4 report-wrapper">
        <div class="card report-card">
            <div class="card-body p-4 p-md-5">

                <h1 class="h4 mb-3">Informe de Ventas</h1>

                <asp:Label ID="lblError" runat="server"
                           CssClass="text-danger d-block mb-2"></asp:Label>

                <!-- Filtros -->
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="row g-2 align-items-end">
                            <div class="col-md-3">
                                <label class="form-label" for="ddlModo">Modo</label>
                                <asp:DropDownList ID="ddlModo" runat="server"
                                    CssClass="form-select">
                                    <asp:ListItem Text="Diario" Value="DIARIO" />
                                    <asp:ListItem Text="Semanal" Value="SEMANAL" />
                                    <asp:ListItem Text="Mensual" Value="MENSUAL" />
                                    <asp:ListItem Text="Anual" Value="ANUAL" />
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label class="form-label" for="txtDesde">Desde</label>
                                <asp:TextBox ID="txtDesde" runat="server"
                                             CssClass="form-control"
                                             TextMode="Date" />
                            </div>

                            <div class="col-md-3">
                                <label class="form-label" for="txtHasta">Hasta</label>
                                <asp:TextBox ID="txtHasta" runat="server"
                                             CssClass="form-control"
                                             TextMode="Date" />
                            </div>

                            <div class="col-md-3 d-grid">
                                <asp:Button ID="btnGenerar" runat="server"
                                    Text="Generar informe"
                                    CssClass="btn btn-primary"
                                    OnClick="btnGenerar_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- TABLAS -->
                <h2 class="section-title">Ventas por período</h2>
                <asp:GridView ID="gvVentasPeriodo" runat="server"
                    CssClass="table table-striped table-sm"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="EtiquetaPeriodo" HeaderText="Período" />
                        <asp:BoundField DataField="TotalVentas" HeaderText="Total ventas"
                            DataFormatString="₡{0:N2}" HtmlEncode="False" />
                        <asp:BoundField DataField="Ganancia" HeaderText="Ganancia"
                            DataFormatString="₡{0:N2}" HtmlEncode="False" />
                    </Columns>
                </asp:GridView>

                <h2 class="section-title">Productos más vendidos</h2>
                <asp:GridView ID="gvProductos" runat="server"
                    CssClass="table table-striped table-sm"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="CantidadVendida" HeaderText="Cantidad" />
                        <asp:BoundField DataField="MontoTotal" HeaderText="Monto total"
                            DataFormatString="₡{0:N2}" HtmlEncode="False" />
                    </Columns>
                </asp:GridView>

                <h2 class="section-title">Clientes frecuentes</h2>
                <asp:GridView ID="gvClientes" runat="server"
                    CssClass="table table-striped table-sm"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="NumeroPedidos" HeaderText="N° pedidos" />
                        <asp:BoundField DataField="TotalComprado" HeaderText="Total comprado"
                            DataFormatString="₡{0:N2}" HtmlEncode="False" />
                    </Columns>
                </asp:GridView>

                <!-- GRÁFICOS EN TARJETAS (tamaño fijo) -->
                <div class="row">
                    <div class="col-lg-6">
                        <div class="chart-card">
                            <p class="chart-title mb-1">Ventas y ganancias por período</p>
                            <canvas id="chartVentas" class="chart-canvas"></canvas>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="chart-card">
                            <p class="chart-title mb-1">Top productos (cantidad vendida)</p>
                            <canvas id="chartProductos" class="chart-canvas"></canvas>
                        </div>

                        <div class="chart-card mt-3">
                            <p class="chart-title mb-1">Clientes frecuentes (n° pedidos)</p>
                            <canvas id="chartClientes" class="chart-canvas"></canvas>
                        </div>
                    </div>
                </div>

                <!-- HiddenFields para enviar datos al JS -->
                <asp:HiddenField ID="hfVentasPeriodos" runat="server" />
                <asp:HiddenField ID="hfVentasMontos" runat="server" />
                <asp:HiddenField ID="hfGananciasMontos" runat="server" />

                <asp:HiddenField ID="hfProductosNombres" runat="server" />
                <asp:HiddenField ID="hfProductosCantidades" runat="server" />

                <asp:HiddenField ID="hfClientesNombres" runat="server" />
                <asp:HiddenField ID="hfClientesPedidos" runat="server" />

                <a href="Menu.aspx" class="btn btn-outline-secondary mt-4">Volver al menú</a>

            </div>
        </div>

        <!-- JS de gráficos: responsive = false para que respeten el tamaño fijo -->
        <script type="text/javascript">
            function splitValues(id) {
                var el = document.getElementById(id);
                if (!el || !el.value) return [];
                return el.value.split('|').filter(function (x) { return x !== ''; });
            }

            function splitNumbers(id) {
                return splitValues(id).map(function (x) {
                    return parseFloat(x.replace(',', '.')) || 0;
                });
            }

            function initCharts() {
                var labelsVentas = splitValues('<%= hfVentasPeriodos.ClientID %>');
                var dataVentas = splitNumbers('<%= hfVentasMontos.ClientID %>');
                var dataGanancias = splitNumbers('<%= hfGananciasMontos.ClientID %>');

                if (labelsVentas.length > 0 && document.getElementById('chartVentas')) {
                    var ctxVentas = document.getElementById('chartVentas').getContext('2d');
                    new Chart(ctxVentas, {
                        type: 'line',
                        data: {
                            labels: labelsVentas,
                            datasets: [
                                {
                                    label: 'Total ventas',
                                    data: dataVentas,
                                    borderColor: 'rgba(54, 162, 235, 1)',
                                    backgroundColor: 'rgba(54, 162, 235, 0.08)',
                                    borderWidth: 2,
                                    tension: 0.3
                                },
                                {
                                    label: 'Ganancia',
                                    data: dataGanancias,
                                    borderColor: 'rgba(75, 192, 192, 1)',
                                    backgroundColor: 'rgba(75, 192, 192, 0.08)',
                                    borderWidth: 2,
                                    tension: 0.3
                                }
                            ]
                        },
                        options: {
                            responsive: false,   // <-- clave
                            plugins: {
                                legend: {
                                    position: 'bottom',
                                    labels: {
                                        boxWidth: 10,
                                        font: { size: 10 }
                                    }
                                }
                            },
                            scales: {
                                x: {
                                    ticks: { font: { size: 10 } }
                                },
                                y: {
                                    ticks: {
                                        font: { size: 10 },
                                        callback: function (value) {
                                            return '₡' + value.toLocaleString('es-CR');
                                        }
                                    }
                                }
                            }
                        }
                    });
                }

                var labelsProd = splitValues('<%= hfProductosNombres.ClientID %>');
                var dataProd = splitNumbers('<%= hfProductosCantidades.ClientID %>');

                if (labelsProd.length > 0 && document.getElementById('chartProductos')) {
                    var ctxProd = document.getElementById('chartProductos').getContext('2d');
                    new Chart(ctxProd, {
                        type: 'bar',
                        data: {
                            labels: labelsProd,
                            datasets: [{
                                label: 'Cantidad vendida',
                                data: dataProd,
                                backgroundColor: 'rgba(153, 102, 255, 0.4)',
                                borderColor: 'rgba(153, 102, 255, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: false,   // <-- tamaño fijo
                            plugins: {
                                legend: {
                                    position: 'bottom',
                                    labels: { boxWidth: 10, font: { size: 10 } }
                                }
                            },
                            scales: {
                                x: { ticks: { font: { size: 10 } } },
                                y: { ticks: { font: { size: 10 } } }
                            }
                        }
                    });
                }

                var labelsCli = splitValues('<%= hfClientesNombres.ClientID %>');
                var dataCli = splitNumbers('<%= hfClientesPedidos.ClientID %>');

                if (labelsCli.length > 0 && document.getElementById('chartClientes')) {
                    var ctxCli = document.getElementById('chartClientes').getContext('2d');
                    new Chart(ctxCli, {
                        type: 'bar',
                        data: {
                            labels: labelsCli,
                            datasets: [{
                                label: 'N° pedidos',
                                data: dataCli,
                                backgroundColor: 'rgba(255, 159, 64, 0.4)',
                                borderColor: 'rgba(255, 159, 64, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: false,   // <-- tamaño fijo
                            plugins: {
                                legend: {
                                    position: 'bottom',
                                    labels: { boxWidth: 10, font: { size: 10 } }
                                }
                            },
                            scales: {
                                x: { ticks: { font: { size: 10 } } },
                                y: { ticks: { font: { size: 10 } } }
                            }
                        }
                    });
                }
            }

            initCharts();
        </script>

    </form>
</body>
</html>

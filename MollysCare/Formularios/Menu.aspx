<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="MollysCare.Formularios.Menu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
   
    <title>Molly's Care - Menú Principal</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

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
        html,body{
            height:100%;
        }
        body{
          
            background: radial-gradient(circle at 20% 10%, var(--pastel-rose), transparent 60%),
                        radial-gradient(circle at 80% 20%, var(--pastel-sky), transparent 60%),
                        radial-gradient(circle at 20% 80%, var(--pastel-mint), transparent 60%),
                        radial-gradient(circle at 90% 85%, var(--pastel-peach), transparent 60%),
                        var(--pastel-lav);
            color:var(--text);
            font-weight:700; 
        }
        .brand-badge{
            background: linear-gradient(135deg, #ff9ac4, #a9e9d2);
            -webkit-background-clip: text;
            background-clip: text;
            color: transparent;
            font-weight:800;
            letter-spacing:.5px;
        }
        .menu-wrapper{
            max-width: 950px;
        }
        .menu-card{
            background-color: var(--card-bg);
            backdrop-filter: blur(6px);
            border: 0;
            border-radius: 1.25rem;
            box-shadow: 0 10px 30px rgba(0,0,0,.08);
        }
        .menu-grid a.menu-link{
            text-decoration:none;
            display:block;
            border-radius: 1rem;
            padding: 18px 16px;
            transition: transform .15s ease, box-shadow .15s ease, background-color .15s ease;
            background-color:#ffffff;
            border:1px solid rgba(0,0,0,.05);
        }
        .menu-grid a.menu-link:hover{
            transform: translateY(-3px);
            box-shadow: 0 12px 22px rgba(0,0,0,.10);
        }
        .chip{
            font-size:.8rem;
            padding:.25rem .6rem;
            border-radius:999px;
            background: #0000000d;
        }
       
        .rose   { background-color: var(--pastel-rose);  }
        .mint   { background-color: var(--pastel-mint);  }
        .lav    { background-color: var(--pastel-lav);   }
        .sky    { background-color: var(--pastel-sky);   }
        .peach  { background-color: var(--pastel-peach); }

        .icon-wrap{
            width:48px;height:48px;
            display:grid;place-items:center;
            border-radius:12px;
            border:1px solid rgba(0,0,0,.06);
        }
        
        .menu-title {
             margin: 0;
             font-size: 1.2rem; 
             line-height: 1.2; 
             color: #000; 
             font-weight: bold; 
        }

        .menu-desc{
            margin:0;
            font-size:.9rem;
            color:#444;
            font-weight:600;
            opacity:.85;
        }

        .footer-note{
            font-size:.85rem;
            opacity:.8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-5 menu-wrapper">
            <div class="card menu-card">
                <div class="card-body p-4 p-md-5">

                    <div class="d-flex align-items-center gap-3 mb-4">
                        <img src='<%= ResolveUrl("~/images/logo.png") %>' 
                            alt="Molly's Care" 
                            style="max-width: 150px; height: auto;">
                        <div>
                            <h1 class="h3 mb-1 brand-badge" style="font-size: 2rem;">Molly's Care</h1>
                            <p class="mb-0">Menú principal — ¡Todo en un solo lugar!</p>

                        </div>

                    </div>

                    <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 g-3 menu-grid">

                        <div class="col">
                            <a class="menu-link" href="RegistroDueno.aspx" aria-label="Registrar dueño">
                                <div class="d-flex align-items-center gap-3">
                                    <div class="icon-wrap mint"><i class="bi bi-person-plus"></i></div>
                                    <div class="flex-grow-1">
                                        <p class="menu-title">Registrar dueño</p>
                                        <p class="menu-desc">Datos de contacto</p>
                                    </div>
                                    <i class="bi bi-chevron-right"></i>
                                </div>
                            </a>
                        </div>

                         <div class="col">
                             <a class="menu-link" href="RegistroMascota.aspx" aria-label="Registrar mascota">
                                 <div class="d-flex align-items-center gap-3">
                                     <div class="icon-wrap rose"><i class="bi bi-heart-pulse"></i></div>
                                     <div class="flex-grow-1">
                                         <p class="menu-title">Registrar mascota</p>
                                         <p class="menu-desc">Datos de las mascotas</p>
                                     </div>
                                     <i class="bi bi-chevron-right"></i>
                                 </div>
                             </a>
                         </div>

                         <div class="col">
                             <a class="menu-link" href="RegistroEspecieRaza.aspx" aria-label="Registrar especie y raza">
                                 <div class="d-flex align-items-center gap-3">
                                     <div class="icon-wrap sky"><i class="bi bi-collection"></i></div>
                                     <div class="flex-grow-1">
                                         <p class="menu-title">Especie y raza</p>
                                         <p class="menu-desc">Datos de especie y raza</p>
                                     </div>
                                     <i class="bi bi-chevron-right"></i>
                                 </div>
                             </a>
                         </div>

                        <div class="col">
                            <a class="menu-link" href="RegistroTratamiento.aspx" aria-label="Registrar tratamiento">
                                <div class="d-flex align-items-center gap-3">
                                    <div class="icon-wrap lav"><i class="bi bi-capsule"></i></div>
                                    <div class="flex-grow-1">
                                        <p class="menu-title">Registrar tratamiento</p>
                                        <p class="menu-desc">Control de tratamientos y medicamentos</p>
                                    </div>
                                    <i class="bi bi-chevron-right"></i>
                                </div>
                            </a>
                        </div>

                       
                        <div class="col">
                            <a class="menu-link" href="ListaMascotas.aspx" aria-label="Lista de mascotas">
                                <div class="d-flex align-items-center gap-3">
                                    <div class="icon-wrap peach"><i class="bi bi-list-ul"></i></div>
                                    <div class="flex-grow-1">
                                        <p class="menu-title">Lista de mascotas</p>
                                        <p class="menu-desc">Búsqueda y filtros</p>
                                    </div>
                                    <i class="bi bi-chevron-right"></i>
                                </div>
                            </a>
                        </div>

                        <div class="col">
                            <a class="menu-link" href="ReporteMascotas.aspx" aria-label="Reporte de mascotas">
                                <div class="d-flex align-items-center gap-3">
                                    <div class="icon-wrap mint"><i class="bi bi-filetype-pdf"></i></div>
                                    <div class="flex-grow-1">
                                        <p class="menu-title">Reporte de mascotas</p>
                                        <p class="menu-desc">PDF y excel</p>
                                    </div>
                                    <i class="bi bi-chevron-right"></i>
                                </div>
                            </a>
                        </div>

                    </div>

                    <hr class="my-4" />
                    <div class="d-flex flex-column flex-sm-row align-items-start align-items-sm-center justify-content-between gap-2">
                        <div class="footer-note d-flex align-items-center gap-2">
                            <i class="bi bi-telephone-fill" style="font-size: 1.2rem;"></i>
                            <span>2416-8578</span>
                        </div>
                        <div class="footer-note d-flex align-items-center gap-2">
                            <i class="bi bi-envelope-fill" style="font-size: 1.2rem;"></i>
                            <span>mollyscare@gmail.com</span>
                        </div>
                        <div class="footer-note d-flex align-items-center gap-2">
                            <i class="bi bi-instagram" style="font-size: 1.2rem;"></i>
                            <span>mollyscare</span>
                        </div>

                        <div class="d-flex gap-2">
                        </div>
                    </div>
                </div>
        </main>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

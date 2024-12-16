
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
</head>
<body>
    <h1>Matrícula:  @ViewBag.matricula (@ViewBag.Resultado )</h1>
    <ul class="listaResultados">
        <li><span class="Item">Documento:</span>  <span class="Result"><a href=@ViewBag.linkDoc>@ViewBag.tipodoc - @ViewBag.codigodoc</a></span></li>
        <li><span class="Item">Articulo:</span>  <span class="Result">@ViewBag.coart</span> </li>
        <li><span class="Item">Descripcion:</span>  <span class="Result">@ViewBag.descripcion</span> </li>
        <li><span class="Item">&nbsp;</span>  <span class="Result">@ViewBag.ref_linea</span></p> </li>
        <li><span class="Item">fecha Pintura:</span><span class="Result">@ViewBag.fecha_muestra</span>
        <li><span class="Item">Fecha Embalaje:</span><span class="Result">@ViewBag.fecha_emb</span>
        <li><span class="Item">Introducido:</span><span class="Result">@ViewBag.introducido</span>
        <li><span class="Item">Última Modificación:</span><span class="Result">@ViewBag.ultima_modificacion</span>
        <li><span class="Item">Agencia:</span><span class="Result">@ViewBag.agencia</span>
        <li><span class="Item">Pintor:</span><span class="Result">@ViewBag.pintor</span>
        <li><span class="Item">Cliente:</span><span class="Result"><a href= @ViewBag.linkCliente>@ViewBag.cliente</a></span>
    </ul>
</body>
</html>


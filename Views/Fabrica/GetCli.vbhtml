@Code
    ViewData("Title") = ViewBag.Title
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
</head>
<body>

    <h1>Cliente:  @ViewBag.CodCliente </h1>

</body>
</html>




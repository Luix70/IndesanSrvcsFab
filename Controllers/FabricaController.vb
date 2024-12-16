Imports System.Data.OleDb
Imports System.Web.Mvc

Namespace Controllers
    Public Class FabricaController
        Inherits Controller

        Public Shared Function Nz(ByVal Value As Object, Optional ByVal valueByDefault As Object = "") As Object
            If Value Is Nothing OrElse IsDBNull(Value) Then
                Return valueByDefault
            Else
                Return Value
            End If

        End Function
        Shared strConexion As String = Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings("conexionDatos"))

        <HttpGet>
        Public Function GetMat(ByVal id As String) As ActionResult
            ' Lógica para procesar el parámetro id.
            ' Podrías hacer alguna consulta o lógica con el id.
            ' Pasar el id a la vista
            ' Ejemplo: pasando el id como modelo a la vista.

            ViewBag.Title = "Identificacion de Artículos por Matrícula"


            ViewBag.matricula = id


            GetData(id)

            Return View()
        End Function

        Public Function GetCli(ByVal id As String) As ActionResult
            ' Lógica para procesar el parámetro id.
            ' Podrías hacer alguna consulta o lógica con el id.
            ' Pasar el id a la vista
            ' Ejemplo: pasando el id como modelo a la vista.

            ViewBag.Title = "Datos del cliente"


            ViewBag.CodCliente = id




            Return View()
        End Function


        Public Function GetDoc(ByVal id As String) As ActionResult
            ' Lógica para procesar el parámetro id.
            ' Podrías hacer alguna consulta o lógica con el id.
            ' Pasar el id a la vista
            ' Ejemplo: pasando el id como modelo a la vista.

            ViewBag.Title = "Datos del Documento"


            ViewBag.CodDocumento = id




            Return View()
        End Function



        Public Function GetData(id As String) As String

            Dim strCadenaConsulta As String

            Dim dt As New DataTable
            strCadenaConsulta = "SELECT documentos.tipodoc, documentos.codigodoc, documentos_desglose.matricula, documentos_desglose.fechaalbaran, documentos_desglose.fecha_emb, documentos.agencia, documentos_desglose.pintor, documentos_desglose.fecha_muestra, documentos.introducido, documentos.ultima_modificacion, documentos_desglose.coart, documentos_desglose.ref_linea, documentos_desglose.descripcion, documentos.cliente, CLIENTES_RST.rzs, CLIENTES_RST.nombrecomercial, CLIENTES_RST.poblacion, agencias.Nombre " _
                                   & "FROM agencias INNER JOIN (CLIENTES_RST INNER JOIN (documentos INNER JOIN documentos_desglose ON (documentos.codigodoc = documentos_desglose.codigodoc) AND (documentos.tipodoc = documentos_desglose.tipodoc)) ON CLIENTES_RST.codigo = documentos.cliente) ON agencias.codagencia = documentos.agencia " _
                                   & "WHERE (((documentos_desglose.matricula)=" & id & "));"


            Try

                Dim Cons As New OleDb.OleDbConnection
                Cons.ConnectionString = strConexion
                Cons.Open()


                Using dad As New OleDb.OleDbDataAdapter(strCadenaConsulta, Cons)

                    Try
                        dad.Fill(dt)



                        If dt.Rows.Count <> 1 Then
                            ViewBag.Resultado = "Varios Registros o Ningun Registro"
                        Else

                            With dt.Rows(0)

                                ViewBag.Resultado = "OK"
                                ViewBag.tipodoc = .Item("tipodoc")
                                ViewBag.codigodoc = .Item("codigodoc")
                                ViewBag.linkDoc = "../GetDoc/" & .Item("tipodoc") & "-" & .Item("codigodoc")
                                ViewBag.coart = .Item("coart")
                                ViewBag.ref_linea = .Item("ref_linea")
                                ViewBag.descripcion = .Item("descripcion")
                                ViewBag.fecha_muestra = Nz(.Item("fecha_muestra"))
                                ViewBag.fecha_emb = Nz(.Item("fecha_emb"))
                                ViewBag.introducido = Nz(.Item("introducido"))
                                ViewBag.ultima_modificacion = Nz(.Item("ultima_modificacion"))
                                ViewBag.agencia = Nz(.Item("agencia")) & "-" & Nz(.Item("nombre"))
                                ViewBag.pintor = Nz(.Item("pintor"))
                                ViewBag.cliente = Nz(.Item("cliente")) & " - " & Nz(.Item("nombrecomercial"), .Item("rzs")) & " (" & .Item("poblacion") & ")"
                                ViewBag.linkCliente = "../GetCli/" & Nz(.Item("cliente"))

                            End With
                        End If

                    Catch ex As Exception
                        ''MsgBox(ex.Message)
                        ViewBag.Resultado = "Consulta Falló"
                    End Try


                End Using
                Cons.Close()
                Cons = Nothing
                'msg.Add("registrado", Date.Now.ToShortDateString & " " & Date.Now.ToLongTimeString)


            Catch ex As Exception
                ViewBag.Resultado = "Conexion falló"

            End Try

            Return "ok"

        End Function

    End Class
End Namespace
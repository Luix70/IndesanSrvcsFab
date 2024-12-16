Imports System.ComponentModel.DataAnnotations ' Importante para la validación

Namespace IndesanSrvcsFab.Models ' Asegúrate de que coincida con tu namespace
    Public Class ModeloLogin
        <Required(ErrorMessage:="El usuario es requerido.")> ' Requiere que el campo no esté vacío
        <Display(Name:="Usuario")>
        Public Property Usuario As String

        <Required(ErrorMessage:="La contraseña es requerida.")> ' Requiere que el campo no esté vacío
        <DataType(DataType.Password)> ' Indica que es una contraseña (para el renderizado en vistas)
        <Display(Name:="Contraseña")>
        Public Property Password As String
    End Class
End Namespace

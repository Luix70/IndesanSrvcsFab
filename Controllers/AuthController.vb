Imports System.Web.Http
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims
Imports System.Text
Imports System.Configuration
Imports IndesanSrvcsFab.IndesanSrvcsFab.Models

Namespace IndesanSrvcsFab.Controllers
    <AllowAnonymous> ' Permite el acceso a esta acción sin autenticación
    <RoutePrefix("api/auth")> ' Prefijo de ruta para las acciones de autenticación
    Public Class AuthController
        Inherits ApiController

        <HttpPost>
        <Route("login")> ' Ruta: api/auth/login
        Public Function Login(modeloLogin As ModeloLogin) As IHttpActionResult
            If modeloLogin Is Nothing Then
                Return BadRequest("Modelo de login no valido")
            End If
            If ModelState.IsValid Then
                ' 1. Validar las credenciales del usuario (¡¡¡NUNCA hardcodear credenciales en producción!!!)
                If String.IsNullOrEmpty(modeloLogin.Usuario) Or String.IsNullOrEmpty(modeloLogin.Password) Then
                    Return BadRequest("Usuario o contraseña vacios")
                End If
                If modeloLogin.Usuario = "usuarioEjemplo" AndAlso modeloLogin.Password = "passwordEjemplo" Then ' ¡¡¡NUNCA HAGAS ESTO EN PRODUCCIÓN!!!
                    ' 2. Generar el token JWT
                    Dim token = GenerarTokenJwt(modeloLogin.Usuario)

                    ' 3. Devolver el token en la respuesta
                    Return Ok(New With {.token = token})
                Else
                    Return Unauthorized() ' Credenciales inválidas
                End If
            Else
                Return BadRequest(ModelState) ' Modelo inválido
            End If
        End Function


        ' Método para generar el token JWT
        Private Function GenerarTokenJwt(username As String) As String
            ' Crear la lista de claims
            Dim claims As New List(Of Claim) From {
                New Claim(ClaimTypes.Name, username)
                ' Puedes agregar otros claims si es necesario
            }

            ' Crear la clave de seguridad a partir de la configuración
            Dim securityKey As New SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings("Jwt:Key")))

            ' Crear las credenciales de firma
            Dim credentials As New SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)

            ' Crear el token usando JwtSecurityTokenHandler
            Dim tokenHandler As New JwtSecurityTokenHandler()

            ' Crear el descriptor del token
            Dim tokenDescriptor As New JwtSecurityTokenDescriptor() With {
                .Subject = New ClaimsIdentity(claims),
                .Expires = Date.Now.AddHours(1), ' Expira en 1 hora (ajusta según necesidad)
                .Issuer = ConfigurationManager.AppSettings("Jwt:Issuer"),
                .Audience = ConfigurationManager.AppSettings("Jwt:Audience"),
                .SigningCredentials = credentials
            }

            ' Crear el token con el descriptor
            Dim token As SecurityToken = tokenHandler.CreateToken(tokenDescriptor)

            ' Devolver el token como una cadena
            Return tokenHandler.WriteToken(token)
        End Function
    End Class
End Namespace
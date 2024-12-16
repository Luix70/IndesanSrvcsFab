
Imports Owin
Imports Microsoft.Owin.Security.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports Microsoft.Owin

<Assembly: OwinStartup(GetType(Startup))>

Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        ' Crear el objeto TokenValidationParameters
        Dim tokenValidationParameters As New TokenValidationParameters()
        tokenValidationParameters.ValidateIssuer = True
        tokenValidationParameters.ValidateAudience = True
        tokenValidationParameters.ValidateLifetime = True
        tokenValidationParameters.ValidateIssuerSigningKey = True
        tokenValidationParameters.ValidIssuer = ConfigurationManager.AppSettings("Jwt:Issuer")
        tokenValidationParameters.ValidAudience = ConfigurationManager.AppSettings("Jwt:Audience")
        tokenValidationParameters.IssuerSigningKey = New SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings("Jwt:Key")))

        ' Crear el objeto JwtBearerAuthenticationOptions y asignar TokenValidationParameters
        Dim options As New JwtBearerAuthenticationOptions()
        options.TokenValidationParameters = tokenValidationParameters

        ' Configurar la autenticación JWT
        app.UseJwtBearerAuthentication(options)

        ' Otras configuraciones de OWIN
    End Sub
End Class
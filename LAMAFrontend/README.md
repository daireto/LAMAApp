# LAMA Frontend - Configuración

## Resumen de la Integración

Este proyecto frontend Blazor WebAssembly se integra con tu API backend de LAMA usando autenticación de Microsoft Entra ID.

## Configuración Requerida

### 1. Aplicaciones en Microsoft Entra ID

**IMPORTANTE**: Necesitas **DOS aplicaciones separadas** en Entra ID:

#### Aplicación Frontend (Blazor WASM) - Ya creada
- **Client ID**: `694951b6-d64f-4a7a-bdb7-80394e00322a`
- **Authority**: `https://lamamedellin.ciamlogin.com/lamamedellin.onmicrosoft.com`
- **Tipo**: Aplicación pública (SPA/Public Client)
- **Redirect URIs**: 
  - `https://localhost:7XXX/authentication/login-callback`
  - `https://tu-dominio.azurewebsites.net/authentication/login-callback`

#### Aplicación Backend (Web API) - Ya existe
- **Client ID**: El que ya tienes configurado en tu API
- **Expose an API**: Debe tener un scope como `api://TU-API-CLIENT-ID/access_as_user`
- **Permissions**: La app frontend debe tener permiso para acceder a este scope

### 2. Configurar API Permissions en Entra ID

En la aplicación Frontend (Blazor WASM), ve a:
1. **API permissions**
2. **Add a permission** ? **My APIs**
3. Selecciona tu aplicación Backend
4. Marca el scope `access_as_user`
5. **Grant admin consent** (si es necesario)

### 3. Actualizar appsettings.json

Edita `LAMAFrontend/wwwroot/appsettings.json` con tus valores reales:

```json
{
  "AzureAd": {
    "ClientId": "694951b6-d64f-4a7a-bdb7-80394e00322a",
    "Authority": "https://lamamedellin.ciamlogin.com/lamamedellin.onmicrosoft.com",
    "ValidateAuthority": true
  },
  "Api": {
    "BaseUrl": "https://TU-API-BACKEND.azurewebsites.net",
    "Scopes": [
      "api://TU-API-CLIENT-ID/access_as_user"
    ]
  }
}
```

**Reemplaza**:
- `TU-API-BACKEND.azurewebsites.net` ? URL de tu API backend
- `TU-API-CLIENT-ID` ? Client ID de tu aplicación Backend en Entra ID

### 4. Instalar Paquetes NuGet

Ejecuta estos comandos en la terminal:

```powershell
dotnet add LAMAFrontend/LAMAFrontend.csproj package Microsoft.AspNetCore.Components.WebAssembly.Authentication --version 8.0.20
dotnet add LAMAFrontend/LAMAFrontend.csproj package Microsoft.Extensions.Http --version 8.0.0
dotnet restore LAMAFrontend/LAMAFrontend.csproj
```

### 5. Estructura del Proyecto

```
LAMAFrontend/
??? Models/                      # DTOs que coinciden con el backend
?   ??? MiembroDto.cs
?   ??? CreateMiembroDto.cs
?   ??? UpdateMiembroDto.cs
??? Services/                    # Servicios para llamar a la API
?   ??? IMiembroService.cs
?   ??? MiembroService.cs
??? Pages/                       # Componentes Razor
?   ??? Miembros.razor          # Lista de miembros
?   ??? CrearMiembro.razor      # Formulario de creación
?   ??? EditarMiembro.razor     # Formulario de edición
?   ??? DetalleMiembro.razor    # Vista de detalle
?   ??? Authentication.razor    # Página de autenticación
??? wwwroot/
    ??? appsettings.json        # Configuración
```

## Funcionalidades Implementadas

### Gestión de Miembros (CRUD Completo)

1. **Lista de Miembros** (`/miembros`)
   - Ver todos los miembros
   - Filtrar por Rango y Estatus
   - Acciones: Ver detalle, Editar, Eliminar

2. **Crear Miembro** (`/miembros/crear`)
   - Formulario completo con validación
   - Información personal, del club y de la moto

3. **Editar Miembro** (`/miembros/editar/{id}`)
   - Carga datos existentes
   - Actualiza información

4. **Ver Detalle** (`/miembros/detalle/{id}`)
   - Vista completa de información del miembro

### Autenticación

- Integración con Microsoft Entra ID
- Todas las páginas de miembros requieren autenticación
- Tokens de acceso se envían automáticamente a la API
- Redirección automática al login si el token expira

## Servicios

### IMiembroService / MiembroService

Encapsula todas las llamadas a la API:

```csharp
Task<IEnumerable<MiembroDto>> GetAllMiembrosAsync();
Task<MiembroDto?> GetMiembroByIdAsync(int id);
Task<IEnumerable<MiembroDto>> GetMiembrosByRangoAsync(string rango);
Task<IEnumerable<MiembroDto>> GetMiembrosByEstatusAsync(string estatus);
Task<MiembroDto?> CreateMiembroAsync(CreateMiembroDto miembro);
Task<MiembroDto?> UpdateMiembroAsync(UpdateMiembroDto miembro);
Task<bool> DeleteMiembroAsync(int id);
```

## Configuración del Backend (API)

Asegúrate de que tu API tenga CORS configurado para permitir el frontend:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWASM",
        policy =>
        {
            policy.WithOrigins("https://localhost:7XXX", "https://tu-frontend.azurewebsites.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

app.UseCors("AllowBlazorWASM");
```

## Ejecutar el Proyecto

```powershell
cd LAMAFrontend
dotnet run
```

O desde Visual Studio: F5

## Despliegue a Azure

### Frontend (Blazor WASM)
- Puede desplegarse en Azure Static Web Apps o Azure App Service
- Actualiza los Redirect URIs en Entra ID con la URL de producción

### Configuración de Producción
- Crea un `appsettings.Production.json` con valores de producción
- O usa Azure App Configuration / Key Vault para secretos

## Troubleshooting

### Error: "The type 'IRemoteAuthenticationBuilder<,>' is defined in an assembly that is not referenced"
**Solución**: Instala el paquete `Microsoft.AspNetCore.Components.WebAssembly.Authentication`

### Error: "AddHttpClient" not found
**Solución**: Instala el paquete `Microsoft.Extensions.Http`

### Error 401 al llamar a la API
**Soluciones**:
1. Verifica que el scope en `appsettings.json` sea correcto
2. Asegúrate de que la app frontend tenga permisos en Entra ID
3. Verifica que el token se esté enviando (inspecciona en DevTools ? Network)
4. Verifica la configuración de autenticación en la API

### Error de CORS
**Solución**: Configura CORS en la API para permitir el origen del frontend

## Próximos Pasos

1. Actualizar `appsettings.json` con tus valores reales
2. Instalar los paquetes NuGet necesarios
3. Verificar la configuración de Entra ID
4. Probar la autenticación
5. Probar el CRUD de miembros

## Notas Importantes

- **Seguridad**: Los tokens se manejan automáticamente por MSAL
- **Permisos**: Las políticas del backend (`AllMembers`, `RocketsOrAbove`, `FullColorOnly`) se aplican en el servidor
- **Validación**: Agrega `DataAnnotations` a los DTOs si necesitas validación del lado del cliente

# LAMAApp - Sistema de Gestión de Miembros

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Blazor WebAssembly](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor)
![Azure AD](https://img.shields.io/badge/Auth-Azure%20AD-0078D4?logo=microsoft-azure)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoft-sql-server)

Sistema de gestión integral para el capítulo **L.A.M.A. Medellín**, diseñado para centralizar y administrar de forma segura la información de los miembros del club.

---

## Tabla de Contenidos

- [Descripción del Proyecto](#-descripción-del-proyecto)
- [Problema y Solución](#-problema-y-solución)
- [Características Principales](#-características-principales)
- [Tecnologías Utilizadas](#?-tecnologías-utilizadas)
- [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
- [Requisitos Previos](#-requisitos-previos)
- [Configuración del Proyecto](#?-configuración-del-proyecto)
- [Ejecución del Proyecto](#-ejecución-del-proyecto)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Roles y Permisos](#-roles-y-permisos)
- [Configuración de Azure AD](#-configuración-de-azure-ad)
- [API Endpoints](#-api-endpoints)
- [Contribuir](#-contribuir)
- [Licencia](#-licencia)

---

## Descripción del Proyecto

LAMAApp es una aplicación web moderna que permite al capítulo L.A.M.A. Medellín gestionar eficientemente la información de sus miembros, incluyendo datos personales, información del club, y detalles de las motocicletas.

### Usuarios del Sistema

- **Administradores**: Miembros con permisos completos para crear, editar y eliminar información
- **Usuarios Standard**: Miembros con acceso de solo lectura para consultar información

---

## Problema y Solución

### Problema
- El capítulo L.A.M.A. Medellín necesitaba una forma eficiente y segura de gestionar la información de sus miembros
- La información estaba dispersa y era difícil de acceder, dificultando la comunicación y toma de decisiones
- No existía un sistema centralizado para almacenar datos importantes

### Solución
- **Aplicación web centralizada** que permite almacenar, gestionar y acceder a la información de forma segura
- **Autenticación robusta** mediante Azure AD (Microsoft Entra ID)
- **Interfaz intuitiva** desarrollada con Blazor WebAssembly
- **API REST segura** con autorización basada en roles
- **Base de datos SQL Server** para almacenamiento confiable

### Beneficios
? Mejora la eficiencia y productividad del capítulo  
? Centraliza la información y facilita el acceso  
? Mejora la comunicación entre miembros  
? Fortalece la seguridad de la información  
? Permite búsqueda y filtrado avanzado  
? Genera informes sobre los miembros  

---

## Características Principales

### Gestión de Miembros
- **CRUD completo** de información de miembros
- **Búsqueda y filtrado** por rango, estatus, ciudad
- **Vista detallada** de cada miembro
- **Información personal**: Nombre, cédula, fecha de nacimiento, contacto
- **Información de motocicleta**: Marca, modelo, placa, cilindraje
- **Datos de emergencia**: Tipo de sangre, EPS, contacto de emergencia

### Seguridad
- **Autenticación Azure AD** (Microsoft Entra ID)
- **Autorización basada en roles** (Admin/Standard)
- **Tokens JWT** para comunicación segura
- **CORS configurado** para prevenir accesos no autorizados

### Interfaz de Usuario
- **Diseño moderno** con Bootstrap 5
- **Responsive** - funciona en móviles y tablets
- **Carga rápida** con Blazor WebAssembly
- **Notificaciones** de éxito y error
- **Badges de estado** visual para rangos y estatus

---

## Tecnologías Utilizadas

### Frontend
- **Blazor WebAssembly** - Framework de aplicación SPA
- **Bootstrap 5** - Framework CSS
- **Bootstrap Icons** - Iconografía

### Backend
- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core** - ORM
- **Microsoft.Identity.Web** - Autenticación Azure AD

### Base de Datos
- **SQL Server** - Base de datos relacional
- **Azure SQL Database** - Hosting en la nube

### Autenticación
- **Azure AD (Microsoft Entra ID)** - Proveedor de identidad
- **JWT Bearer Tokens** - Autenticación de API

---

## Arquitectura del Proyecto

El proyecto sigue una **arquitectura limpia (Clean Architecture)** con separación de responsabilidades:

```
LAMAApp/ LAMAApp.Domain/          # Entidades y reglas de negocio LAMAApp.Application/     # Lógica de aplicación y DTOs LAMAApp.Infrastructure/  # Acceso a datos y servicios externos LAMAApp/                 # API REST (Backend) LAMAFrontend/            # Blazor WebAssembly (Frontend)
```

### Flujo de Datos
```
Usuario ? Blazor WebAssembly ? API REST ? Application Layer ? Infrastructure ? Database
         ?  Azure AD Token  ?  JWT Auth  ?  Entity Framework  ?
```

---

## Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/sql-server) (Local o Azure)
- [Azure AD Tenant](https://azure.microsoft.com/services/active-directory/) configurado
- [Git](https://git-scm.com/)

---

## Configuración del Proyecto

### 1. Clonar el Repositorio

```bash
git clone https://github.com/daireto/LAMAApp.git
cd LAMAApp
```

### 2. Configurar la Base de Datos

#### Actualizar la Cadena de Conexión

Edita el archivo `LAMAApp/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=LAMAAppDB;User ID=TU_USUARIO;Password=TU_PASSWORD;..."
  }
}
```

#### Aplicar Migraciones

```bash
cd LAMAApp.Infrastructure
dotnet ef database update --startup-project ../LAMAApp
```

### 3. Configurar Azure AD

#### Backend API (`LAMAApp/appsettings.json`)

```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "tu-dominio.onmicrosoft.com",
    "TenantId": "TU-TENANT-ID",
    "ClientId": "TU-API-CLIENT-ID",
    "Audience": "api://TU-API-CLIENT-ID",
    "Scopes": "API.Access"
  }
}
```

#### Frontend (`LAMAFrontend/wwwroot/appsettings.json`)

```json
{
  "AzureAd": {
    "Authority": "https://login.microsoftonline.com/TU-TENANT-ID",
    "ClientId": "TU-FRONTEND-CLIENT-ID",
    "ValidateAuthority": false
  },
  "Api": {
    "BaseUrl": "https://localhost:7199",
    "Scopes": [
      "api://TU-API-CLIENT-ID/API.Access"
    ]
  }
}
```

### 4. Restaurar Paquetes NuGet

```bash
dotnet restore
```

---

## Ejecución del Proyecto

### Opción 1: Visual Studio 2022

1. Abre la solución `LAMAApp.sln`
2. Configura **múltiples proyectos de inicio**:
   - Click derecho en la solución ? **Propiedades**
   - **Proyectos de inicio múltiples**
   - Selecciona **LAMAApp** y **LAMAFrontend** como "Iniciar"
3. Presiona `F5` para iniciar en modo debug

### Opción 2: Línea de Comandos

#### Terminal 1 - Backend API
```bash
cd LAMAApp
dotnet run
```

#### Terminal 2 - Frontend
```bash
cd LAMAFrontend
dotnet run
```

### URLs de Acceso

- **Frontend**: https://localhost:7097
- **Backend API**: https://localhost:7199
- **Swagger**: https://localhost:7199/swagger

---

## Estructura del Proyecto

```
LAMAApp/
? LAMAApp.Domain/                  # Capa de Dominio
?   Entities/
?   ?   Miembro.cs              # Entidad principal
?   Repositories/
?       IMiembroRepository.cs    # Interfaz del repositorio
? LAMAApp.Application/             # Capa de Aplicación
?   DTOs/
?   ?   MiembroDto.cs           # DTO de lectura
?   ?   CreateMiembroDto.cs     # DTO de creación
?   ?   UpdateMiembroDto.cs     # DTO de actualización
?   Services/
?       IMiembroService.cs      # Interfaz del servicio
?       MiembroService.cs       # Lógica de negocio
? LAMAApp.Infrastructure/          # Capa de Infraestructura
?   Data/
?   ?   LAMAAppDbContext.cs     # Contexto de EF Core
?   Repositories/
?   ?   MiembroRepository.cs    # Implementación del repositorio
?   Migrations/                  # Migraciones de base de datos
? LAMAApp/                         # API REST (Backend)
?   Controllers/
?   ?   MiembrosController.cs   # Controlador de API
?   Program.cs                   # Configuración de la aplicación
?   appsettings.json            # Configuración (Azure AD, DB)
? LAMAFrontend/                    # Blazor WebAssembly (Frontend)
   Pages/
    ?   Miembros.razor          # Lista de miembros
    ?   DetalleMiembro.razor    # Detalle de miembro
    ?   CrearMiembro.razor      # Formulario de creación
    ?   EditarMiembro.razor     # Formulario de edición
   Services/
    ?   IMiembroService.cs      # Interfaz del servicio
    ?   MiembroService.cs       # Servicio HTTP
   Models/
    ?   MiembroDto.cs           # Modelos del frontend
   Program.cs                   # Configuración de Blazor WASM
```

---

## Roles y Permisos

El sistema implementa **autorización basada en roles**:

### Rol: Admin
- Ver lista de miembros
- Ver detalles de miembros
- Filtrar por rango/estatus
- **Crear** nuevos miembros
- **Editar** miembros existentes
- **Eliminar** miembros

### Rol: Standard
- Ver lista de miembros
- Ver detalles de miembros
- Filtrar por rango/estatus
- Crear nuevos miembros
- Editar miembros existentes
- Eliminar miembros

---

## Configuración de Azure AD

### 1. Crear App Registration para la API

1. Ve a **Azure Portal** ? **Microsoft Entra ID** ? **App registrations**
2. Click en **New registration**
3. Nombre: `LAMAApp API`
4. Supported account types: **Single tenant**
5. Click **Register**

### 2. Configurar App Roles

En la aplicación de API:
1. Ve a **App roles** ? **Create app role**
2. Crea los siguientes roles:

   **Admin Role:**
   - Display name: `Admin`
   - Value: `Admin`
   - Description: `Administrator with full access`
   - Allowed member types: `Users/Groups`

   **Standard Role:**
   - Display name: `Standard`
   - Value: `Standard`
   - Description: `Standard user with read-only access`
   - Allowed member types: `Users/Groups`

### 3. Exponer API

1. Ve a **Expose an API**
2. Click **Add a scope**
3. Scope name: `API.Access`
4. Who can consent: `Admins and users`
5. Display name: `Access LAMAApp API`
6. Click **Add scope**

### 4. Crear App Registration para el Frontend

1. **New registration**
2. Nombre: `LAMAApp Frontend`
3. Redirect URI: `Single-page application (SPA)` ? `https://localhost:7097/authentication/login-callback`
4. Click **Register**

### 5. Configurar API Permissions en el Frontend

1. Ve a **API permissions** ? **Add a permission**
2. **My APIs** ? Selecciona `LAMAApp API`
3. Selecciona el scope `API.Access`
4. Click **Add permissions**
5. Click **Grant admin consent**

### 6. Asignar Usuarios a Roles

1. Ve a **Azure Portal** ? **Enterprise applications**
2. Busca `LAMAApp API`
3. Ve a **Users and groups** ? **Add user/group**
4. Selecciona el usuario
5. Selecciona el rol (Admin o Standard)
6. Click **Assign**

---

## API Endpoints

### Autenticación
Todos los endpoints requieren un **Bearer Token JWT** válido.

### Miembros

#### GET `/api/Miembros`
Obtiene todos los miembros (Admin y Standard)

**Response:**
```json
[
  {
    "id": 1,
    "nombre": "Juan",
    "apellido": "Pérez",
    "rango": "Admin",
    "estatus": "Activo",
    "member": 1001,
    ...
  }
]
```

#### GET `/api/Miembros/{id}`
Obtiene un miembro por ID (Admin y Standard)

#### GET `/api/Miembros/rango/{rango}`
Filtra miembros por rango (Admin y Standard)

#### GET `/api/Miembros/estatus/{estatus}`
Filtra miembros por estatus (Admin y Standard)

#### POST `/api/Miembros`
Crea un nuevo miembro (Solo Admin)

**Request Body:**
```json
{
  "nombre": "Juan",
  "apellido": "Pérez",
  "cedula": "1234567890",
  "fechaNacimiento": "1990-01-01",
  "celular": "3001234567",
  "ciudad": "Medellín",
  "member": 1001,
  "rango": "Standard",
  "estatus": "Activo",
  "moto": "CBR 600RR",
  "marca": "Honda",
  "placaMatricula": "ABC123",
  ...
}
```

#### PUT `/api/Miembros/{id}`
Actualiza un miembro existente (Solo Admin)

#### DELETE `/api/Miembros/{id}`
Elimina un miembro (Solo Admin)

---

## Testing

### Ejecutar Tests (Futuro)
```bash
dotnet test
```

### Probar API con Swagger
1. Navega a `https://localhost:7199/swagger`
2. Click en **Authorize**
3. Ingresa tu Bearer Token
4. Prueba los endpoints

---

## Troubleshooting

### Error: "Audience validation failed"
- Verifica que `Audience` en `appsettings.json` coincida con el ClientId de la API
- En desarrollo, `ValidateAudience` está deshabilitado

### Error: "Authorization failed"
- Verifica que el usuario tenga un rol asignado (Admin o Standard) en Azure AD
- Revisa los claims del token en los logs del backend

### Error de conexión a la base de datos
- Verifica la cadena de conexión en `appsettings.json`
- Asegúrate de que SQL Server esté ejecutándose
- Verifica que las migraciones estén aplicadas

### El frontend no puede conectarse al backend
- Verifica que ambos proyectos estén ejecutándose
- Revisa la configuración de CORS en `Program.cs`
- Verifica que `Api:BaseUrl` en el frontend apunte a la URL correcta

---

# Contribuir

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

## Licencia

Este proyecto es privado y pertenece al capítulo L.A.M.A. Medellín.

---

## Contacto

**L.A.M.A. Medellín**
- GitHub: [@daireto](https://github.com/daireto)
- Repositorio: [LAMAApp](https://github.com/daireto/LAMAApp)

---

## Agradecimientos

- Capítulo L.A.M.A. Medellín por la oportunidad
- Microsoft por las herramientas de desarrollo
- Comunidad de .NET y Blazor

---

**Desarrollado con para L.A.M.A. Medellín**

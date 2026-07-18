# CitasApp — Arquitectura Hexagonal con APIs REST

**Alumno:** Armando Cen
**Materia:** Arquitectura de Software  
**Escuela:** Tecnológico de Software  
**Semana:** 6  

---

## Tabla de Contenidos

- [¿Qué es?](#qué-es)
- [De MVC a Arquitectura Hexagonal](#de-mvc-a-arquitectura-hexagonal)
- [Las 4 Capas Explicadas](#las-4-capas-explicadas)
- [APIs REST Implementadas](#apis-rest-implementadas)
- [Patrones de Diseño](#patrones-de-diseño)
- [Beneficios](#beneficios-de-la-arquitectura-hexagonal)
- [Flujo de Requests](#flujo-de-una-request-en-las-apis-rest)
- [Cómo Ejecutar](#-cómo-ejecutar)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Licencia](#-licencia)

---

## ¿Qué es?

Se migró la aplicación **CitasApp** de una arquitectura **MVC tradicional** (un solo proyecto) a una **arquitectura hexagonal multi-proyecto**, separando responsabilidades en **4 capas independientes**. 

Además, se implementaron **2 capas de API REST**:

1. **API REST para Citas** - Gestión de citas médicas mediante HTTP
2. **API REST para Calculadora** - Operaciones matemáticas básicas vía HTTP

---

## De MVC a Arquitectura Hexagonal

###  Antes (MVC — Un Solo Proyecto)

```
Citas_App/
├── Controllers/
├── Models/
├── Interfaces/
├── Repositories/
├── Views/
└── Program.cs
```

**Problema:** Todo vivía en el mismo proyecto. Los modelos, la lógica de acceso a datos y la presentación estaban mezclados sin una separación clara de responsabilidades.

### Después (Arquitectura Hexagonal — 4 Capas + 2 APIs REST)

```
CitasApp.sln
│
├── CitasApp.Domain/                ← El núcleo del negocio
│   ├── Models/
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Cita.cs
│   │   └── ErrorViewModel.cs
│   └── Interfaces/
│       ├── IPacienteRepository.cs
│       ├── IMedicoRepository.cs
│       └── ICitaRepository.cs
│
├── CitasApp.Application/           ← Lógica de aplicación (Casos de uso)
│   └── Services/
│       ├── PacienteService.cs
│       ├── MedicoService.cs
│       ├── CitaService.cs
│       └── CalculadoraService.cs
│
├── CitasApp.Infrastructure/        ← Adaptadores de salida
│   └── Repositories/
│       ├── JsonPacienteRepository.cs
│       ├── JsonMedicoRepository.cs
│       └── JsonCitaRepository.cs
│
├── CitasApp.Web/                   ← Adaptador de entrada (MVC)
│   ├── Controllers/
│   ├── Views/
│   ├── data/
│   └── Program.cs
│
├── CitasApp.api/                   ← API REST para Citas
│   ├── Controllers/Api/
│   │   ├── CitasController.cs
│   │   ├── PacientesController.cs
│   │   └── MedicosController.cs
│   ├── DTOs/
│   └── Program.cs
│
└── CitasApp.Api-Calculadora/       ← API REST para Calculadora
    ├── Controllers/
    │   └── CalculadoraController.cs
    └── Program.cs
```

### Referencias Entre Proyectos

```
CitasApp.Web
├─→ CitasApp.Application    ← Los Services orquestan la lógica
├─→ CitasApp.Domain         ← Modelos e interfaces
└─→ CitasApp.Infrastructure (transitivamente via Application)

CitasApp.api
├─→ CitasApp.Application    ← Reutiliza los Services
├─→ CitasApp.Domain         ← Modelos e interfaces
└─→ CitasApp.Infrastructure (transitivamente)

CitasApp.Api-Calculadora
└─→ CitasApp.Application    ← CalculadoraService

CitasApp.Application
├─→ CitasApp.Domain         ← Usa los modelos
└─→ CitasApp.Infrastructure ← Usa los Repositories

CitasApp.Infrastructure
└─→ CitasApp.Domain         ← Implementa las interfaces

CitasApp.Domain
└─→ (no depende de nadie)   ← Completamente independiente 
```

---

## Las 4 Capas Explicadas

### 1. **Domain** (El Corazón del Negocio) 

**Responsabilidad:** Define las entidades y contratos del negocio.

```
CitasApp.Domain/
├── Models/
│   ├── Paciente.cs         ← Paciente con ID, nombre, email
│   ├── Medico.cs           ← Médico con especialidad
│   ├── Cita.cs             ← Cita médica con fecha, hora
│   └── ErrorViewModel.cs   ← Modelo de errores
└── Interfaces/
    ├── IPacienteRepository ← Contrato para datos de pacientes
    ├── IMedicoRepository   ← Contrato para datos de médicos
    └── ICitaRepository     ← Contrato para datos de citas
```

**Características:**
-  No conoce de bases de datos
-  No sabe de controllers ni HTTP
-  Completamente independiente
-  Solo define QUÉ es un Paciente, Médico, Cita

---

### 2. **Application** (Orquestación de Lógica) 

**Responsabilidad:** Implementa los casos de uso y orquesta servicios.

```
CitasApp.Application/
└── Services/
    ├── PacienteService
    │   └── GetAllPacientes()
    │   └── GetPacienteById()
    │   └── CreatePaciente()
    │
    ├── MedicoService
    │   └── GetAllMedicos()
    │   └── GetMedicoById()
    │
    ├── CitaService
    │   └── GetAllCitas()
    │   └── GetCitasById()
    │   └── GetCitasPorPaciente()
    │   └── CreateCita()
    │   └── UpdateCita()
    │   └── DeleteCita()
    │
    └── CalculadoraService
        └── Sumar()
        └── Restar()
        └── Multiplicar()
        └── Dividir()
```

**Características:**
-  Aquí va toda la lógica de aplicación
-  Los Services usan Repositories para obtener datos
-  Los Controllers usan Services para satisfacer requests
-  Intermediario entre presentación y datos

---

### 3. **Infrastructure** (Adaptadores de Salida) 🔌

**Responsabilidad:** Implementa el acceso a datos.

```
CitasApp.Infrastructure/
└── Repositories/
    ├── JsonPacienteRepository
    │   └── Implementa IPacienteRepository
    │
    ├── JsonMedicoRepository
    │   └── Implementa IMedicoRepository
    │
    └── JsonCitaRepository
        └── Implementa ICitaRepository
```

**Características:**
- Implementa las interfaces del Domain
   Define **CÓMO** se accede a los datos (JSON, SQL, API, etc.)
-  Fácil de cambiar: `JsonRepository` → `SqlServerRepository` sin afectar el resto

---

### 4. **Web** (Adaptador de Entrada — MVC) 

**Responsabilidad:** Presenta la interfaz gráfica al usuario.

```
CitasApp.Web/
├── Controllers/
│   ├── HomeController.cs
│   ├── PacienteController.cs
│   ├── MedicoController.cs
│   └── CitaController.cs
├── Views/
│   ├── Paciente/
│   ├── Medico/
│   ├── Cita/
│   └── Home/
└── Program.cs
```

**Características:**
- Uno de los posibles clientes de la aplicación
-  Se pueden agregar múltiples adaptadores sin modificar el núcleo
-  Usa Services para obtener datos
-  Renderiza vistas HTML

---

## 📡 APIs REST Implementadas

### API REST de Citas (`CitasApp.api`)

Expone endpoints para gestionar citas, pacientes y médicos:

| Verbo | Endpoint | Descripción |
|-------|----------|-------------|
| `GET` | `/api/Citas` | Obtener todas las citas |
| `GET` | `/api/Citas/{id}` | Obtener cita por ID |
| `GET` | `/api/Citas/por-paciente/{pacienteId}` | Citas de un paciente |
| `POST` | `/api/Citas` | Crear nueva cita |
| `PUT` | `/api/Citas/{id}` | Actualizar cita |
| `DELETE` | `/api/Citas/{id}` | Eliminar cita |
| `GET` | `/api/Pacientes` | Obtener todos los pacientes |
| `GET` | `/api/Medicos` | Obtener todos los médicos |

**Respuesta Exitosa (200 OK):**
```json
{
  "id": 1,
  "pacienteId": 1,
  "medicoId": 1,
  "fecha": "2026-06-20",
  "hora": "14:30:00",
  "motivo": "Consulta general"
}
```

---

### API REST de Calculadora (`CitasApp.Api-Calculadora`)

Expone endpoints para operaciones matemáticas básicas:

| Verbo | Endpoint | Descripción |
|-------|----------|-------------|
| `GET` | `/api/Calculadora/sumar?a=5&b=3` | Suma dos números |
| `GET` | `/api/Calculadora/restar?a=10&b=4` | Resta dos números |
| `GET` | `/api/Calculadora/multiplicar?a=6&b=7` | Multiplica dos números |
| `GET` | `/api/Calculadora/dividir?a=20&b=4` | Divide dos números |

**Respuesta Exitosa (200 OK):**
```json
{
  "a": 5,
  "b": 3,
  "resultado": 8,
  "operacion": "suma"
}
```

---

##  Patrones de Diseño

### Repository Pattern
Abstrae el acceso a datos mediante interfaces, permitiendo cambiar la implementación sin afectar servicios.

```csharp
public interface IPacienteRepository
{
    Task<IEnumerable<Paciente>> GetAll();
    Task<Paciente> GetById(int id);
    Task Add(Paciente paciente);
    Task Update(Paciente paciente);
    Task Delete(int id);
}
```

**Ventaja:** Cambiar `JsonRepository` por `SqlRepository` es solo cambiar 1 línea en `Program.cs`.

---

### Dependency Injection
En `Program.cs` se registran servicios y repositorios para inyección automática:

```csharp
builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CalculadoraService>();
```

**Ventaja:** Loose coupling, fácil de testear con mocks.

---

### service Layer
Los Services orquestan la lógica y son reutilizados por múltiples clientes:

```csharp
public class CitaService
{
    private readonly ICitaRepository _repository;
    
    public CitaService(ICitaRepository repository)
    {
        _repository = repository; // Inyección de dependencia
    }
    
    public async Task<Cita> CreateCita(Cita cita)
    {
        // Lógica de validación
        // Lógica de negocio
        return await _repository.Add(cita);
    }
}
```

**Ventaja:** Reutilización en Web y APIs sin duplicar código.

---

### DTOs (Data Transfer Objects)
Separa la estructura de las APIs del modelo interno:

```csharp
public class CitaDTO
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public string Fecha { get; set; }
    public string Hora { get; set; }
}
```

**Ventaja:** Protege la lógica de negocio, mayor flexibilidad en respuestas.

---

###  CORS Habilitado
Permite que clientes desde cualquier origen accedan a las APIs:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

**Ventaja:** APIs accesibles desde React, Postman, aplicaciones externas, etc.

---

### Swagger Integrado
Las APIs generan documentación automática y permite testing en browser:

```csharp
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();
```

**Ventaja:** Documentación actualizada automáticamente, pruebas sin Postman.

---

## Beneficios de la Arquitectura Hexagonal

| Beneficio | Descripción |
|-----------|-------------|
| **Separación de responsabilidades** | Cada capa tiene un trabajo claro. Domain no sabe de BD, Web no sabe de lógica |
| **Intercambiabilidad de adaptadores** | Cambiar `JsonRepository` por `SqlRepository` es trivial |
| **Testabilidad** | Services y Repositories basados en interfaces, fácil crear mocks |
| **Mantenibilidad** | Un cambio en BD no afecta Controllers. Cambio en UI no afecta lógica |
| **Escalabilidad** | Agregar nuevos adaptadores (APIs, gRPC, móvil) es independiente del núcleo |
| **Reutilización** | Services usados por Web, API REST, Calculadora API sin duplicar |
| **Multi-cliente** | Misma lógica sirve a MVC, APIs REST y otros clientes simultáneamente |

---

## Flujo de una Request en las APIs REST

```
┌─────────────────────────────────────┐
│ Cliente HTTP (Postman, React, etc.) │
└──────────────┬──────────────────────┘
               │
               ▼
┌─────────────────────────────────────────────────┐
│ CitasController / CalculadoraController         │
│ ├─ Valida parámetros                           │
│ └─ Recibe la petición                          │
└──────────────┬──────────────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────────────┐
│ CitaService / CalculadoraService                │
│ ├─ Aplica reglas de negocio                    │
│ └─ Orquesta la lógica                          │
└──────────────┬──────────────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────────────┐
│ CitaRepository                                   │
│ └─ Accede a datos (solo para Citas)            │
└──────────────┬──────────────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────────────┐
│ JSON Files / Base de Datos                      │
│ └─ Persiste información                         │
└──────────────┬──────────────────────────────────┘
               │
               ▼
┌─────────────────────────────────────┐
│ Respuesta JSON al cliente           │
│ ├─ Status Code 200 OK               │
│ └─ Body (JSON serializado)          │
└─────────────────────────────────────┘
```

---

## Cómo Ejecutar

### Requisitos

- **.NET Framework** 4.7 o superior
- **Visual Studio** 2022
- **Git** (opcional, para clonar)

### Pasos

####  Clonar repositorio

```bash
git clone https://github.com/kiki-bot-sudo/ArqSoft-S05-Enrique.git
cd ArqSoft-S05-Enrique
git checkout Api-Calculadora
```

#### Abrir solución en Visual Studio

```bash
start CitasApp.sln
```

O abre Visual Studio manualmente → File → Open Project/Solution → CitasApp.sln

####  Compilar solución

- Menú: **Compilar → Recompilar solución**
- O presiona: `Ctrl + Shift + B`

#### Ejecutar aplicaciones

**Opción A: MVC Web**
- Click en botón ▶ Run (ejecuta `CitasApp.Web` por defecto)
- Accede a: `http://localhost:44345`

**Opción B: API REST Citas**
- Click derecha en proyecto `CitasApp.api` → Set as Startup Project
- Click en botón ▶ Run
- Swagger: `http://localhost:7172/swagger`

**Opción C: API Calculadora**
- Click derecha en proyecto `CitasApp.Api-Calculadora` → Set as Startup Project
- Click en botón ▶ Run
- Swagger: `http://localhost:5000/swagger`

####  Acceder a endpoints

| Aplicación | URL |
|-----------|-----|
| Web MVC | http://localhost:44345 |
| API Citas (Swagger) | http://localhost:7172/swagger |
| API Calculadora (Swagger) | http://localhost:5000/swagger |

---

##  Pruebas con Swagger

1. Abre el Swagger de cualquier API en tu navegador
2. Expande un endpoint (click en la flecha)
3. Click en **"Try it out"**
4. Ingresa parámetros en los campos
5. Click en **"Execute"**
6. Observa la respuesta (Status Code + Body)

**Ejemplo:**
```
GET /api/Calculadora/sumar?a=10&b=5

Respuesta:
{
  "a": 10,
  "b": 5,
  "resultado": 15,
  "operacion": "suma"
}
```

---

##  Estructura del Proyecto

```
CitasApp/
│
├── CitasApp.Domain/
│   ├── Models/
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Cita.cs
│   │   └── ErrorViewModel.cs
│   │
│   └── Interfaces/
│       ├── IPacienteRepository.cs
│       ├── IMedicoRepository.cs
│       └── ICitaRepository.cs
│
├── CitasApp.Application/
│   └── Services/
│       ├── PacienteService.cs
│       ├── MedicoService.cs
│       ├── CitaService.cs
│       └── CalculadoraService.cs
│
├── CitasApp.Infrastructure/
│   └── Repositories/
│       ├── JsonPacienteRepository.cs
│       ├── JsonMedicoRepository.cs
│       └── JsonCitaRepository.cs
│
├── CitasApp.Web/
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── PacienteController.cs
│   │   ├── MedicoController.cs
│   │   └── CitaController.cs
│   │
│   ├── Views/
│   │   ├── Paciente/
│   │   │   ├── Index.cshtml
│   │   │   ├── Create.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   └── Delete.cshtml
│   │   ├── Medico/
│   │   ├── Cita/
│   │   └── Home/
│   │
│   ├── data/ (JSON files)
│   │   ├── pacientes.json
│   │   ├── medicos.json
│   │   └── citas.json
│   │
│   └── Program.cs
│
├── CitasApp.api/
│   ├── Controllers/
│   │   ├── CitasController.cs
│   │   ├── PacientesController.cs
│   │   └── MedicosController.cs
│   │
│   ├── DTOs/
│   │   ├── CitaDTO.cs
│   │   ├── PacienteDTO.cs
│   │   └── MedicoDTO.cs
│   │
│   └── Program.cs
│
├── CitasApp.Api-Calculadora/
│   ├── Controllers/
│   │   └── CalculadoraController.cs
│   │
│   └── Program.cs
│
└── CitasApp.sln
```

---

## Configuración Importante

### `Program.cs` en Web y APIs

```csharp
// Registrar Servicios
builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();
builder.Services.AddScoped<PacienteService>();

// Registrar APIs
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
```

---

## Solución de Problemas

| Problema | Solución |
|----------|----------|
| **Error CS0246: References ambiguas de `Task`** | Verificar `using System.Threading.Tasks;` |
| **Error CS0006: Referencias entre proyectos no resueltas** | Asegurar que las referencias `.csproj` existan |
| **Swagger no aparece** | Verificar `app.UseSwagger()` y `app.UseSwaggerUI()` en `Program.cs` |
| **CORS bloqueado** | Habilitar CORS en `Program.cs` con `AddCors()` |
| **Puerto ya en uso** | Cambiar puerto en `Properties/launchSettings.json` |
| **JSON file no encontrado** | Verificar ruta en Repository (ej: `data/citas.json`) |

---

## 📝 Cláusula de Uso de IA

Durante el desarrollo de **CitasApp**, se utilizó **Claude (Anthropic)** como herramienta asistente para:

-  **Identificación de errores de compilación:** Diagnóstico de errores `CS0246`, `CS0006` y propuesta de soluciones
- **Depuración de configuración:** Asistencia en problemas de namespaces, inyección de dependencias y referencias entre proyectos
-  **Generación de estructura:** Creación de la arquitectura base de 4 capas con patrones SOLID
-  **Implementación de APIs REST:** Código de Controllers y configuración de CORS/Swagger
-  **Refactorización de Services:** Agregación de nuevos servicios y actualización de configuración
-  **Creación de commits documentados:** Mensajes de git claros (feat, fix, docs)

La IA actuó como **acelerador de desarrollo y debugger**, permitiendo enfoque en el aprendizaje.

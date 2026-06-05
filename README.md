# Citas_App

Aplicación web para la gestión de citas médicas, desarrollada como proyecto académico para la materia de **Arquitectura de Software**.

**Autor:** Enrique Zavala  
**Materia:** Arquitectura de Software  
**Tecnología:** ASP.NET Core MVC — C#

---

## Descripción

Citas_App permite consultar y filtrar citas médicas, así como visualizar información de médicos y pacientes. La persistencia de datos se realiza mediante archivos JSON, sin necesidad de una base de datos relacional.

---

## Arquitectura

El proyecto aplica el patrón **Repository** con separación clara de capas:

```
Citas_App/
├── Controllers/      # Controladores MVC (Cita, Médico, Paciente, Home)
├── Interfaces/       # Contratos de los repositorios
├── Repositories/     # Implementaciones JSON de cada repositorio
├── Models/           # Modelos de dominio (Cita, Médico, Paciente)
├── Views/            # Vistas Razor por entidad
└── Data/             # Archivos JSON de persistencia
```

- **Inyección de dependencias** registrada en `Program.cs` con `AddScoped<IInterface, Implementacion>()`.
- Las interfaces desacoplan la lógica de negocio del mecanismo de persistencia, facilitando en un futuro cambiar a una base de datos sin modificar los controladores.

---

## Entidades

| Entidad  | Campos principales |
|----------|--------------------|
| Médico   | Id, Nombre, Apellido, Especialidad, NumeroLicencia |
| Paciente | Id, Nombre, Apellido, Email, Teléfono |
| Cita     | Id, PacienteId, MedicoId, Fecha, Hora, Motivo, Estado |

---

## Funcionalidades

- Listado de médicos con vista de detalle individual
- Listado de pacientes con vista de detalle individual
- Listado de todas las citas con filtro por paciente

---

## Cómo ejecutar

1. Clonar el repositorio
2. Abrir `Citas_App.slnx` en Visual Studio 2022 o posterior
3. Ejecutar con `F5` o `dotnet run` desde la carpeta `Citas_App/`

No se requiere configuración adicional de base de datos.

---

## Cláusula de uso de IA

Durante el desarrollo de este proyecto se utilizó inteligencia artificial (Claude de Anthropic) como herramienta de apoyo para la **identificación y visualización de errores** en el código, tales como namespaces inconsistentes, código muerto y rutas de archivos con sensibilidad a mayúsculas.

Todas las correcciones fueron realizadas manualmente por el autor. La IA no generó código funcional para este proyecto; su rol fue exclusivamente el de revisión y diagnóstico.

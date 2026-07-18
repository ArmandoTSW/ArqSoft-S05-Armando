# CitasApp — Arquitectura Hexagonal con APIs REST

**Alumno:** `Armando Cen`
**Materia:** `Arquitectura de Software`
**Semana:** `6`

---

## Tabla de Contenidos

- `¿Qué es?`
- `Estructura del Proyecto`
- `Arquitectura de 4 Capas`
- `APIs REST Implementadas`
- `Patrones de Diseño`
- `Cómo Ejecutar`
- `Configuración Importante`
- `Solución de Problemas`
- `Cláusula de Uso de IA`

---

## ¿Qué es?

Se migró la aplicación `CitasApp` de una arquitectura `MVC` tradicional (un solo proyecto) a una arquitectura hexagonal multi-proyecto, separando responsabilidades en `4` capas independientes. Además, se implementaron `2` capas de `API REST`: una para gestión de citas y otra para una calculadora.

---

## Estructura del Proyecto

CitasApp/
├── CitasApp.Domain/
├── CitasApp.Application/
├── CitasApp.Infrastructure/
├── CitasApp.Web/
├── CitasApp.api/
├── CitasApp.Api-Calculadora/
└── CitasApp.sln

---

## Arquitectura de 4 Capas

1. `Domain`: Núcleo del negocio. Contiene modelos e interfaces. Es independiente de cualquier tecnología externa.
2. `Application`: Lógica de aplicación. Contiene los servicios que orquestan los casos de uso.
3. `Infrastructure`: Implementa las interfaces definidas en el dominio para el acceso a datos (`JSON`).
4. `Web`: Adaptador de entrada (`MVC`). Interfaz de usuario para la interacción web.

---

## APIs REST Implementadas

- `CitasApp.api`: API para la gestión de citas, pacientes y médicos.
- `CitasApp.Api-Calculadora`: API para realizar operaciones matemáticas básicas.

---

## Patrones de Diseño

- `Repository Pattern`: Abstracción de datos para facilitar el intercambio de persistencia.
- `Dependency Injection`: Configuración de servicios en `Program.cs` para asegurar el desacoplamiento.
- `Service Layer`: Centralización de la lógica para reutilización en `Web` y `APIs`.
- `DTOs`: Objetos de transferencia de datos para proteger el modelo interno.

---

## Cómo Ejecutar

1. Clonar el repositorio: `git clone https://github.com/ArmandoTSW/ArqSoft-S05-Armando.git`
2. Abrir la solución `CitasApp.sln` en `Visual Studio 2022`.
3. Compilar la solución: `Ctrl + Shift + B`.
4. Ejecutar el proyecto deseado (`Web`, `api` o `Api-Calculadora`) estableciéndolo como "Proyecto de inicio".

---

## Configuración Importante

En el archivo `Program.cs` de los proyectos `Web` y `API` se realiza el registro de servicios mediante inyección de dependencias:

builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();
builder.Services.AddScoped<PacienteService>();

Se habilita `CORS` y `Swagger` para permitir la comunicación con clientes externos.

---

## Solución de Problemas

- `Error CS0246`: Verificar los `using` en las clases.
- `Error CS0006`: Verificar que las referencias entre proyectos sean correctas en la solución.
- `Swagger no aparece`: Asegurar que `app.UseSwagger()` esté configurado en `Program.cs`.
- `CORS`: Si el cliente no conecta, verificar la política de `CORS` en `Program.cs`.

---

## Cláusula de Uso de IA

Durante el desarrollo de este proyecto, se utilizó `Gemini` como herramienta asistente para:

- Identificación y diagnóstico de errores de compilación.
- Asistencia en la estructuración de la arquitectura hexagonal.
- Implementación de controladores, `DTOs` y configuración de `APIs REST`.
- Refactorización de servicios e inyección de dependencias.

La responsabilidad final de la implementación, validación y decisiones arquitectónicas recae en el autor del proyecto, `Armando Cen`.

# CitasApp — Arquitectura Hexagonal con APIs REST

**Alumno:** Armando Cen
**Materia:** Arquitectura de Software
**Semana:** 6

---

## Tabla de Contenidos

- [¿Qué es?](#qué-es)
- [De MVC a Arquitectura Hexagonal](#de-mvc-a-arquitectura-hexagonal)
- [Las 4 Capas Explicadas](#las-4-capas-explicadas)
- [APIs REST Implementadas](#apis-rest-implementadas)
- [Patrones de Diseño](#patrones-de-diseño)
- [Beneficios de la Arquitectura Hexagonal](#beneficios-de-la-arquitectura-hexagonal)
- [Flujo de una Request](#flujo-de-una-request)
- [Cómo Ejecutar](#cómo-ejecutar)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Configuración Importante](#configuración-importante)
- [Solución de Problemas](#solución-de-problemas)
- [Cláusula de Uso de IA](#cláusula-de-uso-de-ia)

---

## ¿Qué es?

Se migró la aplicación CitasApp de una arquitectura MVC tradicional (un solo proyecto) a una arquitectura hexagonal multi-proyecto, separando responsabilidades en 4 capas independientes. Además, se implementaron 2 capas de API REST: una para gestión de citas y otra para una calculadora.

---

## De MVC a Arquitectura Hexagonal

### Antes (MVC — Un Solo Proyecto)
Todo vivía en el mismo proyecto, mezclando modelos, lógica de acceso a datos y la presentación, sin separación clara.

### Después (Arquitectura Hexagonal — 4 Capas + 2 APIs REST)
- CitasApp.Domain: El núcleo del negocio (modelos e interfaces).
- CitasApp.Application: Lógica de aplicación (servicios).
- CitasApp.Infrastructure: Adaptadores de salida (acceso a datos JSON).
- CitasApp.Web: Adaptador de entrada MVC.
- CitasApp.api: API REST para citas.
- CitasApp.Api-Calculadora: API REST para calculadora.

---

## Las 4 Capas Explicadas

1. Domain: Define las entidades (Paciente, Medico, Cita) y contratos (interfaces). No conoce de base de datos ni HTTP.
2. Application: Orquesta los casos de uso a través de servicios (PacienteService, CitaService, etc.).
3. Infrastructure: Implementa las interfaces de los repositorios para acceder a datos (JSON).
4. Web: Presenta la interfaz gráfica y renderiza las vistas.

---

## APIs REST Implementadas

### API REST de Citas (CitasApp.api)
- GET /api/Citas: Obtener todas las citas.
- GET /api/Citas/{id}: Obtener cita por ID.
- POST /api/Citas: Crear cita.
- PUT /api/Citas/{id}: Actualizar cita.
- DELETE /api/Citas/{id}: Eliminar cita.

### API REST de Calculadora (CitasApp.Api-Calculadora)
- GET /api/Calculadora/sumar?a=X&b=Y: Suma dos números.
- GET /api/Calculadora/restar?a=X&b=Y: Resta dos números.
- GET /api/Calculadora/multiplicar?a=X&b=Y: Multiplica dos números.
- GET /api/Calculadora/dividir?a=X&b=Y: Divide dos números.

---

## Patrones de Diseño

- Repository Pattern: Abstrae el acceso a datos mediante interfaces.
- Dependency Injection: Registro de servicios en Program.cs para un sistema desacoplado.
- Service Layer: Orquestación de lógica para reutilización en Web y APIs.
- DTOs: Objetos de transferencia de datos para separar la estructura de la API del modelo interno.
- CORS: Habilitado para permitir acceso desde cualquier origen.
- Swagger: Documentación automática de endpoints para pruebas.

---

## Beneficios de la Arquitectura Hexagonal

| Beneficio | Descripción |
|-----------|-------------|
| Separación | Cada capa tiene una responsabilidad clara. |
| Intercambiabilidad | Cambiar la persistencia (ej. SQL) es sencillo. |
| Testabilidad | Fácil de testear con mocks. |
| Mantenibilidad | Cambios en UI o BD no afectan al núcleo. |
| Escalabilidad | Fácil adición de nuevos adaptadores. |

---

## Flujo de una Request

1. Cliente HTTP envía petición.
2. Controller recibe y valida los parámetros.
3. Service ejecuta la lógica de negocio.
4. Repository accede a los datos.
5. Se retorna respuesta JSON al cliente.

---

## Cómo Ejecutar

1. Clonar el repositorio: git clone https://github.com/ArmandoTSW/ArqSoft-S05-Armando.git
2. Abrir solución CitasApp.sln en Visual Studio 2022.
3. Compilar solución: Ctrl + Shift + B.
4. Ejecutar proyectos:
   - Web MVC: Seleccionar CitasApp.Web y ejecutar.
   - API Citas: Seleccionar CitasApp.api como proyecto de inicio.
   - API Calculadora: Seleccionar CitasApp.Api-Calculadora como proyecto de inicio.

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

## Configuración Importante

En Program.cs de los proyectos Web y API se realiza el registro de servicios mediante inyección de dependencias:
- builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();
- builder.Services.AddScoped<PacienteService>();
- Configuración de CORS y Swagger para las APIs.

---

## Solución de Problemas

- Error CS0246: Verificar referencias y 'using'.
- Error CS0006: Asegurar que los proyectos estén correctamente referenciados.
- Swagger no aparece: Verificar middlewares en Program.cs.
- CORS bloqueado: Revisar la política de CORS en el Program.cs de las APIs.

---

## Cláusula de Uso de IA

Durante el desarrollo de CitasApp, se utilizó Gemini (Google) como herramienta asistente para:
- Diagnóstico de errores de compilación y referencias entre proyectos.
- Estructuración de la arquitectura hexagonal de 4 capas.
- Implementación de controladores, DTOs y configuración de APIs REST.
- Refactorización de servicios e inyección de dependencias.

La responsabilidad final de la implementación, validación y decisiones arquitectónicas recae en el autor del proyecto, Armando Cen.

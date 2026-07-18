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
- [Beneficios de la Arquitectura Hexagonal](#beneficios-de-la-arquitectura-hexagonal)
- [Flujo de una Request en las APIs REST](#flujo-de-una-request-en-las-apis-rest)
- [Cómo Ejecutar](#cómo-ejecutar)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Configuración Importante](#configuración-importante)
- [Solución de Problemas](#solución-de-problemas)
- [Cláusula de Uso de IA](#cláusula-de-uso-de-ia)

---

## ¿Qué es?

Se migró la aplicación CitasApp de una arquitectura MVC tradicional (un solo proyecto) a una arquitectura hexagonal multi-proyecto, separando responsabilidades en 4 capas independientes.

Además, se implementaron 2 capas de API REST:

1. API REST para Citas - Gestión de citas médicas mediante HTTP
2. API REST para Calculadora - Operaciones matemáticas básicas vía HTTP

---

## De MVC a Arquitectura Hexagonal

### Antes (MVC — Un Solo Proyecto)

Citas_App/
├── Controllers/
├── Models/
├── Interfaces/
├── Repositories/
├── Views/
└── Program.cs

Problema: Todo vivía en el mismo proyecto. Los modelos, la lógica de acceso a datos y la presentación estaban mezclados sin una separación clara de responsabilidades.

### Después (Arquitectura Hexagonal — 4 Capas + 2 APIs REST)

CitasApp.sln
│
├── CitasApp.Domain/                # El núcleo del negocio
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
├── CitasApp.Application/           # Lógica de aplicación (Casos de uso)
│   └── Services/
│       ├── PacienteService.cs
│       ├── MedicoService.cs
│       ├── CitaService.cs
│       └── CalculadoraService.cs
│
├── CitasApp.Infrastructure/        # Adaptadores de salida
│   └── Repositories/
│       ├── JsonPacienteRepository.cs
│       ├── JsonMedicoRepository.cs
│       └── JsonCitaRepository.cs
│
├── CitasApp.Web/                   # Adaptador de entrada (MVC)
│   ├── Controllers/
│   ├── Views/
│   ├── data/
│   └── Program.cs
│
├── CitasApp.api/                   # API REST para Citas
│   ├── Controllers/Api/
│   │   ├── CitasController.cs
│   │   ├── PacientesController.cs
│   │   └── MedicosController.cs
│   ├── DTOs/
│   └── Program.cs
│
└── CitasApp.Api-Calculadora/       # API REST para Calculadora
    ├── Controllers/
    │   └── CalculadoraController.cs
    └── Program.cs

---

## Las 4 Capas Explicadas

### 1. Domain (El Corazón del Negocio)
Responsabilidad: Define las entidades y contratos del negocio.
Características: No conoce de bases de datos, ni de controllers ni HTTP. Es independiente.

### 2. Application (Orquestación de Lógica)
Responsabilidad: Implementa los casos de uso y orquesta servicios. Aquí va la lógica de negocio.

### 3. Infrastructure (Adaptadores de Salida)
Responsabilidad: Implementa el acceso a datos. Define CÓMO se accede a la información (JSON, SQL, etc.).

### 4. Web (Adaptador de Entrada — MVC)
Responsabilidad: Presenta la interfaz gráfica al usuario. Se pueden agregar múltiples adaptadores sin modificar el núcleo.

---

## APIs REST Implementadas

### API REST de Citas (CitasApp.api)
Expone endpoints para gestionar citas, pacientes y médicos (GET, POST, PUT, DELETE).

### API REST de Calculadora (CitasApp.Api-Calculadora)
Expone endpoints para operaciones matemáticas básicas (sumar, restar, multiplicar, dividir).

---

## Patrones de Diseño

### Repository Pattern
Abstrae el acceso a datos mediante interfaces, permitiendo cambiar la implementación sin afectar servicios.

### Dependency Injection
En Program.cs se registran servicios y repositorios para inyección automática, permitiendo desacoplamiento.

### Service Layer
Los Services orquestan la lógica y son reutilizados por múltiples clientes (Web, API).

### DTOs (Data Transfer Objects)
Separa la estructura de las APIs del modelo interno, protegiendo la lógica de negocio.

---

## Beneficios de la Arquitectura Hexagonal

- Separación de responsabilidades: Cada capa tiene un trabajo claro.
- Intercambiabilidad de adaptadores: Cambiar de origen de datos es trivial.
- Testabilidad: Services y Repositories basados en interfaces facilitan pruebas.
- Mantenibilidad: Cambios en UI o BD no afectan al núcleo del negocio.
- Escalabilidad: Fácil adición de nuevos adaptadores (APIs, gRPC, móvil).

---

## Cómo Ejecutar

1. Clonar repositorio: git clone https://github.com/ArmandoTSW/ArqSoft-S05-Armando.git
2. Abrir solución en Visual Studio: CitasApp.sln
3. Compilar solución: Ctrl + Shift + B
4. Ejecutar aplicaciones:
   - Web MVC: CitasApp.Web
   - API Citas: CitasApp.api
   - API Calculadora: CitasApp.Api-Calculadora

---

## Cláusula de Uso de IA

Durante el desarrollo de CitasApp, se utilizó Gemini (Google) como herramienta asistente para:
- Identificación de errores de compilación y depuración de configuración.
- Generación de estructura de arquitectura de 4 capas.
- Implementación de APIs REST (Controllers, CORS, Swagger).
- Refactorización de Services y configuración de inyección de dependencias.

La IA actuó como acelerador de desarrollo, manteniendo siempre la responsabilidad del autor en las decisiones arquitectónicas y validación final.

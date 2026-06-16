# CitasApp — Arquitectura Hexagonal

**Alumno:** Enrique Zavala  
**Materia:** Arquitectura de Software  
**Escuela:** Tecnológico de Software  
**Semana:** 6  

---

## ¿Qué se hizo en esta práctica?

Se migró la aplicación **CitasApp** de una arquitectura MVC tradicional (un solo proyecto) a una **arquitectura hexagonal multi-proyecto**, separando responsabilidades en **4 capas independientes**.

---

## De MVC a Arquitectura Hexagonal

### Antes (MVC — un solo proyecto)
```
Citas_App/
├── Controllers/
├── Models/
├── Interfaces/
├── Repositories/
├── Views/
└── Program.cs
```
Todo vivía en el mismo proyecto. Los modelos, la lógica de acceso a datos y la presentación estaban mezclados sin una separación clara de responsabilidades.

### Después (Arquitectura Hexagonal — 4 capas)
```
CitasApp.sln
├── CitasApp.Domain/          ← El núcleo del negocio
│   ├── Models/               (Paciente, Medico, Cita, ErrorViewModel)
│   └── Interfaces/           (IPacienteRepository, IMedicoRepository, ICitaRepository)
├── CitasApp.Application/     ← Lógica de aplicación (Casos de uso)
│   └── Services/             (PacienteService, MedicoService, CitaService)
├── CitasApp.Infrastructure/  ← Adaptadores de salida
│   └── Repositories/         (JsonPacienteRepository, JsonMedicoRepository, JsonCitaRepository)
└── CitasApp.Web/             ← Adaptador de entrada (MVC)
    ├── Controllers/
    ├── Views/
    ├── data/
    └── Program.cs
```

### Referencias entre proyectos
```
CitasApp.Web
    ├→ CitasApp.Application  ← Los Services orquestan la lógica
    ├→ CitasApp.Domain       ← Modelos e interfaces
    └→ CitasApp.Infrastructure (transitivamente via Application)

CitasApp.Application
    ├→ CitasApp.Domain       ← Usa los modelos
    └→ CitasApp.Infrastructure ← Usa los Repositories

CitasApp.Infrastructure
    └→ CitasApp.Domain       ← Implementa las interfaces

CitasApp.Domain
    └→ (no depende de nadie)  ← Completamente independiente
```

---

## Las 4 Capas Explicadas

### 1. **Domain** (El corazón del negocio)
Contiene:
- **Models:** `Paciente`, `Medico`, `Cita`, `ErrorViewModel`
- **Interfaces:** `IPacienteRepository`, `IMedicoRepository`, `ICitaRepository`

No conoce de bases de datos, controllers ni nada del mundo exterior. Solo define qué es un Paciente, un Médico y una Cita.

### 2. **Application** (Orquestación de lógica)
Contiene:
- **Services:** `PacienteService`, `MedicoService`, `CitaService`

Aquí va toda la lógica de aplicación. Los Services usan los Repositories para obtener datos y los Controllers los usan para satisfacer requests. Es el intermediario entre la presentación y los datos.

### 3. **Infrastructure** (Adaptadores de salida)
Contiene:
- **Repositories:** `JsonPacienteRepository`, `JsonMedicoRepository`, `JsonCitaRepository`

Implementa las interfaces del Domain. Define **cómo** se accede a los datos (JSON, SQL, API, etc.).

### 4. **Web** (Adaptador de entrada — MVC)
Contiene:
- **Controllers:** `PacienteController`, `MedicoController`, `CitaController`, `HomeController`
- **Views:** HTML que el usuario ve
- **Program.cs:** Configuración de la aplicación

Es solo uno de los posibles clientes. En el futuro podrías agregar una API REST sin tocar nada de las otras capas.

---

## Beneficios de la Arquitectura Hexagonal

1. **Separación de responsabilidades:** Cada capa tiene un trabajo claro. Domain no sabe de bases de datos, Web no sabe de lógica de negocio.

2. **Intercambiabilidad de adaptadores:** Cambiar `JsonPacienteRepository` por `SqlServerPacienteRepository` es trivial — solo cambias la implementación, las interfaces y la lógica siguen igual.

3. **Testabilidad:** Con los Services y Repositories basados en interfaces, es fácil crear mocks para pruebas unitarias.

4. **Mantenibilidad:** Un cambio en la base de datos no afecta los Controllers. Un cambio en la presentación no afecta la lógica de negocio.

5. **Escalabilidad:** Agregar nuevos adaptadores (API REST, gRPC, aplicación móvil) es independiente del núcleo de negocio.

6. **Reutilización:** Los Services pueden ser usados por múltiples clientes (Web, API, CLI) sin duplicar lógica.

---

## Flujo de una Request

```
1. Usuario hace clic en "Ver Pacientes"
   ↓
2. PacienteController.Index() recibe la request
   ↓
3. Controller inyecta PacienteService
   ↓
4. PacienteService.ObtenerTodosPacientes() 
   ↓
5. Service llama IPacienteRepository.ObtenerTodos()
   ↓
6. JsonPacienteRepository implementa la interfaz
   ↓
7. Lee el archivo pacientes.json
   ↓
8. Devuelve List<Paciente> al Service
   ↓
9. Service devuelve al Controller
   ↓
10. Controller pasa a View(pacientes)
    ↓
11. View renderiza HTML con los pacientes
    ↓
12. Usuario ve la lista
```

---

## Cláusula de uso de IA

Durante el desarrollo de esta práctica se utilizó **Claude (Anthropic)** como herramienta de apoyo en el proceso de refactorización. El uso de la IA se limitó a:

- Identificación de errores de compilación (`CS0246`, `CS0006`) y propuesta de soluciones.
- Depuración conjunta de problemas de configuración entre proyectos (referencias, namespaces, `IWebHostEnvironment`).
- Generación de la estructura inicial de archivos `.csproj`, `.sln` y Services.
- Creación de los archivos de la capa Application (Services) y actualización de Controllers.

La comprensión de los conceptos de arquitectura hexagonal, la toma de decisiones sobre la estructura del proyecto, la verificación del funcionamiento final y la integración de las 4 capas fueron responsabilidad del alumno.

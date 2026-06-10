# CitasApp — Arquitectura Hexagonal

**Alumno:** Enrique Zavala  
**Materia:** Arquitectura de Software  
**Escuela:** Tecnológico de Software  
**Semana:** 6  

---

## ¿Qué se hizo en esta práctica?

Se migró la aplicación **CitasApp** de una arquitectura MVC tradicional (un solo proyecto) a una **arquitectura hexagonal multi-proyecto**, separando responsabilidades en tres capas independientes.

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

### Después (Arquitectura Hexagonal — multi-proyecto)
```
CitasApp.sln
├── CitasApp.Domain/          ← El núcleo del negocio
│   ├── Models/               (Paciente, Medico, Cita, ErrorViewModel)
│   └── Interfaces/           (IPacienteRepository, IMedicoRepository, ICitaRepository)
├── CitasApp.Infrastructure/  ← Adaptadores de salida
│   └── Repositories/         (JsonPacienteRepository, JsonMedicoRepository, JsonCitaRepository)
└── CitasApp.Web/             ← Adaptador de entrada
    ├── Controllers/
    ├── Views/
    ├── data/
    └── Program.cs
```

### Referencias entre proyectos
- `CitasApp.Domain` → no depende de nadie (es el núcleo)
- `CitasApp.Infrastructure` → depende de `Domain`
- `CitasApp.Web` → depende de `Domain` e `Infrastructure`

---

## Beneficios de la Arquitectura Hexagonal

1. **Separación de responsabilidades:** El dominio del negocio no conoce ni le importa cómo se almacenan los datos ni cómo se presenta la información.

2. **Intercambiabilidad de adaptadores:** Se puede cambiar `JsonPacienteRepository` por uno que use SQL Server o MongoDB sin tocar una sola línea del Domain o los Controllers, solo cambiando el registro en `Program.cs`.

3. **Testabilidad:** Al depender de interfaces (`IPacienteRepository`), es fácil crear mocks para pruebas unitarias sin necesidad de archivos JSON reales.

4. **Mantenibilidad:** Cada proyecto tiene una responsabilidad clara. Un cambio en la base de datos no afecta la lógica de negocio ni las vistas.

5. **Escalabilidad:** El proyecto puede crecer agregando nuevos adaptadores (API REST, gRPC, otra base de datos) sin romper lo que ya existe.

---

## Cláusula de uso de IA

Durante el desarrollo de esta práctica se utilizó **Claude (Anthropic)** como herramienta de apoyo en el proceso de refactorización. El uso de la IA se limitó a:

- Identificación de errores de compilación (`CS0246`, `CS0006`) y propuesta de soluciones.
- Depuración conjunta de problemas de configuración entre proyectos (referencias, namespaces, `IWebHostEnvironment`).
- Generación de la estructura inicial de archivos `.csproj` y `.sln` para la solución multi-proyecto.

La comprensión de los conceptos de arquitectura hexagonal, la toma de decisiones sobre la estructura del proyecto y la verificación del funcionamiento final fueron responsabilidad del alumno.

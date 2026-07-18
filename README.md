# CitasApp — Arquitectura Hexagonal con APIs REST

**Alumno:** Armando Cen
**Materia:** Arquitectura de Software
**Escuela:** Tecnológico de Software
**Semana:** 6

---

## Tabla de Contenidos

1. ¿Qué es?
2. De MVC a Arquitectura Hexagonal
3. Las 4 Capas Explicadas
4. APIs REST Implementadas
5. Patrones de Diseño
6. Beneficios de la Arquitectura Hexagonal
7. Flujo de una Request
8. Cómo Ejecutar
9. Estructura del Proyecto
10. Solución de Problemas
11. Cláusula de Uso de IA

---

## 1. ¿Qué es?

Se migró la aplicación CitasApp de una arquitectura MVC tradicional (un solo proyecto) a una arquitectura hexagonal multi-proyecto, separando responsabilidades en 4 capas independientes. Además, se implementaron 2 capas de API REST: una para Citas y otra para una Calculadora.

---

## 2. De MVC a Arquitectura Hexagonal

### Antes (MVC)
Todo vivía en el mismo proyecto, mezclando modelos, lógica de acceso a datos y presentación.

### Después (Arquitectura Hexagonal — 4 Capas)
- CitasApp.Domain (Núcleo)
- CitasApp.Application (Lógica/Casos de uso)
- CitasApp.Infrastructure (Persistencia)
- CitasApp.Web (UI/MVC)

---

## 3. Las 4 Capas Explicadas

- Domain: Entidades y contratos. Independiente de la infraestructura.
- Application: Orquesta servicios y casos de uso.
- Infrastructure: Implementa interfaces de datos (JSON/SQL).
- Web: Presentación y adaptadores de entrada.

---

## 4. APIs REST Implementadas

### API Citas (CitasApp.api)
- GET /api/Citas: Obtener todas las citas.
- POST /api/Citas: Crear cita.
- GET /api/Pacientes: Listar pacientes.

### API Calculadora (CitasApp.Api-Calculadora)
- GET /api/Calculadora/sumar?a=X&b=Y: Suma.
- GET /api/Calculadora/restar?a=X&b=Y: Resta.

---

## 5. Patrones de Diseño

- Repository Pattern: Abstrae el acceso a datos para permitir cambios de persistencia sin tocar la lógica.
- Dependency Injection: Inyección de dependencias en Program.cs para un sistema desacoplado.
- Service Layer: Orquesta la lógica para que sea reutilizable tanto por Web como por APIs.
- DTOs: Objetos de transferencia de datos para separar la estructura de la API del modelo interno.

---

## 6. Beneficios de la Arquitectura Hexagonal

- Separación de responsabilidades: Cada capa tiene un trabajo claro.
- Intercambiabilidad: Cambiar de origen de datos es trivial.
- Testabilidad: Facilita pruebas mediante mocks.
- Mantenibilidad: Cambios en UI o BD no afectan al núcleo del negocio.

---

## 7. Flujo de una Request

1. Cliente HTTP (Postman/Web).
2. Controller (Valida parámetros).
3. Service (Ejecuta lógica de negocio).
4. Repository (Accede a datos).
5. Respuesta JSON al cliente.

---

## 8. Cómo Ejecutar

1. Clonar repositorio: git clone https://github.com/ArmandoTSW/ArqSoft-S05-Armando.git
2. Abrir solución: CitasApp.sln en Visual Studio 2022.
3. Compilar: Ctrl + Shift + B.
4. Ejecutar:
   - Web MVC: Seleccionar proyecto CitasApp.Web
   - API Citas: Seleccionar proyecto CitasApp.api
   - API Calculadora: Seleccionar proyecto CitasApp.Api-Calculadora

---

## 9. Estructura del Proyecto

CitasApp/
├── CitasApp.Domain/
├── CitasApp.Application/
├── CitasApp.Infrastructure/
├── CitasApp.Web/
├── CitasApp.api/
└── CitasApp.Api-Calculadora/

---

## 10. Solución de Problemas

- Error CS0246: Verificar using System.Threading.Tasks.
- Error CS0006: Asegurar referencias de proyectos en Visual Studio.
- Swagger no aparece: Verificar app.UseSwagger en Program.cs.
- CORS bloqueado: Verificar configuración en Program.cs.

---

## 11. Cláusula de Uso de IA

Durante el desarrollo de CitasApp, se utilizó Gemini (Google) como herramienta asistente para:
- Identificación de errores de compilación.
- Depuración de configuración de inyección de dependencias.
- Generación de estructura de arquitectura de 4 capas.
- Implementación de APIs REST y configuración de CORS.

La IA actuó como acelerador de desarrollo, manteniendo siempre la responsabilidad del autor en las decisiones arquitectónicas y validación final.
